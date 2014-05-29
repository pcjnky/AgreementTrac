using System.Reflection;

namespace AgreementTrac
{
    class ConfigSettings
    {
        string m_sDealershipName = string.Empty;
        string m_sVideoDevice = string.Empty;
        string m_sAudioDevice = string.Empty;
        string m_sOutputDirectory = string.Empty;
        string m_sSQLServer = string.Empty;
        string m_sSqlUserName = string.Empty;
        string m_sSqlPassword = string.Empty;
        string m_sSqlConnString = string.Empty;
        string m_sDatabaseName = string.Empty;
        bool m_bAppendUsernameChecked = false;
        bool m_bUseSQLAuth = false;

        public ConfigSettings()
        {
            // default constructor
        }

        public ConfigSettings( ConfigSettings rhs )
        {
            // Copy constructor
            // get all the fields in the class
            FieldInfo[] fields_of_class = this.GetType().GetFields(
              BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );

            // copy each value over to 'this'
            foreach( FieldInfo fi in fields_of_class )
            {
                fi.SetValue( this, fi.GetValue( rhs ) );
            }
        }
        
        public override bool Equals(object obj)
        {
            bool bEqual = false;
            if (this.m_sDealershipName == ((ConfigSettings)obj).m_sDealershipName &&
                this.m_bAppendUsernameChecked == ((ConfigSettings)obj).m_bAppendUsernameChecked &&
                this.m_bUseSQLAuth == ((ConfigSettings)obj).m_bUseSQLAuth &&
                this.m_sAudioDevice == ((ConfigSettings)obj).m_sAudioDevice &&
                this.m_sOutputDirectory == ((ConfigSettings)obj).m_sOutputDirectory &&
                this.m_sSqlConnString == ((ConfigSettings)obj).m_sSqlConnString &&
                this.m_sSqlPassword == ((ConfigSettings)obj).m_sSqlPassword &&
                this.m_sSQLServer == ((ConfigSettings)obj).m_sSQLServer &&
                this.m_sSqlUserName == ((ConfigSettings)obj).m_sSqlUserName &&
                this.m_sDatabaseName == ((ConfigSettings)obj).m_sDatabaseName &&
                this.m_sVideoDevice == ((ConfigSettings)obj).m_sVideoDevice)
            {
                bEqual = true;
            }
            else
            {
                bEqual = false;
            }
            return bEqual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region Properties

        public string Dealership
        {
            get { return m_sDealershipName; }
            set { m_sDealershipName = value; }
        }
        public string VideoDevice
        {
            get { return m_sVideoDevice; }
            set { m_sVideoDevice = value; }
        }
        public string AudioDevice
        {
            get { return m_sAudioDevice; }
            set { m_sAudioDevice = value; }
        }
        public string OutputDirectory
        {
            get { return m_sOutputDirectory; }
            set { m_sOutputDirectory = value; }
        }
        public string SqlServer
        {
            get { return m_sSQLServer; }
            set { m_sSQLServer = value; }
        }
        public string DatabaseName
        {
            get { return m_sDatabaseName; }
            set { m_sDatabaseName = value; }
        }
        public string SQLUserName
        {
            get { return m_sSqlUserName; }
            set { m_sSqlUserName = value; }
        }
        public string Password
        {
            get { return m_sSqlPassword; }
            set { m_sSqlPassword = value; }
        }
        public string SQLConnectionString
        {
            get { return m_sSqlConnString; }
            set { m_sSqlConnString = value; }
        }
        public bool UseSQLAuthentication
        {
            get { return m_bUseSQLAuth; }
            set { m_bUseSQLAuth = value; }
        }
        public bool AppendUserName
        {
            get { return m_bAppendUsernameChecked; }
            set { m_bAppendUsernameChecked = value; }
        }
        #endregion

        
    }
}
