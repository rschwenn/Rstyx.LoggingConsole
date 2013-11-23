@ECHO OFF
CLS

REM Help Viewer 2.0
REM Uninstall first in case it is already there.  If not, it won't install below.  We'll ignore any error output
REM by redirecting it to NUL.
start "Uninstall Help Viewr Content" "C:\Program Files (x86)\Microsoft Help Viewer\v2.1\HlpCtntMgr.exe" /catalogName VisualStudio12 /locale de-de /wait 0 /operation uninstall /vendor "Rstyx" /productName "Frameworks and Applications" /bookList "LoggingConsole" > NUL

REM Install the new content.
start "Install Help Viewr Content" "C:\Program Files (x86)\Microsoft Help Viewer\v2.1\HlpCtntMgr.exe" /catalogName VisualStudio12 /locale de-de /wait 0 /operation install /sourceUri "%CD%\LoggingConsole.msha"
