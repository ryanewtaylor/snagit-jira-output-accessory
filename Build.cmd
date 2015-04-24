@echo off
cls
.\.nuget\NuGet.exe Install FAKE -OutputDirectory packages -ExcludeVersion -Version 3.28.7
.\packages\FAKE\tools\FAKE.exe Build.fsx default
