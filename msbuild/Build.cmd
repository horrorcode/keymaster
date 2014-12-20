SET NET_FRAMEWORK_DIR=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
CALL "%VS120COMNTOOLS%..\..\VC\vcvarsall.bat"
rem CALL "%VS110COMNTOOLS%..\..\VC\vcvarsall.bat"


msbuild BuildSolution.proj
pause