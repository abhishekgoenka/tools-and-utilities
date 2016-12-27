using System;
using System.Linq;
using SymbolPath.Console.Utilities;

namespace SymbolPath.Console
{
    /// <summary>
    ///     Sets symbol search path in _NT_SYMBOL_PATH environment variable
    /// </summary>
    public class SymPath
    {
        private const string NT_SYMBOL_VARIABLE_NAME = "_NT_SYMBOL_PATH";
        private const string NT_SYMBOL_PATH = @"srv*c:\symbols*http://msdl.microsoft.com/download/symbols";
        private readonly IEnvironmentVariable _environmentVariable;
        private readonly ILogger _logger;

        public SymPath(IEnvironmentVariable environmentVariable, ILogger logger)
        {
            _environmentVariable = environmentVariable;
            _logger = logger;
        }

        /// <summary>
        /// Set Symbol path
        /// </summary>
        public void SetSymPath()
        {
            //get symbol path for current user
            String currentUserSymPath = _environmentVariable.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME,
                EnvironmentVariableTarget.User);
            if (IsSymbolPathSet(currentUserSymPath))
            {
                //symbol path not set for current user
                String machineSymPath = _environmentVariable.GetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME,
                EnvironmentVariableTarget.Machine);
                if (IsSymbolPathSet(machineSymPath))
                {
                    //symbol path not set for machine. Set symbole path
                    _environmentVariable.SetEnvironmentVariable(NT_SYMBOL_VARIABLE_NAME, NT_SYMBOL_PATH);
                    System.Console.WriteLine("Symbol path set successfully");
                }
                else
                {
                    System.Console.WriteLine("Symbol path already set for machine");
                }
            }
            else
            {
                System.Console.WriteLine("Symbol path already set for current user");
            }
        }

        private Boolean IsSymbolPathSet(String path)
        {
            return string.IsNullOrEmpty(path);
        }
    }
}