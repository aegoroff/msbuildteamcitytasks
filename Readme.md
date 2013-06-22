MSBuild TeamCity Tasks Project provides useful tasks that can help to interact build script with JetBrains TeamCity. Some of these features include displaying real-time test results and customized statistics, changing the build status, and publishing artifacts before the build is finished.

Current Version: *2.3* [[Version History]]

There are following tasks implemented:
  * [[Block Close]] - Block closing message.
  * [[Block Open]] - Block opening message.
  * [[Build Number]] - To set a custom build number directly
  * [[Build Progress Finish]] - Writes progress finish message into TeamCity log
  * [[Build Progress Message]] - Writes progress message into TeamCity log
  * [[Build Progress Start]] - Writes progress start message into TeamCity log
  * [[Build Status]] - Changing build status
  * [[Import Data]] - Third party data import.
  * [[Import Google Tests]] - Imports Google tests (http://code.google.com/p/googletest/) results in xml format into TC. *2.2 version supports only TC 6.5 and higher*
  * [[Run Google Tests]] - Runs Google test (http://code.google.com/p/googletest/) executable and integrate results into TC. *Only for versions 1.1 and newer*. *2.2 version supports only TC 6.5 and higher*
  * [[Publish Artifacts]] - Publish (upload) artifact(s) to JetBrains TeamCity server
  * [[Report Build Statistic]] - Represents common statistic reporter into TeamCity
  * [[Report Message]] - Reports messages for build log
  * *[[Cover Report]]* - Manually configures .NET coverage processing using NCover 1.x tool. *Only for versions 1.1 and newer*
  * *[[Cover3 Report]]* - Manually configures .NET coverage processing using NCover 3 tool. *Only for versions 1.1 and newer*
  * *[[Part Cover Report]]* - Manually configures .NET coverage processing using PartCover (http://partcover.blogspot.com/) tool. *Only for versions 1.1 and newer*
  * *[[Run Part Coverage]]* - Runs code coverage using PartCover tool (http://partcover.blogspot.com/) and exports results into TC. *Only for versions 1.1 and newer*
  * *[[Enable Service Messages]]* - Enables message processing that has been disabled before. *Only for versions 2.0 and newer*. Available since TC 6.0.
  * *[[Disable Service Messages]]* - Disables message processing. *Only for versions 2.0 and newer*. Available since TC 6.0
  * *[[Compilation Started]]* - Starts reporting compilation messages using compiler specified. *Only for versions 2.0 and newer*. Available since TC 6.0.
  * *[[Compilation Finished]]* - Finishes reporting compilation messages using compiler specified. *Only for versions 2.0 and newer*. Available since TC 6.0
  * *[[Test Started]]* - Test start message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[[Test Finished]]* - Test finish message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[[Test Suite Started]]* -  Test suite start message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[[Test Suite Finished]]* -  Test suite start message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[[Test Std Out]]* -  Report test results into stdout message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[[Test Std Err]]* -  Report test results into stderr message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[[Test Failed]]* -  Test fail details message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[[Test Ignored]]* -  Test ignore message. *Only for versions 2.1 and newer*. Available since TC 4.0
  * *[[Set Parameter]]* -  Adding or Changing a Build Parameter from a Build Step. *Only for versions 2.3 and newer*. Available since TC 7.0

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