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
    public class DownloaderViewModel : INotifyPropertyChanged
    {
        private readonly ISysinternalsSuiteDownload _sysinternalsSuiteDownload;


        private string _diagMessage;
        private bool _isDownloadButtonEnabled;
        private int _lastProgressPercentage;

        public DownloaderViewModel(ISysinternalsSuiteDownload sysinternalsSuiteDownload)
        {
            _sysinternalsSuiteDownload = sysinternalsSuiteDownload;
            _sysinternalsSuiteDownload.DownloadProgressChanged = wc_DownloadProgressChanged;
            _sysinternalsSuiteDownload.DownloadFileCompleted = Wc_DownloadFileCompleted;

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

            _sysinternalsSuiteDownload.DownloadFileAsync();
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Log($"File downloaded successfully at {_sysinternalsSuiteDownload.GetDownloadedFilePath()}");
            IsDownloadButtonEnabled = true;

            if (IsExtractToCurrentDirectorySelected)
                Decompress(_sysinternalsSuiteDownload.GetDownloadedFilePath(), Directory.GetCurrentDirectory());
            else if (IsExtractToProgramFilesSelected)
                Decompress(_sysinternalsSuiteDownload.GetDownloadedFilePath(), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
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