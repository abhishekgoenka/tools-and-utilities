namespace SymbolPath.Console.Utilities
{
    /// <summary>
    ///     A simple logger
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        ///     Write log
        /// </summary>
        /// <param name="msg">Message</param>
        public void Log(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}