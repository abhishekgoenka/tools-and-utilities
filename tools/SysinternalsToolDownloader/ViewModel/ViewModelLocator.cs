using SysinternalsToolsDownloader.Utilities;

namespace SysinternalsToolsDownloader.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Returns instance of Downloader view model
        /// </summary>
        public DownloaderViewModel Main => new DownloaderViewModel(new SysinternalsSuiteDownload());
    }
}