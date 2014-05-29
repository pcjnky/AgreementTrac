using System.Diagnostics;

namespace AgreementTrac
{
    partial class AgreementTrac
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            Trace.Flush();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgreementTrac));
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.btnNewDeal = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelDealInfo = new System.Windows.Forms.Panel();
            this.pnlNewDealPreview = new System.Windows.Forms.Panel();
            this.txtDealerName = new System.Windows.Forms.TextBox();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkMaint = new System.Windows.Forms.CheckBox();
            this.chkGAP = new System.Windows.Forms.CheckBox();
            this.chkETCH = new System.Windows.Forms.CheckBox();
            this.chkAH = new System.Windows.Forms.CheckBox();
            this.chkVSC = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMgrName = new System.Windows.Forms.Label();
            this.txtDealNumber = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.txtVinNumber = new System.Windows.Forms.TextBox();
            this.txtStockNumber = new System.Windows.Forms.TextBox();
            this.txtManagerName = new System.Windows.Forms.TextBox();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.pnlVideoPreview = new System.Windows.Forms.Panel();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.gbConfiguration = new System.Windows.Forms.GroupBox();
            this.btnTestSQLConn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.chkUseSQLAuth = new System.Windows.Forms.CheckBox();
            this.txtSqlUser = new System.Windows.Forms.TextBox();
            this.lblSQLServer = new System.Windows.Forms.Label();
            this.txtSQLServer = new System.Windows.Forms.TextBox();
            this.lblDealershipName = new System.Windows.Forms.Label();
            this.txtDealershipName = new System.Windows.Forms.TextBox();
            this.chkAppendUserName = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblVideoDevices = new System.Windows.Forms.Label();
            this.cboVideoDevices = new System.Windows.Forms.ComboBox();
            this.lblOutputDir = new System.Windows.Forms.Label();
            this.cboAudioDevices = new System.Windows.Forms.ComboBox();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.lblAudioDevices = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAgreementTracToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelDealInfo.SuspendLayout();
            this.panelVideo.SuspendLayout();
            this.gbConfiguration.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(578, 403);
            // 
            // btnNewDeal
            // 
            this.btnNewDeal.Location = new System.Drawing.Point(253, 511);
            this.btnNewDeal.Name = "btnNewDeal";
            this.btnNewDeal.Size = new System.Drawing.Size(75, 23);
            this.btnNewDeal.TabIndex = 21;
            this.btnNewDeal.Text = "New Deal";
            this.btnNewDeal.UseVisualStyleBackColor = true;
            this.btnNewDeal.Click += new System.EventHandler(this.btnNewDeal_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(688, 511);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "New Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(349, 511);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelDealInfo
            // 
            this.panelDealInfo.Controls.Add(this.pnlNewDealPreview);
            this.panelDealInfo.Controls.Add(this.txtDealerName);
            this.panelDealInfo.Controls.Add(this.btnStopRecording);
            this.panelDealInfo.Controls.Add(this.btnStartRecording);
            this.panelDealInfo.Controls.Add(this.label1);
            this.panelDealInfo.Controls.Add(this.txtNotes);
            this.panelDealInfo.Controls.Add(this.chkMaint);
            this.panelDealInfo.Controls.Add(this.chkGAP);
            this.panelDealInfo.Controls.Add(this.chkETCH);
            this.panelDealInfo.Controls.Add(this.chkAH);
            this.panelDealInfo.Controls.Add(this.chkVSC);
            this.panelDealInfo.Controls.Add(this.label10);
            this.panelDealInfo.Controls.Add(this.label9);
            this.panelDealInfo.Controls.Add(this.label8);
            this.panelDealInfo.Controls.Add(this.label7);
            this.panelDealInfo.Controls.Add(this.label6);
            this.panelDealInfo.Controls.Add(this.label5);
            this.panelDealInfo.Controls.Add(this.label4);
            this.panelDealInfo.Controls.Add(this.label3);
            this.panelDealInfo.Controls.Add(this.label2);
            this.panelDealInfo.Controls.Add(this.lblMgrName);
            this.panelDealInfo.Controls.Add(this.txtDealNumber);
            this.panelDealInfo.Controls.Add(this.txtLastName);
            this.panelDealInfo.Controls.Add(this.txtFirstName);
            this.panelDealInfo.Controls.Add(this.txtYear);
            this.panelDealInfo.Controls.Add(this.txtModel);
            this.panelDealInfo.Controls.Add(this.txtMake);
            this.panelDealInfo.Controls.Add(this.txtVinNumber);
            this.panelDealInfo.Controls.Add(this.txtStockNumber);
            this.panelDealInfo.Controls.Add(this.txtManagerName);
            this.panelDealInfo.Location = new System.Drawing.Point(12, 41);
            this.panelDealInfo.Name = "panelDealInfo";
            this.panelDealInfo.Size = new System.Drawing.Size(751, 441);
            this.panelDealInfo.TabIndex = 4;
            this.panelDealInfo.Visible = false;
            // 
            // pnlNewDealPreview
            // 
            this.pnlNewDealPreview.Location = new System.Drawing.Point(545, 36);
            this.pnlNewDealPreview.Name = "pnlNewDealPreview";
            this.pnlNewDealPreview.Size = new System.Drawing.Size(203, 174);
            this.pnlNewDealPreview.TabIndex = 46;
            // 
            // txtDealerName
            // 
            this.txtDealerName.Enabled = false;
            this.txtDealerName.Location = new System.Drawing.Point(314, 36);
            this.txtDealerName.Name = "txtDealerName";
            this.txtDealerName.Size = new System.Drawing.Size(205, 20);
            this.txtDealerName.TabIndex = 1;
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Location = new System.Drawing.Point(411, 397);
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(138, 23);
            this.btnStopRecording.TabIndex = 45;
            this.btnStopRecording.Text = "Stop Recording";
            this.btnStopRecording.UseVisualStyleBackColor = true;
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Location = new System.Drawing.Point(201, 397);
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.Size = new System.Drawing.Size(138, 23);
            this.btnStartRecording.TabIndex = 44;
            this.btnStartRecording.Text = "Start Recording";
            this.btnStartRecording.UseVisualStyleBackColor = true;
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Additional Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(44, 300);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(477, 80);
            this.txtNotes.TabIndex = 41;
            // 
            // chkMaint
            // 
            this.chkMaint.AutoSize = true;
            this.chkMaint.Location = new System.Drawing.Point(212, 240);
            this.chkMaint.Name = "chkMaint";
            this.chkMaint.Size = new System.Drawing.Size(142, 17);
            this.chkMaint.TabIndex = 40;
            this.chkMaint.Text = "Purchased Maintenance";
            this.chkMaint.UseVisualStyleBackColor = true;
            this.chkMaint.CheckedChanged += new System.EventHandler(this.chkMaint_CheckedChanged);
            // 
            // chkGAP
            // 
            this.chkGAP.AutoSize = true;
            this.chkGAP.Location = new System.Drawing.Point(212, 222);
            this.chkGAP.Name = "chkGAP";
            this.chkGAP.Size = new System.Drawing.Size(102, 17);
            this.chkGAP.TabIndex = 39;
            this.chkGAP.Text = "Purchased GAP";
            this.chkGAP.UseVisualStyleBackColor = true;
            this.chkGAP.CheckedChanged += new System.EventHandler(this.chkGAP_CheckedChanged);
            // 
            // chkETCH
            // 
            this.chkETCH.AutoSize = true;
            this.chkETCH.Location = new System.Drawing.Point(44, 258);
            this.chkETCH.Name = "chkETCH";
            this.chkETCH.Size = new System.Drawing.Size(109, 17);
            this.chkETCH.TabIndex = 38;
            this.chkETCH.Text = "Purchased ETCH";
            this.chkETCH.UseVisualStyleBackColor = true;
            this.chkETCH.CheckedChanged += new System.EventHandler(this.chkETCH_CheckedChanged);
            // 
            // chkAH
            // 
            this.chkAH.AutoSize = true;
            this.chkAH.Location = new System.Drawing.Point(44, 240);
            this.chkAH.Name = "chkAH";
            this.chkAH.Size = new System.Drawing.Size(101, 17);
            this.chkAH.TabIndex = 37;
            this.chkAH.Text = "Purchased A+H";
            this.chkAH.UseVisualStyleBackColor = true;
            this.chkAH.CheckedChanged += new System.EventHandler(this.chkAH_CheckedChanged);
            // 
            // chkVSC
            // 
            this.chkVSC.AutoSize = true;
            this.chkVSC.Location = new System.Drawing.Point(44, 222);
            this.chkVSC.Name = "chkVSC";
            this.chkVSC.Size = new System.Drawing.Size(101, 17);
            this.chkVSC.TabIndex = 36;
            this.chkVSC.Text = "Purchased VSC";
            this.chkVSC.UseVisualStyleBackColor = true;
            this.chkVSC.CheckedChanged += new System.EventHandler(this.chkVSC_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(383, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Deal #:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Last Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "First Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(380, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Year:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(209, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Model:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Make:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(317, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Vin #:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Stock #:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Dealer Name:";
            // 
            // lblMgrName
            // 
            this.lblMgrName.AutoSize = true;
            this.lblMgrName.Location = new System.Drawing.Point(41, 20);
            this.lblMgrName.Name = "lblMgrName";
            this.lblMgrName.Size = new System.Drawing.Size(83, 13);
            this.lblMgrName.TabIndex = 18;
            this.lblMgrName.Text = "Manager Name:";
            // 
            // txtDealNumber
            // 
            this.txtDealNumber.Location = new System.Drawing.Point(383, 179);
            this.txtDealNumber.MaxLength = 25;
            this.txtDealNumber.Name = "txtDealNumber";
            this.txtDealNumber.Size = new System.Drawing.Size(139, 20);
            this.txtDealNumber.TabIndex = 9;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(212, 179);
            this.txtLastName.MaxLength = 35;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(134, 20);
            this.txtLastName.TabIndex = 8;
            this.txtLastName.TextChanged += new System.EventHandler(this.txtLastName_TextChanged);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(41, 179);
            this.txtFirstName.MaxLength = 35;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(134, 20);
            this.txtFirstName.TabIndex = 7;
            this.txtFirstName.TextChanged += new System.EventHandler(this.txtFirstName_TextChanged);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(380, 133);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(139, 20);
            this.txtYear.TabIndex = 6;
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(209, 133);
            this.txtModel.MaxLength = 25;
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(134, 20);
            this.txtModel.TabIndex = 5;
            this.txtModel.TextChanged += new System.EventHandler(this.txtModel_TextChanged);
            // 
            // txtMake
            // 
            this.txtMake.Location = new System.Drawing.Point(41, 133);
            this.txtMake.MaxLength = 25;
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(134, 20);
            this.txtMake.TabIndex = 4;
            this.txtMake.TextChanged += new System.EventHandler(this.txtMake_TextChanged);
            // 
            // txtVinNumber
            // 
            this.txtVinNumber.Location = new System.Drawing.Point(317, 83);
            this.txtVinNumber.MaxLength = 17;
            this.txtVinNumber.Name = "txtVinNumber";
            this.txtVinNumber.Size = new System.Drawing.Size(202, 20);
            this.txtVinNumber.TabIndex = 3;
            this.txtVinNumber.TextChanged += new System.EventHandler(this.txtVinNumber_TextChanged);
            // 
            // txtStockNumber
            // 
            this.txtStockNumber.Location = new System.Drawing.Point(41, 83);
            this.txtStockNumber.MaxLength = 25;
            this.txtStockNumber.Name = "txtStockNumber";
            this.txtStockNumber.Size = new System.Drawing.Size(226, 20);
            this.txtStockNumber.TabIndex = 2;
            this.txtStockNumber.TextChanged += new System.EventHandler(this.txtStockNumber_TextChanged);
            // 
            // txtManagerName
            // 
            this.txtManagerName.Enabled = false;
            this.txtManagerName.Location = new System.Drawing.Point(41, 36);
            this.txtManagerName.Name = "txtManagerName";
            this.txtManagerName.Size = new System.Drawing.Size(226, 20);
            this.txtManagerName.TabIndex = 0;
            // 
            // panelVideo
            // 
            this.panelVideo.Controls.Add(this.pnlVideoPreview);
            this.panelVideo.Controls.Add(this.btnSaveConfig);
            this.panelVideo.Controls.Add(this.btnPreview);
            this.panelVideo.Controls.Add(this.gbConfiguration);
            this.panelVideo.Location = new System.Drawing.Point(12, 41);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(751, 441);
            this.panelVideo.TabIndex = 5;
            this.panelVideo.Visible = false;
            // 
            // pnlVideoPreview
            // 
            this.pnlVideoPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVideoPreview.Location = new System.Drawing.Point(13, 20);
            this.pnlVideoPreview.Name = "pnlVideoPreview";
            this.pnlVideoPreview.Size = new System.Drawing.Size(320, 240);
            this.pnlVideoPreview.TabIndex = 11;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(122, 340);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(113, 23);
            this.btnSaveConfig.TabIndex = 10;
            this.btnSaveConfig.Text = "Save Configuration";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Visible = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            this.btnSaveConfig.EnabledChanged += new System.EventHandler(this.btnSaveConfig_EnabledChanged);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(137, 292);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // gbConfiguration
            // 
            this.gbConfiguration.Controls.Add(this.btnTestSQLConn);
            this.gbConfiguration.Controls.Add(this.label13);
            this.gbConfiguration.Controls.Add(this.txtDatabaseName);
            this.gbConfiguration.Controls.Add(this.txtPassword);
            this.gbConfiguration.Controls.Add(this.label12);
            this.gbConfiguration.Controls.Add(this.label11);
            this.gbConfiguration.Controls.Add(this.chkUseSQLAuth);
            this.gbConfiguration.Controls.Add(this.txtSqlUser);
            this.gbConfiguration.Controls.Add(this.lblSQLServer);
            this.gbConfiguration.Controls.Add(this.txtSQLServer);
            this.gbConfiguration.Controls.Add(this.lblDealershipName);
            this.gbConfiguration.Controls.Add(this.txtDealershipName);
            this.gbConfiguration.Controls.Add(this.chkAppendUserName);
            this.gbConfiguration.Controls.Add(this.btnBrowse);
            this.gbConfiguration.Controls.Add(this.lblVideoDevices);
            this.gbConfiguration.Controls.Add(this.cboVideoDevices);
            this.gbConfiguration.Controls.Add(this.lblOutputDir);
            this.gbConfiguration.Controls.Add(this.cboAudioDevices);
            this.gbConfiguration.Controls.Add(this.txtOutputDirectory);
            this.gbConfiguration.Controls.Add(this.lblAudioDevices);
            this.gbConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbConfiguration.Location = new System.Drawing.Point(376, 20);
            this.gbConfiguration.Name = "gbConfiguration";
            this.gbConfiguration.Size = new System.Drawing.Size(356, 400);
            this.gbConfiguration.TabIndex = 11;
            this.gbConfiguration.TabStop = false;
            this.gbConfiguration.Text = "Configuration Settings:";
            // 
            // btnTestSQLConn
            // 
            this.btnTestSQLConn.Location = new System.Drawing.Point(239, 320);
            this.btnTestSQLConn.Name = "btnTestSQLConn";
            this.btnTestSQLConn.Size = new System.Drawing.Size(101, 44);
            this.btnTestSQLConn.TabIndex = 20;
            this.btnTestSQLConn.Text = "Test Connection";
            this.btnTestSQLConn.UseVisualStyleBackColor = true;
            this.btnTestSQLConn.Click += new System.EventHandler(this.btnTestSQLConn_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(182, 238);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Database Name:";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Enabled = false;
            this.txtDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseName.Location = new System.Drawing.Point(185, 254);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(155, 20);
            this.txtDatabaseName.TabIndex = 14;
            this.txtDatabaseName.TextChanged += new System.EventHandler(this.txtDatabaseName_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(19, 366);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 19;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(15, 351);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Password:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(16, 312);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "User Name:";
            // 
            // chkUseSQLAuth
            // 
            this.chkUseSQLAuth.AutoSize = true;
            this.chkUseSQLAuth.Enabled = false;
            this.chkUseSQLAuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseSQLAuth.Location = new System.Drawing.Point(19, 282);
            this.chkUseSQLAuth.Name = "chkUseSQLAuth";
            this.chkUseSQLAuth.Size = new System.Drawing.Size(146, 17);
            this.chkUseSQLAuth.TabIndex = 15;
            this.chkUseSQLAuth.Text = "Use SQL Authentication?";
            this.chkUseSQLAuth.UseVisualStyleBackColor = true;
            this.chkUseSQLAuth.CheckedChanged += new System.EventHandler(this.chkUseSQLAuth_CheckedChanged);
            // 
            // txtSqlUser
            // 
            this.txtSqlUser.Enabled = false;
            this.txtSqlUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSqlUser.Location = new System.Drawing.Point(19, 328);
            this.txtSqlUser.Name = "txtSqlUser";
            this.txtSqlUser.Size = new System.Drawing.Size(200, 20);
            this.txtSqlUser.TabIndex = 17;
            this.txtSqlUser.TextChanged += new System.EventHandler(this.txtSqlUser_TextChanged);
            // 
            // lblSQLServer
            // 
            this.lblSQLServer.AutoSize = true;
            this.lblSQLServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSQLServer.Location = new System.Drawing.Point(15, 238);
            this.lblSQLServer.Name = "lblSQLServer";
            this.lblSQLServer.Size = new System.Drawing.Size(94, 13);
            this.lblSQLServer.TabIndex = 11;
            this.lblSQLServer.Text = "SQL Server name:";
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.Enabled = false;
            this.txtSQLServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLServer.Location = new System.Drawing.Point(18, 254);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Size = new System.Drawing.Size(147, 20);
            this.txtSQLServer.TabIndex = 12;
            this.txtSQLServer.TextChanged += new System.EventHandler(this.txtSQLServer_TextChanged);
            // 
            // lblDealershipName
            // 
            this.lblDealershipName.AutoSize = true;
            this.lblDealershipName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDealershipName.Location = new System.Drawing.Point(16, 26);
            this.lblDealershipName.Name = "lblDealershipName";
            this.lblDealershipName.Size = new System.Drawing.Size(91, 13);
            this.lblDealershipName.TabIndex = 1;
            this.lblDealershipName.Text = "Dealership Name:";
            // 
            // txtDealershipName
            // 
            this.txtDealershipName.Enabled = false;
            this.txtDealershipName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDealershipName.Location = new System.Drawing.Point(18, 45);
            this.txtDealershipName.Name = "txtDealershipName";
            this.txtDealershipName.Size = new System.Drawing.Size(200, 20);
            this.txtDealershipName.TabIndex = 2;
            this.txtDealershipName.TextChanged += new System.EventHandler(this.txtDealershipName_TextChanged);
            // 
            // chkAppendUserName
            // 
            this.chkAppendUserName.AutoSize = true;
            this.chkAppendUserName.Enabled = false;
            this.chkAppendUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAppendUserName.Location = new System.Drawing.Point(19, 206);
            this.chkAppendUserName.Name = "chkAppendUserName";
            this.chkAppendUserName.Size = new System.Drawing.Size(208, 17);
            this.chkAppendUserName.TabIndex = 10;
            this.chkAppendUserName.Text = "Append Username to output directory?";
            this.chkAppendUserName.UseVisualStyleBackColor = true;
            this.chkAppendUserName.CheckedChanged += new System.EventHandler(this.chkAppendUserName_CheckedChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(224, 177);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(33, 23);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Visible = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblVideoDevices
            // 
            this.lblVideoDevices.AutoSize = true;
            this.lblVideoDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVideoDevices.Location = new System.Drawing.Point(15, 71);
            this.lblVideoDevices.Name = "lblVideoDevices";
            this.lblVideoDevices.Size = new System.Drawing.Size(74, 13);
            this.lblVideoDevices.TabIndex = 3;
            this.lblVideoDevices.Text = "Video Device:";
            // 
            // cboVideoDevices
            // 
            this.cboVideoDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVideoDevices.Enabled = false;
            this.cboVideoDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVideoDevices.FormattingEnabled = true;
            this.cboVideoDevices.Location = new System.Drawing.Point(18, 90);
            this.cboVideoDevices.Name = "cboVideoDevices";
            this.cboVideoDevices.Size = new System.Drawing.Size(200, 21);
            this.cboVideoDevices.TabIndex = 4;
            this.cboVideoDevices.SelectionChangeCommitted += new System.EventHandler(this.cboVideoDevices_SelectionChangeCommitted);
            // 
            // lblOutputDir
            // 
            this.lblOutputDir.AutoSize = true;
            this.lblOutputDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputDir.Location = new System.Drawing.Point(16, 160);
            this.lblOutputDir.Name = "lblOutputDir";
            this.lblOutputDir.Size = new System.Drawing.Size(87, 13);
            this.lblOutputDir.TabIndex = 7;
            this.lblOutputDir.Text = "Output Directory:";
            // 
            // cboAudioDevices
            // 
            this.cboAudioDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAudioDevices.Enabled = false;
            this.cboAudioDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAudioDevices.FormattingEnabled = true;
            this.cboAudioDevices.Location = new System.Drawing.Point(18, 135);
            this.cboAudioDevices.Name = "cboAudioDevices";
            this.cboAudioDevices.Size = new System.Drawing.Size(200, 21);
            this.cboAudioDevices.TabIndex = 6;
            this.cboAudioDevices.SelectionChangeCommitted += new System.EventHandler(this.cboAudioDevices_SelectionChangeCommitted);
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Enabled = false;
            this.txtOutputDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputDirectory.Location = new System.Drawing.Point(18, 179);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(200, 20);
            this.txtOutputDirectory.TabIndex = 8;
            this.txtOutputDirectory.TextChanged += new System.EventHandler(this.txtOutputDirectory_TextChanged);
            // 
            // lblAudioDevices
            // 
            this.lblAudioDevices.AutoSize = true;
            this.lblAudioDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudioDevices.Location = new System.Drawing.Point(16, 117);
            this.lblAudioDevices.Name = "lblAudioDevices";
            this.lblAudioDevices.Size = new System.Drawing.Size(74, 13);
            this.lblAudioDevices.TabIndex = 5;
            this.lblAudioDevices.Text = "Audio Device:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(445, 511);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 24;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(775, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutAgreementTracToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutAgreementTracToolStripMenuItem1
            // 
            this.aboutAgreementTracToolStripMenuItem1.Name = "aboutAgreementTracToolStripMenuItem1";
            this.aboutAgreementTracToolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.aboutAgreementTracToolStripMenuItem1.Text = "&About AgreementTrac";
            this.aboutAgreementTracToolStripMenuItem1.Click += new System.EventHandler(this.aboutAgreementTracToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // AgreementTrac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 566);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnNewDeal);
            this.Controls.Add(this.panelDealInfo);
            this.Controls.Add(this.panelVideo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AgreementTrac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AgreementTrac";
            this.panelDealInfo.ResumeLayout(false);
            this.panelDealInfo.PerformLayout();
            this.panelVideo.ResumeLayout(false);
            this.gbConfiguration.ResumeLayout(false);
            this.gbConfiguration.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Button btnNewDeal;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelDealInfo;
        private System.Windows.Forms.Panel panelVideo;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.TextBox txtVinNumber;
        private System.Windows.Forms.TextBox txtStockNumber;
        private System.Windows.Forms.TextBox txtDealerName;
        private System.Windows.Forms.TextBox txtManagerName;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cboVideoDevices;
        private System.Windows.Forms.Label lblAudioDevices;
        private System.Windows.Forms.Label lblVideoDevices;
        private System.Windows.Forms.ComboBox cboAudioDevices;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label lblOutputDir;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.GroupBox gbConfiguration;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel pnlVideoPreview;
        private System.Windows.Forms.TextBox txtDealNumber;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMgrName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkMaint;
        private System.Windows.Forms.CheckBox chkGAP;
        private System.Windows.Forms.CheckBox chkETCH;
        private System.Windows.Forms.CheckBox chkAH;
        private System.Windows.Forms.CheckBox chkVSC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnStartRecording;
        private System.Windows.Forms.Button btnStopRecording;
        private System.Windows.Forms.CheckBox chkAppendUserName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblDealershipName;
        private System.Windows.Forms.TextBox txtDealershipName;
        private System.Windows.Forms.Label lblSQLServer;
        private System.Windows.Forms.TextBox txtSQLServer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSqlUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkUseSQLAuth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Button btnTestSQLConn;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel pnlNewDealPreview;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAgreementTracToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;




    }
}

