#r @"packages\FAKE\tools\FakeLib.dll"

open Fake
open System.Text

let buildVersion = if buildVersion = localBuildLabel then "0.0.0" else "0.1.0"
let sha = Git.Information.getCurrentSHA1 @".\"

let debugBuildDir = @".\SnagitJiraOutputAccessory\bin\Debug"
let releaseBuildDir = @".\SnagitJiraOutputAccessory\bin\Release"

Target "Clean" (fun _ ->
    CleanDirs [debugBuildDir; releaseBuildDir]
)

Target "SetAssemblyInfo" (fun _ ->
    !! ".\**\AssemblyInfo.cs"
    |> RegexReplaceInFilesWithEncoding @"[\d]\.[\d]\.[\d](\.[\d])?" buildVersion Encoding.UTF8

    !! ".\**\AssemblyInfo.cs"
    |> RegexReplaceInFilesWithEncoding @"\b[0-9a-f]{40,40}\b" sha Encoding.UTF8
)

Target "BuildSolution" (fun _ ->
    MSBuildWithDefaults "Build" [".\SnagitJiraOutputAccessory.sln"]
    |> Log "AppBuild-Output: "
)

Target "Default" DoNothing

"Clean"
    ==> "SetAssemblyInfo"
    ==> "BuildSolution"
    ==> "Default"

RunTargetOrDefault "Default"
