@echo off

REM Run this to regenerate the unit test coverage statistics for use by Sonarqube

REM Delete old files, otherwise it whines
DEL Annoyances.coverage
DEL Annoyances.coverage.xml

REM Generate Annoyances.converage
"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:Annoyances.coverage "c:\Program Files (x86)\NUnit 2.6.4\bin\nunit-console.exe" .\tests\bin\Debug\Annoyances.Net.Tests.dll

REM Generate Annoyances.converage.xml
"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:Annoyances.coverage.xml Annoyances.coverage

ECHO.
ECHO Now load it into Sonarqube with this command:
ECHO "c:\Program Files (x86)\sonar-runner-2.4\bin\sonar-runner"
ECHO.
ECHO Then open http://localhost:9000

