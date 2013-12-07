Rstyx.LoggingConsole
====================

LoggingConsole is a .NET Class Library designed for easyly adding lightweight and straightforward in-memory logging as well as a log viewer to Your WPF or Windows Forms application. 

The viewer is called Console here because it also contains the interface for complete user interaction with the logger: changing options and solve maintaining tasks.

There's also a simple VBA version


Intention
---------
 - Presentation of program results to the user
 - Debugging

Features
--------
 - The Log is maintained in-memory with configurable maximum Log length. 
 - The Log can be saved to a file at any time. 
 - 4 Levels: Error, Warning, Info, Debug. 
 - Message information: line number, date, time, level, source. 
 - Simple and easy to use gate from log4net
 - Optionally, the console is shown automatically, when an error or warning is logged.
 - Most settings and actions can be invoked programatically or interactive
 - GUI languages: English and German

Documentation
-------------
The repository containes full documentation and is prepared to re-create it with [SHFB] (http://shfb.codeplex.com/).
Also, You could have a look at the [website] (http://www.rstyx.de/LoggingConsole.html)

Dependencies
------------
 - .NET Framework 4.0
 - [Apache log4net](http://logging.apache.org/log4net/) (only at design time)

License
-------
This Software is published under the terms of [The MIT License] (http://opensource.org/licenses/mit-license.html)

Screenshot
----------
![Screenshot](/LoggingConsole/doc/SHFB/Images/Screen_LoggingConsole-Floating.png)
