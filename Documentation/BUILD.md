# Building PlexDL from source

## Before Modifying the Source Code
It is a requirement to first build the solution since multiple projects rely on each other. Failure to do so would result in a potentially broken Visual Studio Designer and unknown references amongst a host of other issues.

### Main NuGet References
- [libbrhscgui](https://www.nuget.org/packages/libbrhscgui/)
- [RestSharp](https://www.nuget.org/packages/RestSharp/)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
- [Google.Protobuf](https://www.nuget.org/packages/Google.Protobuf/)
- [Rssdp](https://www.nuget.org/packages/Rssdp/)
- [Sharpcaster.SocketsForPCL](https://www.nuget.org/packages/sharpcaster.SocketsForPCL/)
- [System.Buffers](https://www.nuget.org/packages/System.Buffers/)
- [System.Memory](https://www.nuget.org/packages/System.Memory/)
- [System.Numerics.Vectors](https://www.nuget.org/packages/System.Numerics.Vectors/)
- [System.Runtime.CompilerServices.Unsafe](https://www.nuget.org/packages/System.Runtime.CompilerServices.Unsafe/)

**In addition, other projects included in the source may need NuGet package restorations.**

### Third Party Projects Included in Source
- SharpCaster by Tapanila \[`SharpCaster`]
- AltoHttp (modified) by aalitor \[`PlexDL.AltoHttp`]
- WinFormAnimation by falahati \[`PlexDL.Animation`]
- PVS.MediaPlayer by Peter Vegter \[`PlexDL.Player`]
- csharp-plex-api by ammmze \[`PlexDL.MyPlex`]

### Build Prerequisites
- Visual Studio 2017/2019
- .NET Framework 4.7.2+
- C# 8.0 Language Support

### Steps for building
1. `git clone http://github.com/BRH-Media/PlexDL.git`
2. Open `PlexDL.sln` in Visual Studio 2017+
3. Enable restoring NuGet packages via `Tools->Options->NuGet Package Manager->Package Restore->Allow NuGet to download missing packages`
4. Right click the `PlexDL` Solution in the Solution Explorer
5. Select `Restore NuGet Packages`
6. `Build->Build Solution`
7. Run resulting `PlexDL.exe` in the `~\bin` folder

### PlexDL won't build?
The `master` branch should always be able to compile successfully. However, sometimes problems may arise.
If you have any trouble after following the steps above, please create an issue and tag it as a build problem. We'll get back to you as soon as we can!
