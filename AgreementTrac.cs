using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Principal;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;

using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


using DirectShowLib;

namespace AgreementTrac
{
    public partial class AgreementTrac : Form
    {
        #region Imports

        [DllImport("user32.dll")]
        public static extern int SendMessage(
              IntPtr hWnd,      // handle to destination window
              int Msg,       // message
              IntPtr wParam,  // first message parameter
              IntPtr lParam   // second message parameter
              );

        [DllImport("User32.Dll")]
        public static extern int SendNotifyMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        #endregion

        #region Enums

        enum PlayState
        {
            Stopped,
            Paused,
            Running,
            Init
        };

        #endregion

        #region Constants

        public const int WM_GRAPHNOTIFY = 0x8000 + 1;
        public const int WM_SAVE_STARTED = 0x0400;
        public const int WM_SAVE_COMPLETED = 0x0401;
        public const int WM_CLOSE = 0x10;

        //private const string HKEY_ROOT = "HKEY_LOCAL_MACHINE\\Software";
        //private const string KEY_APPNAME = "AgreementTrac";
        //private const string KEY_NAME = HKEY_ROOT + "\\" + KEY_APPNAME;
        //private const string KEY_INSTALL_DATE = "Install Date";
        //private const string KEY_COMPANY_NAME = "Company Name";
        //private const string KEY_PRODUCT_KEY = "Product Key";
        //private const string KEY_LICENSE_COUNT = "License Count";
        //private const string KEY_SERIAL_NUMBER = "Serial Number";

        #endregion

        #region Private Fields
        SavingDlg m_sd = null;
        PlayState m_enCurrentState = PlayState.Stopped;
        FilterState m_FilterState = FilterState.Stopped;
        IMediaControl m_MediaControl = null;
        IMediaEventEx m_MediaEventEx = null;
        IVideoWindow m_videoWindow = null;
        
        CCapture m_CaptureObjForPreview = null;
        CCapture m_CaptureObjForCapture = null;

        DsDevice[] m_videoCapDevices = null;
        DsDevice[] m_audioCapDevices = null;

        ConfigSettings csPersistedSettings = null;
        ConfigSettings csChangedSettings = null;

        CDealInfo DealInfo = null;

        string m_sSaveFilePath = string.Empty;
        string m_sCaptureFileName = string.Empty;
        
        string m_sConfigPath = string.Empty;
        string m_sUserName = string.Empty;
        string m_sComputerName = string.Empty;
        string m_sDomainName = string.Empty;
        string m_sIPAddress = string.Empty;
        bool m_bAdmin = false;
        bool m_bDataDirty = false;
        IntPtr m_pHwnd;

        //string m_sCompanyName = string.Empty;
        //string m_sProductKey = string.Empty;
        //long m_nLicenseCount = 0;
        //string m_sSerialNumber = string.Empty;
        #endregion
        
        public AgreementTrac()
        {
            InitializeComponent();

            m_pHwnd = this.Handle;
            // Setup directory information
            string sDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string sLowerDir = sDir.ToLower();
            int nIndex = sLowerDir.LastIndexOf("\\");
            
            if (nIndex >= 0)
                sLowerDir = sLowerDir.Substring(0, nIndex);

            m_sConfigPath = sLowerDir;
            
            // Enable Tracer class
            Tracer.SetupTracing(m_sConfigPath);

            //bool bResult = false;
            //bResult = SetRegistryKeys();
            //Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "SetRegistryKeys() returned result: " + bResult.ToString());
            //CheckEvalStatus();

            // Initialize settings objects
            csPersistedSettings = new ConfigSettings(); // object for holding values from config file
            
            GetConfigurationInfo();
            GetSystemInfo();
            csChangedSettings = new ConfigSettings(csPersistedSettings); 
            
            if (UserIsAdmin())
            {
                SetPreviewOrConfigButtonStates();
                panelVideo.Visible = true;
                cboVideoDevices.Enabled = true;
                cboAudioDevices.Enabled = true;
                txtOutputDirectory.Enabled = true;
                txtDealershipName.Enabled = true;
                btnSaveConfig.Visible = true;
                btnBrowse.Visible = true;
                chkAppendUserName.Enabled = true;
                txtSQLServer.Enabled = true;
                txtDatabaseName.Enabled = true;
                chkUseSQLAuth.Enabled = true;
                if (chkUseSQLAuth.Checked)
                {
                    txtSqlUser.Enabled = true;
                    txtPassword.Enabled = true;
                }
                btnSaveConfig.Enabled = m_bDataDirty;
                btnCancel.Enabled = false;
            }
            toolTip1.SetToolTip(chkAppendUserName, "Selecting this check box will append the\r\ncurrently logged in username to the output path.");
            LoadCaptureSettings();
            SetupSaveFilePath();
            
        }

        #region Public Properties

        public string ManagerName
        {
            get { return m_sUserName; }
        }
        public string IP_Address
        {
            get { return m_sIPAddress; }
        }
        public string ComputerName
        {
            get { return m_sComputerName; }
        }
        
        #endregion

        #region Button Methods

