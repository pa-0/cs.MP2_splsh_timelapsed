@echo off

call FindProgramDir.bat

"%ProgramDir%\Microsoft Visual Studio 9.0\Common7\IDE\devenv.com" ..\MediaPortal-VS2008.sln /Rebuild "Release|x86" /Out build.log