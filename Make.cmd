@echo off
rem SET MSBUILD_PATH=%WinDir%\Microsoft.NET\Framework\v2.0.50727
rem SET MSBUILD_PATH=%WinDir%\Microsoft.NET\Framework\v3.5
rem SET MSBUILD_PATH=%WinDir%\Microsoft.NET\Framework\v4.0.30319
SET MSBUILD_PATH=c:\Program Files (x86)\MSBuild\12.0\Bin
SET PATH=%PATH%;%MSBUILD_PATH%

SET HOME=%USERPROFILE%

"%MSBUILD_PATH%\msbuild.exe" /v:d /fl %1 %2 %3 %4 %5 %6 %7 BuildSolution.proj 
pause