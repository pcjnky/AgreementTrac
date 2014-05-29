using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementTrac
{
    class CVideoInfo
    {
        // ATVideoInfo Fields
        private Guid m_guidVideoIdx;
        private string m_sVidSaveDir = string.Empty;
        private int m_nVidFileLength = 0;
        private string m_sVidFileName = string.Empty;
        private long m_nFileSize = 0;

        public CVideoInfo()
        {
            m_guidVideoIdx = Guid.NewGuid();
        }

        public Guid VideoIdx
        {
            get { return m_guidVideoIdx; }
        }
        public string VidSaveDir
        {
            get { return m_sVidSaveDir; }
            set { m_sVidSaveDir = value; }
        }
        public int VidFileLength
        {
            get { return m_nVidFileLength; }
            set { m_nVidFileLength = value; }
        }
        public string VidFileName
        {
            get { return m_sVidFileName; }
            set { m_sVidFileName = value; }
        }
        public long VidFileSize
        {
            get { return m_nFileSize; }
            set { m_nFileSize = value; }
        }
    }
}
