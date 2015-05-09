# Snagit JIRA Output Accessory 

This project adds a new button to Snagit's Output Accessories to enable you to send captured content directly to JIRA.

## Installation

### Prerequisites

* Windows 7 or better
* Snagit 11 or better
* .NET 4.5

*Note: The installer will verify that you have these prerequisites before continuing*

### Install
1. If you have a previous version installed, uninstall it via Control Panel
2. Download the latest installer from the [releases](https://github.com/ryanewtaylor/snagit-jira-output-accessory/releases)
3. Run the installation

### Configure
4. Launch Snagit Editor
5. Click the Share tab
6. Click the arrow/menu under the JIRA button
7. Click Options
8. Supply your JIRA Url, Username, and Password, example:
   * JIRA Url = https://jira.yourdomain.com
   * Username = Godzilla
   * Password = \*\*\*\*\*\*\*\*\*\*\*
9. Click Save

### Usage
10. Click the JIRA button to upload the selected image to a new issue
11. Click the JIRA arrow/menu for more options such as attaching the image to an existing issue

*Note: This has only been tested and verified on Windows 7, with Snagit 11, and 
.NET 4.5 and only on my machine. For a list of known issues and limitations (or
to submit new ones) see the [issues list](https://github.com/ryanewtaylor/snagit-jira-output-accessory/issues)*

## Contributing

### Prerequisites

* Visual Studio 2012
* [WiX Toolset](http://wixtoolset.org/)
* Snagit 11

### Getting the Source

* Fork [Snagit JIRA Output Accessory](https://github.com/ryanewtaylor/snagit-jira-output-accessory) on Github
* Clone your forked repository

### Programming

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
