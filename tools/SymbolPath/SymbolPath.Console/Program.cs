using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SymbolPath.Console.Utilities;

namespace SymbolPath.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Show header message to Console
            PrintHeaderMessage();

            SymPath symPath = new SymPath(new EnvironmentVariable(), new Logger());
            symPath.SetSymPath();

        }

        static void PrintHeaderMessage()
        {
            System.Console.WriteLine("SymbolePath v1.1 - Diagnosis utility");
            System.Console.WriteLine("Copyright (C) 2015-2016 Abhishek Goenka");
            System.Console.WriteLine();
            System.Console.WriteLine("Sets symbol search path in _NT_SYMBOL_PATH environment variable");
            System.Console.WriteLine();
            System.Console.WriteLine("Capture Usage:");
            System.Console.WriteLine("\t symbolPath.exe [-debug]");
            System.Console.WriteLine();
            System.Console.WriteLine("Options");
            System.Console.WriteLine("\t -debug\t Shows all debug logs");
            System.Console.WriteLine();
            System.Console.WriteLine();
        }
    }
}
