using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementTrac
{
    class CVehicleInfo
    {

        // ATVehicleInfo Fields
        private Guid m_guidVehicleIdx;
        private string m_sStockNumber = string.Empty;
        private string m_sMake = string.Empty;
        private string m_sModel = string.Empty;
        private string m_sYear = string.Empty;
        private string m_sVinNumber = string.Empty;

        public CVehicleInfo()
        {
            // Constructor
            m_guidVehicleIdx = Guid.NewGuid();
        }

        #region Public propeties
        
        public Guid VehicleIdx
        {
            get { return m_guidVehicleIdx; }
        }
        public string StockNumber
        {
            get { return m_sStockNumber; }
            set { m_sStockNumber = value; }
        }
        public string Make
        {
            get { return m_sMake; }
            set { m_sMake = value; }
        }
        public string Model
        {
            get { return m_sModel; }
            set { m_sModel = value; }
        }
        public string Year
        {
            get { return m_sYear; }
            set { m_sYear = value; }
        }
        public string VIN
        {
            get { return m_sVinNumber; }
            set { m_sVinNumber = value; }
        }

        #endregion
    }
}
