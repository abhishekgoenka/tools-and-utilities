using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace SysinternalsToolsDownloader.Utilities
{
    /// <summary>
    ///     Provides common methods to download SysinternalsSuite.zip file
    /// </summary>
    public class SysinternalsSuiteDownload : ISysinternalsSuiteDownload
    {
        private const string DOWNLOAD_URL = "https://download.sysinternals.com/files/SysinternalsSuite.zip";
        private readonly string TEMP_FILE_PATH = Path.GetTempPath() + "SysinternalsSuite.zip";

        public DownloadProgressChangedEventHandler DownloadProgressChanged { set; private get; }
        public AsyncCompletedEventHandler DownloadFileCompleted { set; private get; }

        /// <summary>
        ///     Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.
        /// </summary>
        public void DownloadFileAsync()
        {
            using (var wc = new WebClient())
            {
                wc.DownloadProgressChanged += DownloadProgressChanged;
                wc.DownloadFileCompleted += DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri(DOWNLOAD_URL),
                    TEMP_FILE_PATH);
            }
        }

        /// <summary>
        ///     Returns the download file location
        /// </summary>
        public string GetDownloadedFilePath()
        {
            return TEMP_FILE_PATH;
        }
    }

    /// <summary>
    ///     Provides common methods to download SysinternalsSuite.zip file
    /// </summary>
    public interface ISysinternalsSuiteDownload
    {
        /// <summary>
        ///     Occurs when an asynchronous file download operation completes.
        /// </summary>
        AsyncCompletedEventHandler DownloadFileCompleted { set; }

        /// <summary>
        ///     Occurs when an asynchronous download operation successfully transfers some or all of the data.
        /// </summary>
        DownloadProgressChangedEventHandler DownloadProgressChanged { set; }

        /// <summary>
        ///     Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.
        /// </summary>
        void DownloadFileAsync();

        /// <summary>
        ///     Returns the download file location
        /// </summary>
        /// <returns></returns>
        string GetDownloadedFilePath();
    }
}