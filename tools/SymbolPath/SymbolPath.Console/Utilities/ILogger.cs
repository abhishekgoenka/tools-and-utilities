namespace SymbolPath.Console.Utilities
{
    /// <summary>
    ///     A simple logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Write log
        /// </summary>
        /// <param name="msg">Message</param>
        void Log(string msg);
    }
}