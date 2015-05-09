@echo off
cls
.\.nuget\NuGet.exe Install FAKE -OutputDirectory packages -ExcludeVersion -Version 3.28.7

set TARGET="DEFAULT"
if not [%1]==[] (set TARGET="%1")
.\packages\FAKE\tools\FAKE.exe Build.fsx target=%TARGET%
