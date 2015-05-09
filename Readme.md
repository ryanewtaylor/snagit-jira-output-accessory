# Snagit Jira Output Accessory 

This project adds a new button to Snagit's Output Accessories to enable you to send captured content directly to Jira.

# Installation

# Contributing

## Prerequisites

* Visual Studio 2012
* [WiX Toolset](http://wixtoolset.org/)
* Snagit 11

## Getting the Source

* Fork repository on Github
* Clone your forked repository

## Programming

* Verify your cloned repo works:
  * Open SnagitJiraOutputAccessory.sln with Visual Studio
  * Open the SnagitJiraOutputAccessory project properties
  * Navigate to the Debug property page
  * Check Start external program and enter the path to your SnagitEditor.exe (e.g., `C:\Program Files (x86)\TechSmith\Snagit 11\SnagitEditor.exe)`
  * Press F6 to build the solution
  * Run as Admin `%LOCALAPPDATA%\TechSmith\Snagit\Accessories\{55A50CAA-D9E3-4239-8B02-D6D2A396F4AF}\Register.bat`
  * Press F5 to Debug
* Create a new feature or bug fix branch
* Implement your feature of bug fix
* Commit and push changes to your fork
* Use Github to create a pull request to the original repo

More details to follow as the code/process evolves
