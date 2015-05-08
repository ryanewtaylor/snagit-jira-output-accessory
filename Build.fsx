#r @"packages\FAKE\tools\FakeLib.dll"

open Fake
open System.Text

let buildVersion = "0.1.0.0"
let sha = Git.Information.getCurrentSHA1 @".\"

Target "Clean" (fun _ ->
    MSBuildWithDefaults "Clean" [".\SnagitJiraOutputAccessory.sln"]
    |> Log "AppBuild-Output: "
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
