using System;

namespace SymbolPath.Console.Utilities
{
    /// <summary>
    ///     Retrieves and Set the value of an environment variable from the current process or from the Windows operating
    ///     system registry key for the current user or local machine.
    /// </summary>
    public class EnvironmentVariable : IEnvironmentVariable
    {
        /// <summary>
        ///     Retrieves the value of an environment variable from the current process or from the Windows operating system
        ///     registry key for the current user or local machine.
        /// </summary>
        /// <param name="variable">The name of an environment variable.</param>
        /// <param name="environmentVariableTarget">The target environment</param>
        /// <returns></returns>
        public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget environmentVariableTarget)
        {
            return Environment.GetEnvironmentVariable(variable, environmentVariableTarget);
        }

        /// <summary>
        ///     Creates, modifies, or deletes an environment variable stored in the current process.
        /// </summary>
        /// <param name="variable">The name of an environment variable.</param>
        /// <param name="value">A value to assign to</param>
        public void SetEnvironmentVariable(string variable, string value)
        {
            Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.Machine);
        }
    }
}