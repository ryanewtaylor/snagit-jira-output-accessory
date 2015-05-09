#r @"packages\FAKE\tools\FakeLib.dll"

open Fake
open System.Text
open System.IO

let buildVersion = "0.1.0"
let sha = Git.Information.getCurrentSHA1 @".\"
let currBranch = Git.Information.getBranchName @".\"

Target "Clean" (fun _ ->
    let binDirs = Directory.GetDirectories(@".\", "bin", SearchOption.AllDirectories) |> Seq.toList
    let objDirs = Directory.GetDirectories(@".\", "obj", SearchOption.AllDirectories) |> Seq.toList
    CleanDirs (List.append binDirs objDirs)
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

Target "PublishMsi" ( fun _ ->
    let msiDir = @".\SnagitJiraOutputAccessory.Setup\bin\Release"
    let versionedFilename = msiDir @@ sprintf "SnagitJiraOutputAccessorySetup-%s.msi" buildVersion
    Rename versionedFilename (msiDir @@ "SnagitJiraOutputAccessorySetup.msi")
)

Target "TagRelease" ( fun _ ->
    Git.Branches.tag "" buildVersion
    Git.Branches.pushTag "" "origin" buildVersion
)

Target "Release" DoNothing

"Clean"
    ==> "SetAssemblyInfo"
    ==> "BuildSolution"
    ==> "Default"
    =?> ("PublishMsi", currBranch = "master")
    =?> ("TagRelease", currBranch = "master")
    ==> "Release"

RunTargetOrDefault "Default"
