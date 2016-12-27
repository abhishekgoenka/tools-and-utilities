using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SymbolPath.Console;
using SymbolPath.Console.Utilities;

namespace SymbolPath.Tests
{
    [TestClass]
    public class SymPathTests
    {
        private const string NT_SYMBOL_VARIABLE_NAME = "_NT_SYMBOL_PATH";
        private const string NT_SYMBOL_PATH = @"srv*c:\symbols*http://msdl.microsoft.com/download/symbols";
        private Mock<IEnvironmentVariable> mock_environmentVariable;
        private Mock<ILogger> mock_Logger;

        [TestInitialize]
        public void Init()
        {
            mock_environmentVariable = new Mock<IEnvironmentVariable>();
            mock_Logger = new Mock<ILogger>();

        }

        [TestMethod]
        public void SetSymPath_For_CurrentUser_Should_Return_Already_Exists()
        {
            //arrange
            SymPath symPath = new SymPath(mock_environmentVariable.Object, mock_Logger.Object);
            mock_environmentVariable.Setup(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.User)).Returns(NT_SYMBOL_PATH);

            //act
            symPath.SetSymPath();

            //assert
            mock_environmentVariable.Verify(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.User), Times.Once);
            mock_environmentVariable.Verify(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.Machine), Times.Never);
            mock_environmentVariable.Verify(e => e.SetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, NT_SYMBOL_PATH), Times.Never);
        }

        [TestMethod]
        public void SetSymPath_For_Machine_Should_Return_Already_Exists()
        {
            //arrange
            SymPath symPath = new SymPath(mock_environmentVariable.Object, mock_Logger.Object);
            mock_environmentVariable.Setup(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.User)).Returns(String.Empty);
            mock_environmentVariable.Setup(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.Machine)).Returns(NT_SYMBOL_PATH);

            //act
            symPath.SetSymPath();

            //assert
            mock_environmentVariable.Verify(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.User), Times.Once);
            mock_environmentVariable.Verify(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.Machine), Times.Once);
            mock_environmentVariable.Verify(e => e.SetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, NT_SYMBOL_PATH), Times.Never);
        }

        [TestMethod]
        public void SetSymPath_Should_Return_Successful()
        {
            //arrange
            SymPath symPath = new SymPath(mock_environmentVariable.Object, mock_Logger.Object);
            mock_environmentVariable.Setup(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.User)).Returns(String.Empty);
            mock_environmentVariable.Setup(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.Machine)).Returns(String.Empty);

            //act
            symPath.SetSymPath();

            //assert
            mock_environmentVariable.Verify(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.User), Times.Once);
            mock_environmentVariable.Verify(e => e.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, EnvironmentVariableTarget.Machine), Times.Once);
            mock_environmentVariable.Verify(e => e.SetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, NT_SYMBOL_PATH), Times.Once);

        }
    }
}