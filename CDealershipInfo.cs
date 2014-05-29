using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementTrac
{
    class CDealershipInfo
    {
        // ATDealershipInfo Fields
        private Guid m_guidDealershipIdx;
        private string m_sDealershipName = string.Empty;

        public CDealershipInfo()
        {
            m_guidDealershipIdx = Guid.NewGuid();
        }

        public Guid DealershipIdx
        {
            get { return m_guidDealershipIdx; }
        }

        public string DealershipName
        {
            get { return m_sDealershipName; }
            set { m_sDealershipName = value; }
        }
    }
}
