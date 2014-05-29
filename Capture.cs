using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

using DirectShowLib;

namespace AgreementTrac
{
    class CCapture : IDisposable
    {
        #region Member Fields

        public IFilterGraph2 m_FilterGraph = null;
        
        private ICaptureGraphBuilder2 m_CaptureGraph = null;
        private string m_sCaptureFile = string.Empty;
        
        #endregion

        #region Constructor/Destructor
        public CCapture(DsDevice devVideo, DsDevice devAudio)
        {
            m_FilterGraph = (IFilterGraph2)new FilterGraph();
            m_CaptureGraph = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            SetupGraphForPreview(devVideo, devAudio);
        }
        public CCapture(DsDevice devVideo, DsDevice devAudio, string sOutputFilename)
        {
            m_FilterGraph = (IFilterGraph2)new FilterGraph();
            m_CaptureGraph = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            m_sCaptureFile = sOutputFilename;
            SetupGraphForCapture(devVideo, devAudio, m_sCaptureFile);
        }

        
        ~CCapture()
        {
            Dispose();
        }

        #endregion

        #region Static Methods
        public static DsDevice[] GetVideoDevices()
        {
            DsDevice[] vidDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            if (vidDevices.Length <= 0)
                return null;

            return vidDevices;

        }
        public static DsDevice[] GetAudioDevices()
        {
            DsDevice[] audDevices = DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice);

            if (audDevices.Length <= 0)
                return null;

            return audDevices;
        }
        #endregion
        
        #region Private Methods
        private void SetupGraphForPreview(DsDevice videoDevice, DsDevice audioDevice)
        {
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Entered SetupGraphForPreview() method.");
            int hr = 0;
            string sErrorText = string.Empty;
            IBaseFilter videoCapFilter = null;
            IBaseFilter audioCapFilter = null;
            try
            {
                // start building the graph
                hr = m_CaptureGraph.SetFiltergraph(m_FilterGraph);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);

                // add the video capture device to the graph
                hr = m_FilterGraph.AddSourceFilterForMoniker(videoDevice.Mon, null, videoDevice.Name, out videoCapFilter);
                sErrorText = DsError.GetErrorText(hr);
                if (hr != 0)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tAddSourceFilter failed with message: " + sErrorText);
                    throw new Exception("AddSourceFilterForMoniker() failed with error: " + sErrorText.Trim() +
                        "\r\nVideo source incompatible with graph. Please select a different video device.");
                }
                DsError.ThrowExceptionForHR(hr);

                hr = m_FilterGraph.AddSourceFilterForMoniker(audioDevice.Mon, null, audioDevice.Name, out audioCapFilter);
                sErrorText = DsError.GetErrorText(hr);
                if (hr != 0)
                {
                    Trace.WriteLineIf(Tracer.TracerSwitch.TraceError, "\tAddSourceFilter failed with message: " + sErrorText);
                    throw new Exception("AddSourceFilterForMoniker() failed with error: " + sErrorText.Trim() + 
                        "\r\nAudio source incompatible with graph. Please select a different audio device.");
                }
                