        private void NewDealPreviewLoad()
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered NewDealPreviewLoad() Method.");
            CloseInterfaces();
            string sError = string.Empty;
            int hr = 0;
            try
            {
                if (m_CaptureObjForPreview == null)
                    m_CaptureObjForPreview = new CCapture(GetVideoDevice(csChangedSettings.VideoDevice), GetAudioDevice(csChangedSettings.AudioDevice));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CloseInterfaces();
                return;
            }
            if (m_MediaControl == null)
                m_MediaControl = (IMediaControl)m_CaptureObjForPreview.m_FilterGraph;
            if (m_videoWindow == null)
                m_videoWindow = (IVideoWindow)m_CaptureObjForPreview.m_FilterGraph;
            if (m_MediaEventEx == null)
            {
                m_MediaEventEx = (IMediaEventEx)m_CaptureObjForPreview.m_FilterGraph;
                hr = m_MediaEventEx.SetNotifyWindow(pnlNewDealPreview.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
                sError = DsError.GetErrorText(hr);
                if (hr != 0)
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tSetNotifyWindow failed with message: " + sError);
                DsError.ThrowExceptionForHR(hr);
                SetupVideoWindow(true);
            }

            if (m_FilterState == FilterState.Stopped)
                hr = m_MediaControl.Run();

            m_MediaControl.GetState(5, out m_FilterState);

            sError = DsError.GetErrorText(hr);
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tMediaControl.Run() failed with message: " + sError);
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tThe previous error can safely be ignored unless other errors are occurring.");
            m_enCurrentState = PlayState.Running;
            
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting NewDealPreviewLoad() Method.\r\n");
        }
        private void btnPreview_Click(object sender, EventArgs e)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered btnPreview_Click() Method.");
            string sError = string.Empty;
            if (btnPreview.Text == "Stop")
            {
                btnPreview.Text = "Preview";
                while (m_FilterState != FilterState.Stopped)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tAttempting to stop MediaControl");
                    btnPreview.Enabled = false;
                    m_MediaControl.StopWhenReady();
                    m_MediaControl.GetState(0, out m_FilterState);
                }
                CloseInterfaces();
                btnPreview.Enabled = true;
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting btnPreview_Click() Method after stopping preview.\r\n");
                return;
            }
            if (cboVideoDevices.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a video capture device before trying to preview.",
                    "No Video Device Selected", MessageBoxButtons.OK);
                cboVideoDevices.Focus();
            }
            else
            {
                int hr = 0;
                try
                {
                    if (m_CaptureObjForPreview == null)
                        m_CaptureObjForPreview = new CCapture(GetVideoDevice(csChangedSettings.VideoDevice), GetAudioDevice(csChangedSettings.AudioDevice));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    CloseInterfaces();
                    return;
                }
                if (m_MediaControl == null)
                    m_MediaControl = (IMediaControl)m_CaptureObjForPreview.m_FilterGraph;
                if (m_videoWindow == null)
                    m_videoWindow = (IVideoWindow)m_CaptureObjForPreview.m_FilterGraph;
                if (m_MediaEventEx == null)
                {
                    m_MediaEventEx = (IMediaEventEx)m_CaptureObjForPreview.m_FilterGraph;
                    hr = m_MediaEventEx.SetNotifyWindow(pnlVideoPreview.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
                    sError = DsError.GetErrorText(hr);
                    if (hr != 0)
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tSetNotifyWindow failed with message: " + sError);
                    DsError.ThrowExceptionForHR(hr);
                    SetupVideoWindow(false);
                }
                
                if (m_FilterState == FilterState.Stopped)
                    hr = m_MediaControl.Run();

                m_MediaControl.GetState(5, out m_FilterState);

                sError = DsError.GetErrorText(hr);
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tMediaControl.Run() failed with message: " + sError);
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tThe previous error can safely be ignored unless other errors are occurring.");
                m_enCurrentState = PlayState.Running;
                btnPreview.Text = "Stop";
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting btnPreview_Click() Method.\r\n");
            }
        }
        private void btnNewDeal_Click(object sender, EventArgs e)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered btnNewDeal_Click() method.");
            // Test connection string
            btnTestSQLConn_Click(null, null);
            if (m_bDataDirty && !m_bAdmin)
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tApplication needs re-configured by Admin.");
                MessageBox.Show("Configuration is invalid. Please have an Admin reconfigure the application. Application will now exit.", "Configuration Error", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
            else if (m_bDataDirty)
            {
                MessageBox.Show("Configuration is invalid. Please reconfigure the application.", "Configuration Error", MessageBoxButtons.OK);
                if (!panelVideo.Visible)
                    panelVideo.Visible = true;
                return;
            }
            ResetDataFieldsAndDestroyObjects();
            SetDataFieldsEnabledValueTo(true);

            // Instantiate Data Object
            InitDataObjects();
            
            txtManagerName.Text = this.ManagerName;
            txtDealerName.Text = csChangedSettings.Dealership;

            panelDealInfo.Visible = true;
            if (panelVideo.Visible)
                panelVideo.Visible = false;

            InitNewDealButtonStates();
            NewDealPreviewLoad();
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting btnNewDeal_Click() method.");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered btnSaveConfig() Method.");

            if (m_FilterState != FilterState.Stopped || m_CaptureObjForPreview != null)
            {
                btnPreview.Enabled = false;
                btnPreview.Text = "Preview";
                CloseInterfaces();
                btnPreview.Enabled = true;
            }

            if (csChangedSettings.UseSQLAuthentication && (txtSqlUser.Text != string.Empty && txtPassword.Text != string.Empty))
            {
                // create SQL Connection string
                csChangedSettings.SQLConnectionString = CreateConnectionString(csChangedSettings.SqlServer, csChangedSettings.SQLUserName, csChangedSettings.Password);
            }
            else if (txtSQLServer.Text != string.Empty && !chkUseSQLAuth.Checked)
            {
                csChangedSettings.SQLConnectionString = CreateConnectionString(csChangedSettings.SqlServer, "", "");
            }

            if (cboVideoDevices.SelectedIndex < 0 || cboAudioDevices.SelectedIndex < 0 || 
                txtOutputDirectory.Text == string.Empty || txtDealershipName.Text == string.Empty ||
                txtSQLServer.Text == string.Empty || txtDatabaseName.Text == string.Empty)
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tEnter values for all fields before saving config.");
                MessageBox.Show("Please enter values for all fields before saving configuration.", "Configuration values missing.", MessageBoxButtons.OK);
                if (txtDealershipName.Text == string.Empty)
                    txtDealershipName.Focus();
                else if (cboVideoDevices.SelectedIndex < 0)
                    cboVideoDevices.Focus();
                else if (cboAudioDevices.SelectedIndex < 0)
                    cboAudioDevices.Focus();
                else if (txtOutputDirectory.Text == string.Empty)
                    txtOutputDirectory.Focus();
                else if (txtSQLServer.Text == string.Empty)
                    txtSQLServer.Focus();
                else
                    txtDatabaseName.Focus();
            }
            else if (csChangedSettings.UseSQLAuthentication && (txtSqlUser.Text == string.Empty || txtPassword.Text == string.Empty))
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tAdmin failed to enter SQL username or password.");
                MessageBox.Show("SQL Authentication requires a valid user name and password.\r\nPlease ensure both values are filled in.", "Configuration values missing.", MessageBoxButtons.OK);
                txtSqlUser.Focus();
                return;
            }
            else if ( !System.IO.Directory.Exists(txtOutputDirectory.Text) )
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tOutput directory doesn't exist or format invalid.");
                DialogResult dr = MessageBox.Show("Path: " + txtOutputDirectory.Text + " doesn't exist or is invalid.\r\nWould you like to try to create it?", "Invalid Output Path", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(txtOutputDirectory.Text);
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tDirectoryNotFoundException encountered. Message: " + ex.Message);
                        MessageBox.Show(ex.Message);
                        txtOutputDirectory.Focus();        
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tUnhandled exception occurred. Message: " + ex.Message);
                        MessageBox.Show(ex.Message);
                        txtOutputDirectory.Focus();
                    }
                }
                else
                {
                    txtOutputDirectory.Focus();
                }
            }
            else
            {
                try
                {
                    // Lets write the configuration file
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tStarting to save configuration file.");
                    string sFilePath = m_sConfigPath + Path.DirectorySeparatorChar + "AppSettings.xml";
                    XmlDocument doc = new XmlDocument();
                    doc.Load(sFilePath);
                    XmlNodeList nodeList = doc.SelectNodes("/Configuration");

                    foreach (XmlNode n in nodeList)
                    {
                        if (n.HasChildNodes)
                        {
                            XmlNode vidNode = n.SelectSingleNode("descendant::VideoCaptureDevice");
                            vidNode.InnerText = csChangedSettings.VideoDevice = cboVideoDevices.Text;
                            XmlNode audNode = n.SelectSingleNode("descendant::AudioCaptureDevice");
                            audNode.InnerText = csChangedSettings.AudioDevice = cboAudioDevices.Text;
                            XmlNode outputNode = n.SelectSingleNode("descendant::OutputDirectory");
                            outputNode.InnerText = csChangedSettings.OutputDirectory = txtOutputDirectory.Text;
                            XmlNode appendUserNode = n.SelectSingleNode("descendant::AppendUserNameToOutput");
                            csChangedSettings.AppendUserName = chkAppendUserName.Checked;
                            appendUserNode.InnerText = csChangedSettings.AppendUserName.ToString();
                            XmlNode dealershipName = n.SelectSingleNode("descendant::DealershipName");
                            dealershipName.InnerText = csChangedSettings.Dealership = txtDealershipName.Text;
                            XmlNode xnSQLServer = n.SelectSingleNode("descendant::SQLServerInstance");
                            xnSQLServer.InnerText = csChangedSettings.SqlServer = txtSQLServer.Text;
                            XmlNode xnDatabaseName = n.SelectSingleNode("descendant::DatabaseName");
                            xnDatabaseName.InnerText = csChangedSettings.DatabaseName = txtDatabaseName.Text;
                            XmlNode xnUseSQLAuth = n.SelectSingleNode("descendant::UseSQLAuthentication");
                            csChangedSettings.UseSQLAuthentication = chkUseSQLAuth.Checked;
                            xnUseSQLAuth.InnerText = csChangedSettings.UseSQLAuthentication.ToString();
                            XmlNode xnSQLConnectionString = n.SelectSingleNode("descendant::SQLConnectionString");
                            byte[] baConnString = Encoding.ASCII.GetBytes(csChangedSettings.SQLConnectionString);
                            byte[] baPrivateKey = Encoding.ASCII.GetBytes("e");
                            baConnString = MangleConnString(baConnString, baPrivateKey);
                            xnSQLConnectionString.InnerText = Encoding.ASCII.GetString(baConnString); 
                        }
                    }
                    doc.Save(sFilePath);
                    btnSaveConfig.Enabled = m_bDataDirty = false; // may not need bDataDirty flag?
                    if (!btnPreview.Enabled)
                        btnPreview.Enabled = true;

                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tSave configuration file succeeded.");
                }
                catch (XmlException ex)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tSaving configuration file failed with message: " + ex.Message);
                    MessageBox.Show("XmlException Occurred accessing configuration file. Please review log file for more information.",
                        "XmlException Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            csPersistedSettings = null;
            csPersistedSettings = new ConfigSettings(csChangedSettings);
            SetupSaveFilePath();
            btnTestSQLConn_Click(null, null);
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting btnSaveConfig() Method.");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog.ShowDialog();
            
            if (dr == DialogResult.Cancel)
            {
                txtOutputDirectory.Text = txtOutputDirectory.Text;
            }
            else
            {
                txtOutputDirectory.Text = folderBrowserDialog.SelectedPath;
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Trace.Close();
            Application.ExitThread();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnNewDeal_Click(null, null);
        }
        private void btnTestSQLConn_Click(object sender, EventArgs e)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered btnTestSQLConn_Click() method.");
            bool bExceptionCaught = false;
            System.Data.SqlClient.SqlConnection sConn = null;
            try
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tBegin testing SQL Connection.");
                sConn = new System.Data.SqlClient.SqlConnection(csChangedSettings.SQLConnectionString);
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tBegin opening SQLConnection.");
                sConn.Open();
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tOpen SQL Connection successful.");
                SqlCommand cmd = sConn.CreateCommand();
                cmd.CommandText = @"SELECT count(name) FROM master.dbo.sysdatabases WHERE name = N'" + csChangedSettings.DatabaseName + "'";

                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tBegin ExecuteScalar() to test if database exists.");
                Int32 i = (Int32)cmd.ExecuteScalar();
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tExecuteScalar() succeeded. SELECT query returned count: " + i.ToString());
                if ( i == 0 )
                {
                    if (m_bAdmin)
                    {
                        // create database
                        cmd.CommandText = @"CREATE DATABASE " + csChangedSettings.DatabaseName + "";
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tBegin ExecuteNonQuery to create database.");
                        cmd.ExecuteNonQuery();
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tCreate Database succeeded.");
                    }
                    else
                    {
                        // Database doesn't exist. Admin needs to run application to create DB
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tDatabase doesn't exist. Please run as Admin to create database.");
                        MessageBox.Show("Configured database doesn't exist, or SQL error occurred. Please have an admin reconfigure. Application will now exit.",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Trace.Close();
                        if (sConn != null && sConn.State != System.Data.ConnectionState.Closed)
                            sConn.Close();

                        Environment.Exit(0);
                    }
                    
                }

                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tTry to change connection to " + csChangedSettings.DatabaseName + ".");
                sConn.ChangeDatabase(csChangedSettings.DatabaseName);
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tChanged to " + csChangedSettings.DatabaseName + " succeeded.");

                if (m_bAdmin)
                {
                    TextReader tr = new StreamReader(m_sConfigPath + Path.DirectorySeparatorChar + "DBValidate.sql");
                    cmd.CommandText = tr.ReadToEnd();
                    tr.Close();
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tAttempting to Validate DB tables.");
                    cmd.ExecuteNonQuery();
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tValidate DB tables succeeded.");
                }
                else
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tUser is not admin, assume database is ok.");
                }
                
                if (sender != null) // called from loading NewDeal screen
                    MessageBox.Show("Connection Test Successful.", "Success", MessageBoxButtons.OK);

            }
            catch (ArgumentException ex)
            {
                bExceptionCaught = true;
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tArgumentExeption caught. Message: " + ex.Message);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                bExceptionCaught = true;
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tSQLException caught. Message: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                bExceptionCaught = true;
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tInvalidOperationExeption caught, Problem with Connection string. Message: " + ex.Message);
            }
            catch (Exception ex)
            {
                bExceptionCaught = true;
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tUnexpected Exeption caught. Message: " + ex.Message);
            }
            finally
            {
                if (bExceptionCaught)
                {
                    if (!m_bAdmin)
                    {
                        MessageBox.Show("There was a problem with the configuration. Please have an Admin re-configure the application. The application will now exit.");
                        Trace.Close();
                        Application.ExitThread();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong with the SQL connection. Please reconfigure and save settings.", "Error Connecting", MessageBoxButtons.OK);
                        btnSaveConfig.Enabled = m_bDataDirty = true;
                        if (!panelVideo.Visible)
                            panelVideo.Visible = true;
                    }
                }
                if (sConn != null && sConn.State == System.Data.ConnectionState.Open)
                    sConn.Close();

                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tSQL Connection closed successfully.");
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting btnTestSQLConn_Click() method.");
        }
        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            // check for required fields
            if (txtLastName.Text == string.Empty || txtFirstName.Text == string.Empty ||
                txtMake.Text == string.Empty || txtModel.Text == string.Empty || txtYear.Text == string.Empty ||
                txtStockNumber.Text == string.Empty || txtDealNumber.Text == string.Empty)
            {
                MessageBox.Show("One or more required fields are missing. Please fill in the data correctly before starting recording.", 
                    "Data Entry Error", MessageBoxButtons.OK);
                return;
            }
            if (btnCancel.Enabled)
                btnCancel.Enabled = false;  // Do not allow canceling once recording has been initiated.

            if (btnExit.Enabled)
                btnExit.Enabled = false;

            btnStartRecording.Enabled = false; // Disable this button so it cannot be selected again.
            
            // Do not allow any changes to data fields
            SetDataFieldsEnabledValueTo(false);
            
            DealInfo.DealNumber = txtDealNumber.Text;
            m_sCaptureFileName =  DealInfo.customerInfo.LastName.Trim() + DealInfo.customerInfo.FirstName.Substring(0,1).Trim() + DealInfo.DealNumber.Trim() + ".asf";
            DealInfo.videoInfo.VidFileName = m_sCaptureFileName;
            DealInfo.videoInfo.VidSaveDir = m_sSaveFilePath;
            DealInfo.TransactionDate = DateTime.Now;

            bool bSucceeded = StartCapture(DealInfo.videoInfo.VidSaveDir + DealInfo.videoInfo.VidFileName);
            if (!bSucceeded)
            {
                MessageBox.Show("Error occurred when trying to create capture object. Capture has been cancelled.");
                SetDataFieldsEnabledValueTo(true);
                btnStartRecording.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                // Allow the recording to be stopped.
                // Capture should be setup by now. Errors can occur if user is
                // allowed to cancel before capture is fully started.
                btnStopRecording.Enabled = true;
            }
        }
        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
            m_sd = new SavingDlg();
            m_sd.ShowDialog(this);
        }

        #endregion Button Methods

        #region Supporting Methods

        //private bool SetRegistryKeys()
        //{
        //    bool bSuccess = true;
        //    object obj = null;
        //    // Get\Set Company Name registry key
        //    try
        //    {
        //        obj = Registry.GetValue(KEY_NAME, KEY_COMPANY_NAME, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Error getting registry value. Error message: " + ex.Message);
        //        bSuccess = false;
        //    }
        //    if (obj == null)
        //    {
        //        m_sCompanyName = "Evaluation";
        //        Registry.SetValue(KEY_NAME, KEY_COMPANY_NAME, m_sCompanyName);
        //    }
        //    else
        //    {
        //        m_sCompanyName = obj.ToString();
        //    }
        //    // Get\Set Product Key registry key
        //    obj = null;
        //    try
        //    {
        //        obj = Registry.GetValue(KEY_NAME, KEY_PRODUCT_KEY, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Error getting registry value. Error message: " + ex.Message);
        //        bSuccess = false;
        //    }
        //    if (obj == null)
        //    {
        //        m_sProductKey = string.Empty;
        //        Registry.SetValue(KEY_NAME, KEY_PRODUCT_KEY, "");
        //    }
        //    else
        //    {
        //        m_sProductKey = obj.ToString();
        //    }
        //    // Get\Set License count registry key
        //    obj = null;
        //    try
        //    {
        //        obj = Registry.GetValue(KEY_NAME, KEY_LICENSE_COUNT, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Error getting registry value. Error message: " + ex.Message);
        //        bSuccess = false;
        //    }
        //    if (obj == null)
        //    {
        //        m_nLicenseCount = 0;
        //        Registry.SetValue(KEY_NAME, KEY_LICENSE_COUNT, m_nLicenseCount, RegistryValueKind.DWord);
        //    }
        //    else
        //    {
        //        m_nLicenseCount = Convert.ToInt64(obj);
        //    }
        //    // Get\Set Serial Number registry key
        //    obj = null;
        //    try
        //    {
        //        obj = Registry.GetValue(KEY_NAME, KEY_SERIAL_NUMBER, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Error getting registry value. Error message: " + ex.Message);
        //        bSuccess = false;
        //    }
        //    if (obj == null)
        //    {
        //        m_sSerialNumber = string.Empty;
        //        Registry.SetValue(KEY_NAME, KEY_SERIAL_NUMBER, "");
        //    }
        //    else
        //    {
        //        m_sSerialNumber = obj.ToString();
        //    }

        //    return bSuccess;
        //}

        //private void CheckEvalStatus()
        //{
        //    object objDate = null;
        //    TimeSpan tsInstallDate;
        //    TimeSpan tsToday;

        //    long nBinaryInstallDate = 0;
        //    try
        //    {
        //        objDate = Registry.GetValue(KEY_NAME, KEY_INSTALL_DATE, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Error getting registry value. Error message: " + ex.Message);
        //    }
        //    if (objDate == null)
        //    {
        //        nBinaryInstallDate = DateTime.Now.Date.ToBinary();
        //        Registry.SetValue(KEY_NAME, KEY_INSTALL_DATE, nBinaryInstallDate);
        //    }
        //    else
        //    {
        //        nBinaryInstallDate = Convert.ToInt64(objDate);
        //    }

        //    if (m_nLicenseCount <= 0) // Must be evaluation version
        //    {

        //        tsInstallDate = new TimeSpan(DateTime.FromBinary(nBinaryInstallDate).Ticks);
        //        tsToday = new TimeSpan(DateTime.Now.Date.Ticks);

        //        if ((30 - (tsToday.Days - tsInstallDate.Days)) < 0)
        //        {
        //            MessageBox.Show("Evaluation period has expired. Please uninstall the application. Program will now exit", "Evaluation Expired", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            Environment.Exit(0);
        //        }
        //        else
        //        {
        //            int nDaysLeft = (30 - (tsToday.Days - tsInstallDate.Days));
        //            if (nDaysLeft == 0)
        //            {
        //                MessageBox.Show("Day 30 of Evaluation period. Program will expire tomorrow.", "Evaluation expiration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Evaluation period will expire in " + nDaysLeft + " days.", "Evaluation expiration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //}
        
        private void GetSystemInfo()
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered GetSystemInfo() method.");
            m_sUserName = SystemInformation.UserName;
            m_sComputerName = SystemInformation.ComputerName;
            m_sDomainName = SystemInformation.UserDomainName;
            System.Net.IPAddress[] ipAddresses = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ip in ipAddresses)
            {
                if ((ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) && (ip.ToString() != "127.0.0.1"))
                {
                    if (ip.ToString().StartsWith("0"))
                    {
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Invalid IP. Value = " + ip.ToString());
                        continue;
                    }
                    m_sIPAddress = ip.ToString();
                    return;
                }
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting GetSystemInfo() method.");
        }

        private bool UserIsAdmin()
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered IsUserAdmin() Method.");

            string sLocalAdminGroup = @"BUILTIN\Administrators";
            string sDomainAdminGroup = m_sDomainName + @"\Domain Admins";

            try
            {
                AppDomain myDomain = System.Threading.Thread.GetDomain();
                myDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

                WindowsPrincipal myPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;

                if (myPrincipal.IsInRole(sLocalAdminGroup))
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tUser is in local Admin group. Setting m_bAdmin to 'true'.");
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tUserName: " + m_sUserName);
                    m_bAdmin = true;
                }
                else if (myPrincipal.IsInRole(sDomainAdminGroup))
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tUser is in Domain Admin group. Setting m_bAdmin to 'true'.");
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tDomainName: " + m_sDomainName + " UserName: " + m_sUserName);
                    m_bAdmin = true;
                }
                else
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tUser is not an Admin. Setting m_bAdmin to 'false'.");
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tUserName: " + m_sUserName);
                    m_bAdmin = false;
                }
            }
            catch (Exception ex)
            {
                // Something went wrong with finding credentials
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tProblem occurred in GetUserInfo(). \r\n\tFailure message: " + ex.Message);
                m_bAdmin = false;
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting IsUserAdmin() Method.\r\n");
            return m_bAdmin;
        }

        private void LoadCaptureSettings()
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered LoadCaptureSettings() Method.");
            m_videoCapDevices = CCapture.GetVideoDevices();
            m_audioCapDevices = CCapture.GetAudioDevices();

            if (m_videoCapDevices != null)
            {
                foreach (DsDevice d in m_videoCapDevices)
                {
                    cboVideoDevices.Items.Add(d.Name);
                }
                if (csChangedSettings.VideoDevice.Length > 0 && cboVideoDevices.Items.Contains(csChangedSettings.VideoDevice))
                {
                    cboVideoDevices.Text = csChangedSettings.VideoDevice;
                }
               
                if ( !m_bAdmin && !(cboVideoDevices.Items.Contains(csChangedSettings.VideoDevice)) )
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tNot admin, and persisted video solution doesn't exist, please reconfigure.");
                    MessageBox.Show("Selected Video solution doesn't exist, please contact an Admin to re-configure. Application will now exit.", "Configuration Invalid",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Trace.Close();
                    Environment.Exit(0);
                }
            }
            else
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tNo Video capture devices found...Exiting.");
                System.Windows.Forms.MessageBox.Show("No Video Capture devices found. The application will now exit.");
                Trace.Close();
                Environment.Exit(0);
            }
            if (m_audioCapDevices != null)
            {
                foreach (DsDevice d in m_audioCapDevices)
                {
                    cboAudioDevices.Items.Add(d.Name);
                }
                if (csChangedSettings.AudioDevice.Length > 0 && cboAudioDevices.Items.Contains(csChangedSettings.AudioDevice))
                {
                    cboAudioDevices.Text = csChangedSettings.AudioDevice;
                }

                if (!m_bAdmin && !(cboAudioDevices.Items.Contains(csChangedSettings.AudioDevice)))
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tNot admin, and persisted audio solution doesn't exist, please reconfigure.");
                    MessageBox.Show("Selected Audio solution doesn't exist, please contact an Admin to re-configure. Application will now exit.", "Configuration Invalid",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Trace.Close();
                    Environment.Exit(0);
                }
            }
            else
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tNo Audio capture devices found...Exiting.");
                System.Windows.Forms.MessageBox.Show("No Audio Capture devices found. The application will now exit.");
                Trace.Close();
                Environment.Exit(0);
            }
            if (csChangedSettings.OutputDirectory != string.Empty)
            {
                txtOutputDirectory.Text = csChangedSettings.OutputDirectory;
            }
            if (csChangedSettings.Dealership != string.Empty)
            {
                txtDealershipName.Text = csChangedSettings.Dealership;
            }
            if (csChangedSettings.SqlServer != string.Empty)
            {
                txtSQLServer.Text = csChangedSettings.SqlServer;
            }
            if (csChangedSettings.DatabaseName != string.Empty)
            {
                txtDatabaseName.Text = csChangedSettings.DatabaseName;
            }
            if (csChangedSettings.UseSQLAuthentication)
            {
                chkUseSQLAuth.Checked = csChangedSettings.UseSQLAuthentication;
                txtSqlUser.Text = csChangedSettings.SQLUserName;
                txtPassword.Text = csChangedSettings.Password;
            }
            if (!m_bAdmin && (cboVideoDevices.Text == string.Empty ||
                cboAudioDevices.Text == string.Empty ||
                txtOutputDirectory.Text == string.Empty ||
                txtDealershipName.Text == string.Empty ||
                csChangedSettings.SQLConnectionString == string.Empty))
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tOne or more configuration values are null, check AppSettings.xml file");
                MessageBox.Show("One or more configuration values are missing, please have an admin re-configure the application. The Application will now exit.", "Configuration Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.Close();
                Environment.Exit(0);
            }
            chkAppendUserName.Checked = csChangedSettings.AppendUserName;

            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting LoadCaptureSettings() Method.\r\n");
        }

        private void SetupSaveFilePath()
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Entered SetupSaveFilePath() method.");
            // ensure that m_sSaveFilePath has a ending '/' character
            if (csChangedSettings.AppendUserName)
            {
                // Need to append username to output path, and create it if it doesn't exist
                if (csChangedSettings.OutputDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    m_sSaveFilePath = csChangedSettings.OutputDirectory + m_sUserName + Path.DirectorySeparatorChar;
                else
                    m_sSaveFilePath = csChangedSettings.OutputDirectory + Path.DirectorySeparatorChar + m_sUserName + 
                        Path.DirectorySeparatorChar;
            }
            else
            {
                if (csChangedSettings.OutputDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    m_sSaveFilePath = csChangedSettings.OutputDirectory;
                else
                    m_sSaveFilePath = csChangedSettings.OutputDirectory + Path.DirectorySeparatorChar;
            }
            if (!System.IO.Directory.Exists(m_sSaveFilePath))
            {
                try
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tAttempt to create save file path.");
                    System.IO.Directory.CreateDirectory(m_sSaveFilePath);
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tSave file path created successfully.");
                }
                catch (DirectoryNotFoundException ex)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tDirectoryNotFoundException encountered. Message: " + ex.Message);
                    if (!m_bAdmin)
                    {
                        MessageBox.Show("Error creating save file path. Please have an Admin reconfigure the application. The application will now exit",
                            "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Trace.Close();
                        Environment.Exit(0);
                    }

                }
                catch (Exception ex)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tUnhandled exception occurred. Message: " + ex.Message);
                    MessageBox.Show("Unexpected error occurred. Message: " + ex.Message + "\r\nApplication will now exit.");
                    Trace.Close();
                    Environment.Exit(0);
                }
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Exiting SetupSaveFilePath() method.");
        }

        [FileIOPermission(SecurityAction.Assert)]
        private void GetConfigurationInfo()
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered GetConfigurationInfo() Method.");
            string sAppSettingsPath = m_sConfigPath + Path.DirectorySeparatorChar + "AppSettings.xml";
            FileInfo fiConfigFile = new FileInfo(sAppSettingsPath);

            if (!fiConfigFile.Exists)
            {
                // If file doesn't exist, treat as new config.
                try
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tAppSettings.xml file doesn't exist...Creating.");
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(sAppSettingsPath, Encoding.Unicode);

                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlTextWriter.Indentation = 6;
                    xmlTextWriter.Namespaces = false;

                    xmlTextWriter.WriteStartDocument();
                    xmlTextWriter.WriteStartElement("", "Configuration", "");
                    xmlTextWriter.WriteStartElement("", "DealershipName", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("", "VideoCaptureDevice", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("", "AudioCaptureDevice", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("", "OutputDirectory", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("", "AppendUserNameToOutput", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("", "SQLServerInstance", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("", "DatabaseName", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("", "UseSQLAuthentication", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("", "SQLConnectionString", "");
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.Flush();
                    xmlTextWriter.Close();
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tAppSettings.xml file created successfully.");
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tSet Dirty Flag, so save button will be enabled.");
                    m_bDataDirty = true;
                }
                catch (Exception ex)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError,
                        "\tError occurred trying to create AppSettings file.\r\n\t Failed with message: " + ex.Message);
                    MessageBox.Show("Error occurred trying to create configuration file. Please review log file for more information. Program will now exit.",
                        "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Trace.Close();
                    Environment.Exit(0);
                }
            }
            else
            {
                // open file and get values
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tAppSettings.xml file exists...Getting values.");
                try
                {
                    XmlTextReader xmlReader = new XmlTextReader(sAppSettingsPath);
                    xmlReader.ReadStartElement("Configuration");
                    xmlReader.WhitespaceHandling = WhitespaceHandling.None;
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            if (xmlReader.LocalName.Equals("DealershipName"))
                            {
                                csPersistedSettings.Dealership = xmlReader.ReadString();
                            }
                            if (xmlReader.LocalName.Equals("VideoCaptureDevice"))
                            {
                                csPersistedSettings.VideoDevice = xmlReader.ReadString();
                            }
                            if (xmlReader.LocalName.Equals("AudioCaptureDevice"))
                            {
                                csPersistedSettings.AudioDevice = xmlReader.ReadString();
                            }
                            if (xmlReader.LocalName.Equals("OutputDirectory"))
                            {
                                csPersistedSettings.OutputDirectory = xmlReader.ReadString();
                            }
                            if (xmlReader.LocalName.Equals("AppendUserNameToOutput"))
                            {
                                string sChecked = xmlReader.ReadString().ToLower();
                                if (sChecked == "true")
                                    csPersistedSettings.AppendUserName = true;
                                else
                                    csPersistedSettings.AppendUserName = false;
                            }
                            if (xmlReader.LocalName.Equals("UseSQLAuthentication"))
                            {
                                string sChecked = xmlReader.ReadString().ToLower();
                                if (sChecked == "true")
                                    csPersistedSettings.UseSQLAuthentication = true;
                                else
                                    csPersistedSettings.UseSQLAuthentication = false;
                            }
                            if (xmlReader.LocalName.Equals("SQLServerInstance"))
                            {
                                csPersistedSettings.SqlServer = xmlReader.ReadString();
                            }
                            if (xmlReader.LocalName.Equals("DatabaseName"))
                            {
                                csPersistedSettings.DatabaseName = xmlReader.ReadString();
                            }
                            if (xmlReader.LocalName.Equals("SQLConnectionString"))
                            {
                                csPersistedSettings.SQLConnectionString = xmlReader.ReadString(); // will need to decrypt
                                byte[] baConnString = Encoding.ASCII.GetBytes(csPersistedSettings.SQLConnectionString);
                                byte[] baPrivateKey = Encoding.ASCII.GetBytes("e");
                                baConnString = MangleConnString(baConnString, baPrivateKey);
                                csPersistedSettings.SQLConnectionString = Encoding.ASCII.GetString(baConnString);
                            }
                        }
                        if (csPersistedSettings.UseSQLAuthentication)
                        {
                            string[] sConn = csPersistedSettings.SQLConnectionString.Split(Convert.ToChar(";"));
                            foreach (string s in sConn)
                            {
                                if (s.StartsWith("User Id"))
                                {
                                    csPersistedSettings.SQLUserName = s.Substring(s.IndexOf("=") + 1);
                                }
                                else if (s.StartsWith("Password"))
                                {
                                    csPersistedSettings.Password = s.Substring(s.IndexOf("=") + 1);
                                }
                            }
                        }
                    }
                    xmlReader.Close();

                    if (csPersistedSettings.Dealership == string.Empty || csPersistedSettings.VideoDevice == string.Empty ||
                        csPersistedSettings.AudioDevice == string.Empty || csPersistedSettings.OutputDirectory == string.Empty ||
                        csPersistedSettings.SqlServer == string.Empty || csPersistedSettings.DatabaseName == string.Empty ||
                        csPersistedSettings.SQLConnectionString == string.Empty)
                    {
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tOne or more config values were missing.");
                        Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tSet Dirty Flag, so save button will be enabled.");
                        m_bDataDirty = true;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError,
                        "\tError occurred reading values from AppSettings file. \r\n\tFailed with message: " + ex.Message);
                    MessageBox.Show("Error occurred reading values from configuration file. Please review log file for more information. Program will now exit.",
                        "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Trace.Close();
                    Environment.Exit(0);
                }
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting GetConfigurationInfo() Method.\r\n");
        }

        private DsDevice GetVideoDevice(string sVideoCaptureDevice)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered GetVideoDevice() Method.");
            DsDevice device = null;
            foreach (DsDevice dev in m_videoCapDevices)
            {
                if (dev.Name == sVideoCaptureDevice)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tFound VideoCaptureDevice, returning device: " + dev.Name);
                    device = dev;
                }
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting GetVideoDevice() Method.\r\n");
            return device;
        }

        private DsDevice GetAudioDevice(string sAudioCaptureDevice)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered GetAudioDevice() Method.");
            DsDevice device = null;
            foreach (DsDevice dev in m_audioCapDevices)
            {
                if (dev.Name == sAudioCaptureDevice)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tFound AudioCaptureDevice, returning device: " + dev.Name);
                    device = dev;
                }
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting GetAudioDevice() Method.\r\n");
            return device;
        }

        private void InitDataObjects()
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered InitDataObjects() method.");
            DealInfo = new CDealInfo(csChangedSettings.SQLConnectionString, this.ManagerName, csChangedSettings.DatabaseName);
            DealInfo.IP_Address = this.IP_Address;
            DealInfo.ComputerName = this.ComputerName;
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting InitDataObjects() method.");
        }
        
        private byte[] MangleConnString(byte[] baConnString, byte[] baPrivKey)
        {
            for (int i = 0; i < baConnString.Length; ++i)
            {
                int b = (int)baConnString[i];
                int key = (int)baPrivKey[0];
                int result = b ^ key;
                baConnString[i] = (byte)result;
            }
            return baConnString;
        }

        private string CreateConnectionString(string sServerName, string sUserName, string sPassword)
        {
            string sConnString = string.Empty;
            if (sUserName == string.Empty && sPassword == string.Empty)
            {
                // use integrated security "Data Source=Your_Server_Name;Initial Catalog=Your_Database_Name;Integrated Security=SSPI;" 
                return sConnString = string.Format("Data Source={0};Integrated Security=SSPI;", sServerName);
            }
            else
            {
                // use SQL Authentication "Data Source=Your_Server_Name;Initial Catalog= Your_Database_Name;UserId=Your_Username;Password=Your_Password;"
                return sConnString = string.Format("Data Source={0};User Id={1};Password={2};",
                                                    sServerName, sUserName, sPassword);
            }
        }

        private void CloseInterfaces()
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered CloseInterfaces() Method.");
            try
            {
                if (m_MediaControl != null)
                {
                    if (m_enCurrentState != PlayState.Stopped)
                        m_MediaControl.StopWhenReady();

                    m_MediaControl.GetState(0, out m_FilterState);

                    while (m_FilterState != FilterState.Stopped)
                    {
                        m_MediaControl.GetState(0, out m_FilterState);
                    }
                }

                // Stop receiving events
                if (m_MediaEventEx != null)
                    m_MediaEventEx.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero);

                // Relinquish ownership of video window
                if (m_videoWindow != null)
                {
                    m_videoWindow.put_Visible(OABool.False);
                    m_videoWindow.put_Owner(IntPtr.Zero);
                }
                m_enCurrentState = PlayState.Stopped;

                if (m_MediaControl != null)
                    m_MediaControl = null;
                if (m_MediaEventEx != null)
                    m_MediaEventEx = null;
                if (m_videoWindow != null)
                    m_videoWindow = null;

                if (m_CaptureObjForPreview != null)
                {
                    m_CaptureObjForPreview.Dispose();
                    m_CaptureObjForPreview = null;
                }
                if (m_CaptureObjForCapture != null)
                {
                    m_CaptureObjForCapture.Dispose();
                    m_CaptureObjForCapture = null;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tCloseInterfaces failed with message: " + ex.Message);
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting CloseInterfaces() Method.\r\n");
        }

        public void SetupVideoWindow(bool bNewDeal)
        {
            int hr = 0;

            // Set the video window to be a child of the main window
            if (bNewDeal)
                hr = this.m_videoWindow.put_Owner(pnlNewDealPreview.Handle);
            else
                hr = this.m_videoWindow.put_Owner(pnlVideoPreview.Handle);
            
            string sErrorText = DsError.GetErrorText(hr);
            DsError.ThrowExceptionForHR(hr);

            hr = this.m_videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
            sErrorText = DsError.GetErrorText(hr);
            DsError.ThrowExceptionForHR(hr);

            ResizeVideoWindow(bNewDeal);

            // Make the video window visible
            hr = this.m_videoWindow.put_Visible(OABool.True);
            sErrorText = DsError.GetErrorText(hr);
            DsError.ThrowExceptionForHR(hr);

        }

        public void ResizeVideoWindow(bool bNewDeal)
        {
            if (this.m_videoWindow != null)
            {
                if (bNewDeal)
                    this.m_videoWindow.SetWindowPosition(0, 0, pnlNewDealPreview.ClientSize.Width, pnlNewDealPreview.ClientSize.Height);
                else
                    this.m_videoWindow.SetWindowPosition(0, 0, pnlVideoPreview.ClientSize.Width, pnlVideoPreview.ClientSize.Height);
            }
        }

        public void HandleGraphEvent()
        {
            int hr = 0;
            EventCode evCode;
            IntPtr evParam1, evParam2;

            if (m_MediaEventEx == null)
                return;

            while (m_MediaEventEx.GetEvent(out evCode, out evParam1, out evParam2, 0) == 0)
            {
                // Free event parameters to prevent memory leaks associated with
                // event parameter data.  While this application is not interested
                // in the received events, applications should always process them.
                hr = m_MediaEventEx.FreeEventParams(evCode, evParam1, evParam2);
                string sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);

            }
        }
        
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_GRAPHNOTIFY:
                    {
                        HandleGraphEvent();
                        break;
                    }
                case WM_SAVE_STARTED:
                    {
                        SetSaveStartedState();
                        break;
                    }
                case WM_SAVE_COMPLETED:
                    {
                        SetSaveCompletedState();
                        break;
                    }
            }

            // Pass this message to the video window for notification of system changes
            if (m_videoWindow != null)
                m_videoWindow.NotifyOwnerMessage(m.HWnd, m.Msg, m.WParam, m.LParam);

            base.WndProc(ref m);
        }

        private void SetSaveCompletedState()
        {
            this.btnStopRecording.Text = "Stop Recording";
            btnNewDeal.Enabled = true;
            btnExit.Enabled = true;
        }

        private void InitNewDealButtonStates()
        {
            btnStartRecording.Enabled = true;
            btnNewDeal.Enabled = false;
            btnCancel.Enabled = true;
            btnSearch.Enabled = false;
            btnStopRecording.Enabled = false;
        }

        private void SetSaveStartedState()
        {
            this.btnStopRecording.Enabled = false;
            this.btnStopRecording.Text = "Saving Data...";
        }

        private void SetPreviewOrConfigButtonStates()
        {
            if (csChangedSettings.Equals(csPersistedSettings))
            {
                btnSaveConfig.Enabled = m_bDataDirty = false;
                btnPreview.Enabled = true;
            }
            else
            {
                btnSaveConfig.Enabled = m_bDataDirty = true;
                btnPreview.Enabled = false;
            }
        }

        private void SetDataFieldsEnabledValueTo(bool bEnable)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered SetDataFieldsEnabledValueTo() method");
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tSetting all data fields to :" + bEnable.ToString());
            
            txtStockNumber.Enabled = txtVinNumber.Enabled = bEnable;
            txtMake.Enabled = txtModel.Enabled = txtYear.Enabled = bEnable;
            txtFirstName.Enabled = txtLastName.Enabled = txtDealNumber.Enabled = bEnable;
            chkAH.Enabled = chkETCH.Enabled = chkGAP.Enabled = chkMaint.Enabled = chkVSC.Enabled = bEnable;
            txtNotes.Enabled = bEnable;
            
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting SetDataFieldsEnabledValueTo() method");
        }

        private void ResetDataFieldsAndDestroyObjects()
        {
            // reset all values
            txtStockNumber.Text = txtVinNumber.Text = txtMake.Text = txtModel.Text = string.Empty;
            txtYear.Text = txtFirstName.Text = txtLastName.Text = txtDealNumber.Text = txtNotes.Text = string.Empty;
            chkAH.Checked = chkETCH.Checked = chkGAP.Checked = chkMaint.Checked = chkVSC.Checked = false;
            if (DealInfo != null)
            {
                DealInfo = null;
            }
        }

        private bool StartCapture(string sFullPath)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered StartCapture() method.");
            string sError = string.Empty;
            int hr = 0;
            try
            {
                CloseInterfaces();

                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tTry to create CaptureObject.");
                m_CaptureObjForCapture = new CCapture(GetVideoDevice(csChangedSettings.VideoDevice),
                                    GetAudioDevice(csChangedSettings.AudioDevice), sFullPath);
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tCaptureObject created successfully.");
            }
            catch (Exception ex)
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tException caught. Message: " + ex.Message);
                CloseInterfaces();
                return false;
            }
            if (m_MediaControl == null)
                m_MediaControl = (IMediaControl)m_CaptureObjForCapture.m_FilterGraph;

            if (m_FilterState != FilterState.Running)
                hr = m_MediaControl.Run();

            m_MediaControl.GetState(50, out m_FilterState);

            sError = DsError.GetErrorText(hr);
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tMediaControl.Run() returned message: " + sError);
            if (hr != 0)
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tThe previous error can safely be ignored unless other errors are occurring.");

            m_enCurrentState = PlayState.Running;
            return true;
        }

        #endregion

        #region Changed Configuration Event handlers

        private void cboVideoDevices_SelectionChangeCommitted(object sender, EventArgs e)
        {
            csChangedSettings.VideoDevice = cboVideoDevices.Text;
            SetPreviewOrConfigButtonStates();
        }
        private void cboAudioDevices_SelectionChangeCommitted(object sender, EventArgs e)
        {
            csChangedSettings.VideoDevice = cboAudioDevices.Text;
            SetPreviewOrConfigButtonStates();
        }
        private void txtOutputDirectory_TextChanged(object sender, EventArgs e)
        {
            csChangedSettings.OutputDirectory = txtOutputDirectory.Text;
            SetPreviewOrConfigButtonStates();
        }
        private void txtDealershipName_TextChanged(object sender, EventArgs e)
        {
            csChangedSettings.Dealership = txtDealershipName.Text;
            SetPreviewOrConfigButtonStates();
        }
        private void txtSQLServer_TextChanged(object sender, EventArgs e)
        {
            csChangedSettings.SqlServer = txtSQLServer.Text;
            SetPreviewOrConfigButtonStates();
        }
        private void chkAppendUserName_CheckedChanged(object sender, EventArgs e)
        {
            csChangedSettings.AppendUserName = chkAppendUserName.Checked;
            SetPreviewOrConfigButtonStates();
        }
        private void chkUseSQLAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseSQLAuth.Checked)
            {
                txtSqlUser.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                txtSqlUser.Enabled = false;
                txtPassword.Enabled = false;
            }

            csChangedSettings.UseSQLAuthentication = chkUseSQLAuth.Checked;
            SetPreviewOrConfigButtonStates();
        }
        private void txtSqlUser_TextChanged(object sender, EventArgs e)
        {
            csChangedSettings.SQLUserName = txtSqlUser.Text;
            SetPreviewOrConfigButtonStates();
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            csChangedSettings.Password = txtPassword.Text;
            SetPreviewOrConfigButtonStates();
        }
        private void txtDatabaseName_TextChanged(object sender, EventArgs e)
        {
            csChangedSettings.DatabaseName = txtDatabaseName.Text;
            SetPreviewOrConfigButtonStates();
        }
        private void btnSaveConfig_EnabledChanged(object sender, EventArgs e)
        {
            if (btnSaveConfig.Enabled)
                btnTestSQLConn.Enabled = false;
            else
                btnTestSQLConn.Enabled = true;
        }

        #endregion

        #region Changed Data Fields Event handlers

        private void txtStockNumber_TextChanged(object sender, EventArgs e)
        {
            DealInfo.vehicleInfo.StockNumber = txtStockNumber.Text;
        }
        private void txtVinNumber_TextChanged(object sender, EventArgs e)
        {
            DealInfo.vehicleInfo.VIN = txtVinNumber.Text;
        }
        private void txtMake_TextChanged(object sender, EventArgs e)
        {
            DealInfo.vehicleInfo.Make = txtMake.Text;
        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {
            DealInfo.vehicleInfo.Model = txtModel.Text;
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            DealInfo.vehicleInfo.Year = txtYear.Text;
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            DealInfo.customerInfo.FirstName = txtFirstName.Text;
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            DealInfo.customerInfo.LastName = txtLastName.Text;
        }

        private void chkVSC_CheckedChanged(object sender, EventArgs e)
        {
            DealInfo.VSC = chkVSC.Checked;
        }

        private void chkAH_CheckedChanged(object sender, EventArgs e)
        {
            DealInfo.AH = chkAH.Checked;
        }

        private void chkETCH_CheckedChanged(object sender, EventArgs e)
        {
            DealInfo.ETCH = chkETCH.Checked;
        }

        private void chkGAP_CheckedChanged(object sender, EventArgs e)
        {
            DealInfo.GAP = chkGAP.Checked;
        }

        private void chkMaint_CheckedChanged(object sender, EventArgs e)
        {
            DealInfo.MAINT = chkMaint.Checked;
        }

        #endregion

        #region Implement backgroundWorker
        
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SendMessage(m_pHwnd, WM_SAVE_STARTED, IntPtr.Zero, IntPtr.Zero);
            CloseInterfaces();
            AssignDataValues();
            BackgroundWorker bw = sender as BackgroundWorker;
            e.Result = SaveData(bw);
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "Error occurred during SaveData(). Message :" + e.Error.Message);
                SendNotifyMessage(m_sd.Handle, WM_CLOSE, 0, 0);
                string sMsg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(sMsg);
            }
            else
            {
                SendNotifyMessage(m_sd.Handle, WM_CLOSE, 0, 0); 
                SendMessage(m_pHwnd, WM_SAVE_COMPLETED, IntPtr.Zero, IntPtr.Zero);
            }
        }
        private string SaveData(BackgroundWorker bw)
        {
            string sResult = string.Empty;
            sResult = DealInfo.ExecuteSQLInserts(csChangedSettings.DatabaseName);
            return sResult;
        }
        private void AssignDataValues()
        {
            // customer Info
            DealInfo.customerInfo.FirstName = txtFirstName.Text;
            DealInfo.customerInfo.LastName = txtLastName.Text;
            // Address, City, State, Zip, Phone -- Not Implemented

            // Dealership Info
            DealInfo.DealershipName = txtDealerName.Text;

            // Video Info
            // vidFilename and vidSaveDir are set in btnStartRecording_Click() method
            FileInfo fiVidFile = new FileInfo(DealInfo.videoInfo.VidSaveDir + DealInfo.videoInfo.VidFileName);
            DealInfo.videoInfo.VidFileSize = fiVidFile.Length;

            // Vehicle Info
            DealInfo.vehicleInfo.Make = txtMake.Text;
            DealInfo.vehicleInfo.Model = txtModel.Text;
            DealInfo.vehicleInfo.StockNumber = txtStockNumber.Text;
            DealInfo.vehicleInfo.VIN = txtVinNumber.Text;
            DealInfo.vehicleInfo.Year = txtYear.Text;
            // Deal Info
            // DealInfo.CustomerRecordNumber set in btnStartRecording_Click() method
            // DealInfo.IP_Address set in InitDataObjects
            // DealInfo.ComputerName set in InitDataObjects
            DealInfo.AH = chkAH.Checked;
            DealInfo.ETCH = chkETCH.Checked;
            DealInfo.GAP = chkGAP.Checked;
            DealInfo.MAINT = chkMaint.Checked;
            DealInfo.Notes = txtNotes.Text;
            DealInfo.VSC = chkVSC.Checked;
        }
        
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnExit_Click(null, null);
        }

        private void aboutAgreementTracToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //AboutBox ab = new AboutBox();
            //ab.ShowDialog();
        }

        //private void aboutAgreementTracToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    CRegisterProduct frmRegister = new CRegisterProduct(m_sCompanyName, m_sProductKey, m_sSerialNumber, m_nLicenseCount);
        //    frmRegister.ShowDialog();
        //}
        
    }
}