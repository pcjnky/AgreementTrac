using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;

namespace AgreementTrac
{
    class CDealInfo
    {
        // ATDealInfo Fields
        private SqlConnection m_SqlConn = null;
        private Guid m_guidDealIdx;
        private string m_sComputerName = string.Empty;
        private string m_sIPAddress = string.Empty;
        private string m_sManagerName = string.Empty;
        private string m_sDealNumber = string.Empty;
        private string m_sDealershipName = string.Empty;
        private DateTime m_dtTransactionDate;
        private bool m_bVscChecked = false;
        private bool m_bAHChecked = false;
        private bool m_bEtchChecked = false;
        private bool m_bGapChecked = false;
        private bool m_bMaintChecked = false;
        private string m_sNotes = string.Empty;

        public CCustInfo customerInfo = null;
        public CVehicleInfo vehicleInfo = null;
        public CVideoInfo videoInfo = null;
        
        public CDealInfo(string sConnString, string sManagerName, string sDatabaseName)
        {
            m_guidDealIdx = Guid.NewGuid();
            m_sManagerName = sManagerName;
            m_SqlConn = new SqlConnection(sConnString);

            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tCreating CCustInfo object.");
            customerInfo = new CCustInfo();
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tCCustInfo object created successfully.");
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tCreating CVehicle object.");
            vehicleInfo = new CVehicleInfo();
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tCVehicle object created successfully.");
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tCreating CVideoInfo object.");
            videoInfo = new CVideoInfo();
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tCVideoInfo object created successfully.");
        }