                // Used for preview
                hr = m_CaptureGraph.RenderStream(PinCategory.Preview, MediaType.Video, videoCapFilter, null, null);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);
            }
            finally
            {
                if (videoCapFilter != null)
                {
                    hr = Marshal.ReleaseComObject(videoCapFilter);
                    sErrorText = DsError.GetErrorText(hr);
                    DsError.ThrowExceptionForHR(hr);
                    videoCapFilter = null;
                }
                if (audioCapFilter != null)
                {
                    hr = Marshal.ReleaseComObject(audioCapFilter);
                    sErrorText = DsError.GetErrorText(hr);
                    DsError.ThrowExceptionForHR(hr);
                    audioCapFilter = null;
                }
            }
            Trace.WriteLineIf(Tracer.TracerSwitch.TraceInfo, "Exiting SetupGraphForPreview() method.\r\n");
        }

        private void SetupGraphForCapture(DsDevice video, DsDevice audio, string sOutputFilename)
        {
            int hr = 0;
            string sErrorText = string.Empty;
            IBaseFilter videoCapFilter = null;
            IBaseFilter audioCapFilter = null;
            IBaseFilter asfWriter = null;
            
            try
            {
                // Instantiate the capture graph
                m_CaptureGraph = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

                // start building the graph
                hr = m_CaptureGraph.SetFiltergraph(m_FilterGraph);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);

                // add the video capture device to the graph
                hr = m_FilterGraph.AddSourceFilterForMoniker(video.Mon, null, video.Name, out videoCapFilter);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);

                hr = m_FilterGraph.AddSourceFilterForMoniker(audio.Mon, null, audio.Name, out audioCapFilter);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);

                // Used for preview
                //hr = m_CaptureGraph.RenderStream(PinCategory.Preview, MediaType.Video, videoCapFilter, null, null);
                //sErrorText = DsError.GetErrorText(hr);
                //DsError.ThrowExceptionForHR(hr);

                asfWriter = ConfigAsf(m_CaptureGraph, sOutputFilename);
                hr = m_CaptureGraph.RenderStream(PinCategory.Capture, MediaType.Video, videoCapFilter, null, asfWriter);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);

                hr = m_CaptureGraph.RenderStream(PinCategory.Capture, MediaType.Audio, audioCapFilter, null, asfWriter);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);
            }
            finally
            {
                if (videoCapFilter != null)
                {
                    hr = Marshal.ReleaseComObject(videoCapFilter);
                    sErrorText = DsError.GetErrorText(hr);
                    DsError.ThrowExceptionForHR(hr);
                    videoCapFilter = null;
                }
                if (audioCapFilter != null)
                {
                    hr = Marshal.ReleaseComObject(audioCapFilter);
                    sErrorText = DsError.GetErrorText(hr);
                    DsError.ThrowExceptionForHR(hr);
                    audioCapFilter = null;
                }
                if (asfWriter != null)
                {
                    hr = Marshal.ReleaseComObject(asfWriter);
                    sErrorText = DsError.GetErrorText(hr);
                    DsError.ThrowExceptionForHR(hr);
                    asfWriter = null;
                }
            }
        }

        private IBaseFilter ConfigAsf(ICaptureGraphBuilder2 captureGraph, string sOutputFilename)
        {
            IFileSinkFilter pTmpSink = null;
            IBaseFilter asfWriter = null;

            int hr = captureGraph.SetOutputFileName(MediaSubType.Asf, sOutputFilename, out asfWriter, out pTmpSink);
            string sErrorText = DsError.GetErrorText(hr);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                IConfigAsfWriter lConfig = asfWriter as IConfigAsfWriter;
                
                // Following profiles found in C:\Windows\WMSysPr9.prx
                // Video for LAN, cable modem, or xDSL (100 to 768 Kbps)
                // Approximately 10MB/min
                //Guid cat = new Guid(0x045880DC, 0x34B6, 0x4CA9, 0xA3, 0x26, 0x73, 0x55, 0x7E, 0xD1, 0x43, 0xF3);
                
                //Video for dial-up modems or LAN (28.8 to 100 Kbps)" 
                // Approximately 1MB/min
                //guid="{07DF7A25-3FE2-4A5B-8B1E-348B0721CA70}"

                //Guid cat = new Guid(0x07DF7A25,0x3FE2,0x4A5B,0x8B,0x1E,0x34,0x8B,0x07,0x21,0xCA,0x70);
                
                //Video for e-mail and dual-channel ISDN (128 Kbps)" 
                //guid="{D9F3C932-5EA9-4C6D-89B4-2686E515426E}"
                // Approximately 800KB/min (audio is mono)
                Guid cat = new Guid(0xD9F3C932, 0x5EA9, 0x4C6D, 0x89, 0xB4, 0x26, 0x86, 0xE5, 0x15, 0x42, 0x6E);

                hr = lConfig.ConfigureFilterUsingProfileGuid(cat);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);

            }
            finally
            {
                hr = Marshal.ReleaseComObject(pTmpSink);
                sErrorText = DsError.GetErrorText(hr);
                DsError.ThrowExceptionForHR(hr);
            }
            return asfWriter;
        }

        private void CloseInterfaces()
        {
            try
            {
                int hr = 0;
                string sErr = string.Empty;
                if (m_FilterGraph != null)
                {
                    hr = Marshal.FinalReleaseComObject(m_FilterGraph);
                    sErr = DsError.GetErrorText(hr);
                    DsError.ThrowExceptionForHR(hr);
                    m_FilterGraph = null;
                }
                if (m_CaptureGraph != null)
                {
                    hr = Marshal.FinalReleaseComObject(m_CaptureGraph);
                    sErr = DsError.GetErrorText(hr);
                    DsError.ThrowExceptionForHR(hr);
                    m_CaptureGraph = null;
                }
                
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            CloseInterfaces();
        }
        #endregion
        

        
    }
}
