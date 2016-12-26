using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Input;
using SysinternalsToolsDownloader.Utilities;

namespace SysinternalsToolsDownloader.ViewModel
{
    /// <summary>
    ///     A ViewModel class that is bind with MainWindow
    /// </summary>
    public class Downloader : INotifyPropertyChanged
    {
        private const string DOWNLOAD_URL = "https://download.sysinternals.com/files/SysinternalsSuite.zip";
        private readonly string TEMP_FILE_PATH = Path.GetTempPath() + "SysinternalsSuite.zip";
        private string _diagMessage;
        private bool _isDownloadButtonEnabled;
        private int _lastProgressPercentage;

        public Downloader()
        {
            IsExtractToCurrentDirectorySelected = true;
            IsDownloadButtonEnabled = true;
        }

        /// <summary>
        ///     TRUE if "Added folder to system path variable" checkbox is checked
        /// </summary>
        public bool IsAddToSystemPathSelected { get; set; }

        /// <summary>
        ///     TRUE if "Current Directory" is selected
        /// </summary>
        public bool IsExtractToCurrentDirectorySelected { get; set; }

        /// <summary>
        ///     TRUE if "C:\Program Files" is selected
        /// </summary>
        public bool IsExtractToProgramFilesSelected { get; set; }

        /// <summary>
        ///     Enable/Disable Download button
        /// </summary>
        public bool IsDownloadButtonEnabled
        {
            get { return _isDownloadButtonEnabled; }
            set
            {
                _isDownloadButtonEnabled = value;
                RaisePropertyChanged("IsDownloadButtonEnabled");
            }
        }

        /// <summary>
        ///     Diagnosis logs
        /// </summary>
        public string DiagMessage
        {
            get { return _diagMessage; }
            set
            {
                _diagMessage = value;
                RaisePropertyChanged("DiagMessage");
            }
        }

        /// <summary>
        ///     Command to start downloading
        /// </summary>
        public ICommand StartDownloadCommand => new DelegateCommand<string>(e => OnStartDownloadCommand());

        #region Private Properties/Methods

        private void OnStartDownloadCommand()
        {
            Log("Downloading started...");
            IsDownloadButtonEnabled = false;

            using (var wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri(DOWNLOAD_URL),
                    TEMP_FILE_PATH);
            }
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Log($"File downloaded successfully at {TEMP_FILE_PATH}");
            IsDownloadButtonEnabled = true;

            if (IsExtractToCurrentDirectorySelected)
                Decompress(TEMP_FILE_PATH, Directory.GetCurrentDirectory());
            else if (IsExtractToProgramFilesSelected)
                Decompress(TEMP_FILE_PATH, Directory.GetCurrentDirectory());
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (_lastProgressPercentage != e.ProgressPercentage)
            {
                Log($"{e.ProgressPercentage} percentage completed…");
                _lastProgressPercentage = e.ProgressPercentage;
            }
        }

        public void Decompress(string sourceArchiveFileName, string destinationDirectoryName)
        {
            destinationDirectoryName += "\\Sysinternals";

            if (Directory.Exists(destinationDirectoryName))
            {
                Log($"{destinationDirectoryName} directory already exists!!!");
                return;
            }
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
            Log($"Utilities extracted to {destinationDirectoryName}");


            if (IsAddToSystemPathSelected)
                SetEnvironmentVariablePath(destinationDirectoryName);
        }

        private void SetEnvironmentVariablePath(string directoryPath)
        {
            var environmentVariable = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
            if (environmentVariable != null)
            {
                var path = environmentVariable.Split(';');
                if (path.Any(e => string.Compare(e, directoryPath, StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    Log("Directory path already exists in system path");
                    return;
                }

                environmentVariable += ";" + directoryPath;
                Environment.SetEnvironmentVariable("Path", environmentVariable, EnvironmentVariableTarget.Machine);
            }
        }

        private void Log(string message)
        {
            if (string.IsNullOrEmpty(DiagMessage)) DiagMessage = message;
            else DiagMessage += Environment.NewLine + message;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}