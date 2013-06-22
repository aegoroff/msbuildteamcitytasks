MSBuild TeamCity Tasks Project provides useful tasks that can help to interact build script with JetBrains TeamCity. Some of these features include displaying real-time test results and customized statistics, changing the build status, and publishing artifacts before the build is finished.

Current Version: *2.3* [VersionHistory]

There are following tasks implemented:
  * *BlockClose* - Block closing message.
  * *BlockOpen* - Block opening message.
  * *BuildNumber* - To set a custom build number directly
  * *BuildProgressFinish* - Writes progress finish message into TeamCity log
  * *BuildProgressMessage* - Writes progress message into TeamCity log
  * *BuildProgressStart* - Writes progress start message into TeamCity log
  * *BuildStatus* - Changing build status
  * *ImportData* - Third party data import.
  * *ImportGoogleTests* - Imports Google tests (http://code.google.com/p/googletest/) results in xml format into TC. *2.2 version supports only TC 6.5 and higher*
  * *RunGoogleTests* - Runs Google test (http://code.google.com/p/googletest/) executable and integrate results into TC. *Only for versions 1.1 and newer*. *2.2 version supports only TC 6.5 and higher*
  * *PublishArtifacts* - Publish (upload) artifact(s) to JetBrains TeamCity server
  * *ReportBuildStatistic* - Represents common statistic reporter into TeamCity
  * *ReportMessage* - Reports messages for build log
  * *[NCoverReport]* - Manually configures .NET coverage processing using NCover 1.x tool. *Only for versions 1.1 and newer*
  * *[NCover3Report]* - Manually configures .NET coverage processing using NCover 3 tool. *Only for versions 1.1 and newer*
  * *[PartCoverReport]* - Manually configures .NET coverage processing using PartCover (http://partcover.blogspot.com/) tool. *Only for versions 1.1 and newer*
  * *[RunPartCoverage]* - Runs code coverage using PartCover tool (http://partcover.blogspot.com/) and exports results into TC. *Only for versions 1.1 and newer*
  * *[EnableServiceMessages]* - Enables message processing that has been disabled before. *Only for versions 2.0 and newer*. Available since TC 6.0.
  * *[DisableServiceMessages]* - Disables message processing. *Only for versions 2.0 and newer*. Available since TC 6.0
  * *[CompilationStarted]* - Starts reporting compilation messages using compiler specified. *Only for versions 2.0 and newer*. Available since TC 6.0.
  * *[CompilationFinished]* - Finishes reporting compilation messages using compiler specified. *Only for versions 2.0 and newer*. Available since TC 6.0
  * *[TestStarted]* - Test start message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[TestFinished]* - Test finish message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[TestSuiteStarted]* -  Test suite start message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[TestSuiteFinished]* -  Test suite start message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[TestStdOut]* -  Report test results into stdout message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[TestStdErr]* -  Report test results into stderr message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[TestFailed]* -  Test fail details message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[TestIgnored]* -  Test ignore message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[SetParameter]* -  Adding or Changing a Build Parameter from a Build Step. *Only for versions 2.3 and newer*. Available since TC 7.0

So as to use these tasks you have to:

  * Download and install tasks using MSI installer
  * Include following line into your MSBuild script:
```
<Import Project="$(MSBuildExtensionsPath)\MSBuildTeamCityTasks\MSBuild.TeamCity.Tasks.Targets"/>
```

## Requirements 1.1
 * Microsoft .NET Framework 2.0 
 * Any Windows version where Microsoft .NET Framework 2.0 is supported.
 * JetBrains TeamCity 4.5 and newer

## Requirements 2.1 and 2.2
 * Microsoft .NET Framework 3.5 
 * Any Windows version where Microsoft .NET Framework 3.5 is supported.
 * JetBrains TeamCity 4.5 and newer

## Requirements 2.3
 * Microsoft .NET Framework 4.0
 * Any Windows version where Microsoft .NET Framework 4.0 is supported.
 * JetBrains TeamCity 4.5 and newer