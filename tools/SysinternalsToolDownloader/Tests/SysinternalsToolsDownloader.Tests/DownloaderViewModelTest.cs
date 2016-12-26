using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SysinternalsToolsDownloader.Utilities;
using SysinternalsToolsDownloader.ViewModel;
namespace SysinternalsToolsDownloader.Tests
{
    [TestClass]
    public class DownloaderViewModelTest
    {
        private Mock<ISysinternalsSuiteDownload> mockSysinternalsSuiteDownload;

        [TestInitialize]
        public void Init()
        {
            mockSysinternalsSuiteDownload = new Mock<ISysinternalsSuiteDownload>();
        }

        [TestMethod]
        public void DownloaderViewModel_Should_Implement_INotifyPropertyChanged()
        {
            //arrange
            DownloaderViewModel viewModel = new DownloaderViewModel(mockSysinternalsSuiteDownload.Object);

            //act
            INotifyPropertyChanged iNotifyPropertyChanged = viewModel;

            //assert
            Assert.IsNotNull(iNotifyPropertyChanged);
        }

        [TestMethod]
        public void DownloaderViewModel_Default_Value_Test()
        {
            //arrange
            DownloaderViewModel viewModel = new DownloaderViewModel(mockSysinternalsSuiteDownload.Object);

            //act

            //assert
            Assert.IsFalse(viewModel.IsAddToSystemPathSelected);
            Assert.IsTrue(viewModel.IsDownloadButtonEnabled);
            Assert.IsTrue(viewModel.IsExtractToCurrentDirectorySelected);
            Assert.IsFalse(viewModel.IsExtractToProgramFilesSelected);
        }

        [TestMethod]
        public void DownloaderViewModel_StartDownload_Command_Test()
        {
            //arrange
            DownloaderViewModel viewModel = new DownloaderViewModel(mockSysinternalsSuiteDownload.Object);

            //act
            ICommand command = viewModel.StartDownloadCommand;
            command.Execute("Test");

            //assert
            mockSysinternalsSuiteDownload.Verify(e => e.DownloadFileAsync());
        }
    }
}
