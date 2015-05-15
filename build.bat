call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" amd64
msbuild src\MSBuild.TeamCity.Tasks.xml /p:Configuration=Release;Platform=AnyCPU