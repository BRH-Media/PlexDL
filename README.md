# PlexDL
Plex Downloader/Streamer written in C#

* Utilises MaterialSkin.NET by IgnaceMaes - https://github.com/IgnaceMaes/MaterialSkin
* Utilises csharp-plex-api by ammmze for server detection - https://github.com/ammmze/csharp-plex-api
* Utilises Google's Material Design Icons - https://material.io/icons
* Utilises AltoHttp by aalitor for Web Downloads - https://github.com/aalitor/AltoHttp
* Utilises PVS.MediaPlayer by Peter Vegter - https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library

### What does PlexDL do?
PlexDL uses a Plex Media Server's ability to serve XML API requests. PlexDL gathers information from the API and displays it in various gridviews to make it easier for you to enjoy your content. PlexDL can gather information about Plex Movies and TV Shows (archives and other content variations are not yet implemented), and provide you with the ability to stream the content or download it from the server. You can also view various metadata attributes about the selected content via the button in the "Data" section.

### Performance?
PlexDL is in **no way** stable enough to be called high-performance. It is, however, stable enough to be used in most situations, and will work for almost any PMS out there (provided you have an account key). However, there may be instances where the software is underperforming due to a variety of reasons. One such reason, is that the custom interfaces built to interpret the data from the PMS are far from perfect, and may stutter from time to time. PlexDL is also heavily reliant on internet speeds and reliability, so that is also a factor.

### How to get started
You will first need to build the software. PlexDL targets the .NET Framework 4.7.2, and was initially built with Visual Studio 2019. The PlexDL source comes preloaded with necessary icons and resources, AltoHttp, chsarp-plex-api, and PVS.MediaPlayer. You will need to obtain the latest DLL of MaterialSkin (by building it from source or from NuGet) and update the project reference accordingly. Another DLL you will need to obtain is the libbrhscgui project, which you can find on my project page. You will also need to install RestSharp via NuGet. Once you have configured the project, you may build it and launch the software.

### Using PlexDL
1. To get started, first obtain your Plex account token.
2. Select the connect button from the main panel and enter your account token
3. Allow the program to search for and connect to the server.
4. PlexDL may also display additional servers registered to your token in the Servers panel.
5. Once you are connected (and the library is filled), you may start to browse your library.
6. The "Library Content" panel (center datagrid) is your hub for titles. Here, you can select TV Shows and Movies.
7. If you select a movie from the "Library Content" panel, the options to stream or download the content will become immediately available.
8. If you select a TV Show from the "Library Content" panel, you may browse the seasons (Top-Right panel) and episodes (Bottom-Right panel) associated with that title.
9. You may only stream or download a TV Show title upon selecting an episode from the episodes panel.
10. You may browse metadata associated with your selected content by clicking the respective button in the "Data" section.
11. PlexDL allows Profile loading and saving, which may allow you to save you account token for later use, or change internal settings to your liking. To do this, first load your content view, and then select the Save icon. You can then edit the *.prof* XML file in any ordinary text editor.
12. Likewise, to load the profile, simply select the Load icon and browse to your generated *.prof* file.
