# Sysinternals Tools Downloader
This utility downloads the all the tools from [Sysinternals](https://technet.microsoft.com/en-us/sysinternals/bb545021.aspx) website and unzip in selected directory. This utility will also add the selected folder path to system variable, so that [Sysinternals](https://technet.microsoft.com/en-us/sysinternals/bb545021.aspx) tools can be run from anywhere. [Download](https://github.com/abhishekgoenka/tools-and-utilities/blob/master/tools/binaries/SysinternalsToolsDownloader.exe)


[More Info](https://github.com/abhishekgoenka/tools-and-utilities/tree/master/tools/SysinternalsToolDownloader)


# SymbolPath
The debugger uses either the symbol search path that is specified by the user—which is found in Options\Debugging\Symbols in Visual Studio—or the _NT_SYMBOL_PATH environment variable. 

SymbolPath utility sets symbol search path in _NT_SYMBOL_PATH environment variable. [Download](https://github.com/abhishekgoenka/tools-and-utilities/blob/master/tools/binaries/SymbolPath.exe) 

[More Info](https://github.com/abhishekgoenka/tools-and-utilities/tree/master/tools/SymbolPath/SymbolPath.Console)

# ProcessExplorerColumnSets
The excellent [Process Explorer](https://technet.microsoft.com/en-us/sysinternals/processexplorer.aspx) offers a super useful feature for debugging on it's View menu, Column Sets. This is a quick way to look at key per-process performance counters. In this directory is a .REG file which will add the column sets I find most useful when looking at common problems. [Download](https://github.com/abhishekgoenka/tools-and-utilities/blob/master/tools/binaries/ProcessExplorerColumnSets.reg) 

Below are column sets added by the .REG file and what columns are turned on in the main process window. Note that all column sets turn on the following columns.

* Process 
* Process ID 
* CPU 
* Image Type 
* User Name 
* Description 
* Company Name 

Private Bytes (CTRL+1) - For memory usage
* Private Bytes History 
* Private Bytes 
* Working Set 

.NET GC Data (CTRL+2) - For .NET Garbage Collection information
* Gen 0 Collections 
* Get 1 Collections 
* Gen 2 Collections 
* % Time in GC 

Handle Data (CTRL+3) - For looking at handle leaks
* Handles 
* Threads 
* USER Obkects 
* GDI Objects 

I/O Counters (CTRL+4) - For looking at I/O issues
* I/O History 
* I/O Reads 
* I/O Writes 
* I/O Other 

None (CTRL+5) - Turns off all but the core column sets



# License
All tools are licensed under the [GPL(v3)](https://www.gnu.org/licenses/gpl-3.0.en.html) license.