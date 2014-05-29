using System.Diagnostics;
using System.Security.Permissions;
using System.IO;
using System.Security.AccessControl;

namespace AgreementTrac
{
    public static class Tracer
    {

        #region Private Fields

        private static System.IO.FileStream m_fsMyTraceLog = null;
        private static System.Diagnostics.TextWriterTraceListener m_AgreementTracListener = null;
        private static TraceSwitch m_swCriticalTraceSwitch = null;
        
        #endregion

        //[FileIOPermission(SecurityAction.Assert)]
        [FileIOPermissionAttribute(SecurityAction.Demand, Unrestricted = true)]
        public static void SetupTracing(string path)
        {
            string sUserProfileDir = System.Environment.ExpandEnvironmentVariables("%allusersprofile%");

            sUserProfileDir = sUserProfileDir + System.IO.Path.DirectorySeparatorChar + "AgreementTrac";

            Debug.Print("Users Profile: " + sUserProfileDir);
            string sFilePath = sUserProfileDir + System.IO.Path.DirectorySeparatorChar + "agreement_trac_log.txt";
            //string sFilePath = path + System.IO.Path.DirectorySeparatorChar + "agreement_trac_log.txt";
            FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, sFilePath);
            try
            {
                //Tracer.m_fsMyTraceLog = new System.IO.FileStream(path + System.IO.Path.DirectorySeparatorChar + "agreement_trac_log.txt", System.IO.FileMode.Create);
                if (!System.IO.Directory.Exists(sUserProfileDir))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(sUserProfileDir);
                        // Create a new DirectoryInfo object.
                        DirectoryInfo dInfo = new DirectoryInfo(sUserProfileDir);

                        // Get a DirectorySecurity object that represents the 
                        // current security settings.
                        DirectorySecurity dSecurity = dInfo.GetAccessControl();

                        // Add the FileSystemAccessRule to the security settings. 
                        dSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.Modify, InheritanceFlags.ObjectInherit, 
                            PropagationFlags.InheritOnly, AccessControlType.Allow));

                        // Set the new access settings.
                        dInfo.SetAccessControl(dSecurity);
                        
                    }
                    catch (System.Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }
                }
                Tracer.m_fsMyTraceLog = new System.IO.FileStream(sFilePath, System.IO.FileMode.Create);
                Tracer.m_AgreementTracListener = new TextWriterTraceListener(Tracer.m_fsMyTraceLog);
            
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            Trace.Listeners.Add(Tracer.m_AgreementTracListener);
            m_swCriticalTraceSwitch = new TraceSwitch("GeneralTrace", "Use for entire application");
        }

        public static TraceSwitch TracerSwitch
        {
            get { return m_swCriticalTraceSwitch; }
        }
    }
}
