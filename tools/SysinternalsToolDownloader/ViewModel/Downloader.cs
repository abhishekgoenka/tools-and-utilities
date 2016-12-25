using System;
using System.ComponentModel;
using System.Windows.Input;
using SysinternalsToolsDownloader.Utilities;

namespace SysinternalsToolsDownloader.ViewModel
{
    /// <summary>
    ///     A ViewModel class that is bind with MainWindow
    /// </summary>
    public class Downloader : INotifyPropertyChanged
    {
        private string _diagMessage;

        public Downloader()
        {
            IsExtractToCurrentDirectorySelected = true;
        }

        /// <summary>
        ///     TRUE if "Added folder to system path variable" checkbox is checked
        /// </summary>
        public bool IsAddToSystemPathSelected { get; set; }

        public bool IsExtractToCurrentDirectorySelected { get; set; }

        public bool IsExtractToProgramFilesSelected { get; set; }

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
        }


        private void Log(string message)
        {
            DiagMessage += Environment.NewLine + message;
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