        public string ExecuteSQLInserts(string sDatabaseName)
        {
            string sResult = string.Empty;
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered ExecuteSQLInserts() method.");
            try
            {
                // Open the connection and switch to correct database
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tOpen SQL Connection.");
                m_SqlConn.Open();
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tSQL Connection opened successfully.");
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tChanging database context to :" + sDatabaseName);
                m_SqlConn.ChangeDatabase(sDatabaseName);
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tDatabase change successful.");

                // ATCustInfo Insert
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tStarting Insert to ATCustInfo table.");
                string strInsertQuery;
                strInsertQuery = "INSERT INTO ATCustInfo (CustomerIdx,FName,LName,Address,City,State,Zip,Phone)\n" +
                    "VALUES(@CustomerIdx, @FName, @LName, @Address, @City, @State, @Zip, @Phone);";

                SqlCommand sqlCommand = new SqlCommand(strInsertQuery, m_SqlConn);
                
                // Add the parameters
                sqlCommand.Parameters.AddWithValue("@CustomerIdx", customerInfo.CustomerIdx);
                sqlCommand.Parameters.AddWithValue("@FName", customerInfo.FirstName);
                sqlCommand.Parameters.AddWithValue("@LName", customerInfo.LastName);
                sqlCommand.Parameters.AddWithValue("@Address", customerInfo.Address);
                sqlCommand.Parameters.AddWithValue("@City", customerInfo.City);
                sqlCommand.Parameters.AddWithValue("@State", customerInfo.State);
                sqlCommand.Parameters.AddWithValue("@Zip", customerInfo.Zip);
                sqlCommand.Parameters.AddWithValue("@Phone", customerInfo.Phone);
                sqlCommand.ExecuteScalar();     // execute the query
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tInsert to ATCustInfo table successful.");
                
                // ATVehicleInfo Insert
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tStarting Insert to ATVehicleInfo table.");
                sqlCommand.CommandText = "INSERT INTO ATVehicleInfo(VehicleIdx,StockNo,Make,Model,VehYear,Vin)\n" +
                    "VALUES(@VehicleIdx, @StockNo, @Make, @Model, @VehYear, @Vin);";

                sqlCommand.Parameters.AddWithValue("@VehicleIdx", vehicleInfo.VehicleIdx);
                sqlCommand.Parameters.AddWithValue("@StockNo", vehicleInfo.StockNumber);
                sqlCommand.Parameters.AddWithValue("@Make", vehicleInfo.Make);
                sqlCommand.Parameters.AddWithValue("@Model", vehicleInfo.Model);
                sqlCommand.Parameters.AddWithValue("@VehYear", vehicleInfo.Year);
                sqlCommand.Parameters.AddWithValue("@Vin", vehicleInfo.VIN);
                sqlCommand.ExecuteNonQuery();       // execute the query
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tInsert to ATVehicleInfo table successful.");
                
                // ATVideoInfo Insert
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tStarting Insert to ATVideoInfo table.");
                sqlCommand.CommandText = "INSERT INTO ATVideoInfo(VideoIdx, vidFileLength, vidSaveDir, vidFileName, vidFileSize)\n" +
                    "VALUES(@VideoIdx, @vidFileLength, @vidSaveDir, @vidFileName, @vidFileSize);";

                sqlCommand.Parameters.AddWithValue("@VideoIdx", videoInfo.VideoIdx);
                sqlCommand.Parameters.AddWithValue("@vidFileLength", videoInfo.VidFileLength);
                sqlCommand.Parameters.AddWithValue("@vidSaveDir", videoInfo.VidSaveDir);
                sqlCommand.Parameters.AddWithValue("@vidFileName", videoInfo.VidFileName);
                sqlCommand.Parameters.AddWithValue("@vidFileSize", videoInfo.VidFileSize);
                sqlCommand.ExecuteNonQuery();       // execute the query
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tInsert to ATVideoInfo table successful.");

                // ATDealInfo Insert
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tStarting Insert to ATDealInfo table.");
                sqlCommand.CommandText = "INSERT INTO ATDealInfo(DealIdx, ComputerName, DealershipName, TransactionDate, IPAddress, MgrName, CustomerIdx, VideoIdx, VehicleIdx, DealNumber,\n" +
                    "VSC, AH, ETCH, GAP, MAINT, Notes) VALUES (@DealIdx, @ComputerName, @DealershipName, @TransactionDate, @IPAddress, @MgrName, @CustomerIdxFK, @VideoIdxFK, @VehicleIdxFK,\n" +
                    "@DealNumber, @VSC, @AH, @ETCH, @GAP, @MAINT, @Notes);";

                sqlCommand.Parameters.AddWithValue("@DealIdx", this.m_guidDealIdx);
                sqlCommand.Parameters.AddWithValue("@ComputerName", this.ComputerName);
                sqlCommand.Parameters.AddWithValue("@DealershipName", this.DealershipName);
                sqlCommand.Parameters.AddWithValue("@TransactionDate", this.TransactionDate);
                sqlCommand.Parameters.AddWithValue("@IPAddress", this.IP_Address);
                sqlCommand.Parameters.AddWithValue("@MgrName", this.m_sManagerName);
                sqlCommand.Parameters.AddWithValue("@CustomerIdxFK", customerInfo.CustomerIdx);
                sqlCommand.Parameters.AddWithValue("@VideoIdxFK", videoInfo.VideoIdx);
                sqlCommand.Parameters.AddWithValue("@VehicleIdxFK", vehicleInfo.VehicleIdx);
                sqlCommand.Parameters.AddWithValue("@DealNumber", this.DealNumber);
                sqlCommand.Parameters.AddWithValue("@VSC", Convert.ToInt32(this.VSC));
                sqlCommand.Parameters.AddWithValue("@AH", Convert.ToInt32(this.AH));
                sqlCommand.Parameters.AddWithValue("@ETCH", Convert.ToInt32(this.ETCH));
                sqlCommand.Parameters.AddWithValue("@GAP", Convert.ToInt32(this.GAP));
                sqlCommand.Parameters.AddWithValue("@MAINT", Convert.ToInt32(this.MAINT));
                sqlCommand.Parameters.AddWithValue("@Notes", this.Notes);

                sqlCommand.ExecuteNonQuery();
                sResult = "Save Successful.";
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "\tInsert to ATDealInfo table successful.");
            }
            catch (Exception ex)
            {
                Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tError occurred during inserts. Message text: " + ex.Message);
                sResult = ex.Message;
            }
            finally
            {
                m_SqlConn.Close();
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting ExecuteSQLInserts() method.");
            return sResult;
        }
        #region Public Properties

        public string DealNumber
        {
            get { return m_sDealNumber; }
            set { m_sDealNumber = value; }
        }
        public string DealershipName
        {
            get { return m_sDealershipName; }
            set { m_sDealershipName = value; }
        }
        public DateTime TransactionDate
        {
            get { return m_dtTransactionDate; }
            set { m_dtTransactionDate = value; }
        }
        public string Notes
        {
            get { return m_sNotes; }
            set { m_sNotes = value; }
        }
        public bool VSC
        {
            get { return m_bVscChecked; }
            set { m_bVscChecked = value; }
        }
        public bool AH
        {
            get { return m_bAHChecked; }
            set { m_bAHChecked = value; }
        }
        public bool ETCH
        {
            get { return m_bEtchChecked; }
            set { m_bEtchChecked = value; }
        }
        public bool GAP
        {
            get { return m_bGapChecked; }
            set { m_bGapChecked = value; }
        }
        public bool MAINT
        {
            get { return m_bMaintChecked; }
            set { m_bMaintChecked = value; }
        }
        // System Information properties
        public string ComputerName
        {
            get { return m_sComputerName; }
            set { m_sComputerName = value; }
        }
        public string IP_Address
        {
            get { return m_sIPAddress; }
            set { m_sIPAddress = value; }
        }
        
        #endregion

    }
}
