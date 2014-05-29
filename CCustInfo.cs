using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementTrac
{
    class CCustInfo
    {
        // ATCustInfo Fields
        private Guid m_guidCustomerIdx;
        private string m_sFirstName = string.Empty;
        private string m_sLastName = string.Empty;
        
        private string m_sAddress = string.Empty;   // not implemented yet
        private string m_sCity = string.Empty;      // not implemented yet
        private string m_sState = string.Empty;     // not implemented yet
        private string m_sZip = string.Empty;       // not implemented yet
        private string m_sPhone = string.Empty;     // not implemented yet

        public CCustInfo()
        {
            // Constructor
            m_guidCustomerIdx = Guid.NewGuid();
        }

        #region public properties

        public Guid CustomerIdx
        {
            get { return m_guidCustomerIdx; }
        }
        public string FirstName
        {
            get { return m_sFirstName; }
            set { m_sFirstName = value; }
        }
        public string LastName
        {
            get { return m_sLastName; }
            set { m_sLastName = value; }
        }
        public string Address
        {
            get { return m_sAddress; }
            set { m_sAddress = value; }
        }
        public string City
        {
            get { return m_sCity; }
            set { m_sCity = value; }
        }
        public string State
        {
            get { return m_sState; }
            set { m_sState = value; }
        }
        public string Zip
        {
            get { return m_sZip; }
            set { m_sZip = value; }
        }
        public string Phone
        {
            get { return m_sPhone; }
            set { m_sPhone = value; }
        }

        #endregion
    }
}
