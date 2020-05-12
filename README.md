# PlexDL
### Plex Downloader/Streamer written in C#

* Utilises modified csharp-plex-api by ammmze for server detection - [GitHub Repo](https://github.com/ammmze/csharp-plex-api)
* Utilises Google's Material Design Icons - [Official Site](https://material.io/icons)
* Utilises Newtonsoft.Json by JamesNK - [GitHub Repo](https://github.com/JamesNK/Newtonsoft.Json)
* Utilises AltoHttp by aalitor for Web Downloads - [GitHub Repo](https://github.com/aalitor/AltoHttp)
* Utilises PVS.MediaPlayer by Peter Vegter - [CodeProject Article](https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library)
* Utilises WinFormAnimation by falahati - [Github Repo](https://github.com/falahati/WinFormAnimation/)
* Utilises CircularProgressBar by falhati - [GitHub Repo](https://github.com/falahati/CircularProgressBar/)

### What does PlexDL do?
PlexDL uses a Plex Media Server's ability to serve XML API requests. PlexDL gathers information from the API and displays it in various gridviews to make it easier for you to enjoy your content. PlexDL can gather information about Plex Movies and TV Shows (archives and other content variations are not yet implemented), and provide you with the ability to stream the content or download it from the server. You can also view various metadata attributes about the selected content via the button in the "Data" section.

### Performance?
PlexDL is in **no way** stable enough to be called high-performance. It is, however, stable enough to be used in most situations, and will work for almost any PMS out there (provided you have an account key/valid Plex.tv account). However, there may be instances where the software is underperforming due to a variety of reasons. One such reason, is that the custom interfaces built to interpret the data from the PMS are far from perfect, and may stutter from time to time. PlexDL is also heavily reliant on internet speeds and server reliability, so that is also a factor.

### How to get started
#### __1. Building from Source__
PlexDL targets the .NET Framework 4.7.2, and was initially built with Visual Studio 2019. The PlexDL source comes preloaded with all necessary icons and resources, AltoHttp, chsarp-plex-api, WinFormAnimation, CircularProgressBar and PVS.MediaPlayer.

PlexDL includes the appropriate NuGet references to libbrhscgui (prebuilt for you) and RestSharp, and upon cloning this repo, you'll just need to restore the packages. In addition, LogDel (a separate project linked with PlexDL in this repo), will need to have Newtonsoft.Json restored from NuGet in order to build. Note that LogDel is required for PlexDL operation, and cannot be excluded without modifying the source.

Steps for building
1. `git clone http://github.com/Brhsoftco/PlexDL-MetroSet_UI.git`
2. Open `PlexDL.sln` in Visual Studio 2017+
3. Emable restoring NuGet packages via `Tools->Options->NuGet Package Manager->Package Restore->Allow NuGet to download missing packages`
4. Right click the `PlexDL` Solution in the Solution Explorer
5. Select `Restore NuGet Packages`
6. `Build->Build Solution`
7. Run resulting `PlexDL.exe` in the `~\bin` folder

#### __2. Downloading from Releases__
Alternatively, can access the latest build [here](https://github.com/Brhsoftco/PlexDL-MetroSet_UI/releases/latest). Just download `Release.zip` to get all dependencies and the pre-built executable.
### __Using PlexDL__
#### __Basic Usage__
1. To get started, first obtain your Plex account token. A guide for this may be found [here](https://support.plex.tv/articles/204059436-finding-an-authentication-token-x-plex-token/). Alternatively, you can just use your Plex.tv account (v1.4.1 and above) to automatically retrieve your account token.
2. Select `Servers->Server Manager` from the main panel. This will load the Server Manager.
3. Select `Authenticate` and choose your preferred method.
4. Upon authenticating successfully, you can then use `Load->Servers` to populate the grid with your registered servers and `Load->Relays` to populate the grid with your registered Plex.tv indirect relays (`*.*.plex.direct` remote-access hostnames).
5. Select the server/relay you would like to connect to, and then select `Connect` on the Server Manager's menu.
6. Once you are connected (and the library is filled), you may start to browse your library.
7. The "Library Content" area is your hub for titles. Here, you can select TV Shows and Movies from their respective tabs.
8. If you select a movie from the "Movies" panel, the options to stream or download the content will become immediately available.
9. If you select a TV Show from the "TV" panel, you may browse the TV seasons (Top-Right grid) and episodes (Bottom-Right grid) associated with that title.
10. You may only stream or download a TV/Movie title upon selecting an item from the appropriate grid.
11. You may browse metadata associated with your selected content by using `Right-click->Metadata` or `Content->Metadata`.
12. PlexDL allows profile loading and saving, which allows you to save your account token for later use, or change internal settings to your liking and then save those changes. To do this, first follow Steps 1-6, and then select `File->Save` (it's also possible to use `Ctrl+S`). You can then edit the *.prof* XML file in any ordinary text editor.
13. Likewise, to load the profile, simply select `File->Load` (you can also use `Ctrl+O`), then browse to your generated XML *.prof* file.

#### __Content Filtering__
* PlexDL natively filters possibly adult-orientated content.
* It is possible to disable this filter by exporting a profile, then changing "AdultContentProtection" from "true" to "false" inside the resulting _.prof_ file, then reloading that profile back into PlexDL.
* For users' convenience, PlexDL will filter content that matches a genre-based criteria by pixelating posters in the metadata section, and warning users before streaming the content.
* PlexDL can also filter adult content based on a keyword list. E.g. a text file that contains terms related to adult content.
* PlexDL includes a blank file named "keywordBlacklist.txt" in the "Resources" section of the source code, however, you must populate this list yourself and build the source from there.
* PlexDL does not provide populated keyword lists by default; please do not ask for any.

#### __Shortcut Keys__
### __Main App__
* Control+O - Load a Profile
* Control+S - Save a Profile
* Control+C - Launches the Server Manager
* Control+D - Disconnects and Clears All Main Grids
* Control+M - Allows Viewing Metadata
* Control+F - Launches the Search Dialog
* Control+E - Allows Exporting a PMXML file of the currently selected content
### __Server Manager__
* Control+C - Launches Token Authentication Dialog
* Control+L - Launches Plex.tv Login Dialog
* Control+E - Allows Exporting PlexMovie XML Data (for the Currently Selected Title)
* Control+S - Populates Grid With Registered Servers
* Control+R - Populates Grid With Registered Plex.tv Relays
* Comtrol+D - Launches Direct Connection Dialog
