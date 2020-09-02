# PlexDL
### Plex Downloader/Streamer written in C#

* Utilises modified csharp-plex-api by ammmze for server detection - [GitHub Repo](https://github.com/ammmze/csharp-plex-api)
* Utilises Google's Material Design Icons - [Official Site](https://material.io/icons)
* Utilises Newtonsoft.Json by JamesNK - [GitHub Repo](https://github.com/JamesNK/Newtonsoft.Json)
* Utilises AltoHttp by aalitor for Web Downloads - [GitHub Repo](https://github.com/aalitor/AltoHttp)
* Utilises PVS.MediaPlayer by Peter Vegter - [CodeProject Article](https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library)
* Utilises WinFormAnimation by falahati - [Github Repo](https://github.com/falahati/WinFormAnimation/)
* Utilises CircularProgressBar by falahati - [GitHub Repo](https://github.com/falahati/CircularProgressBar/)
* Utilises SharpCaster by Tapanila - [GitHub Repo](https://github.com/Tapanila/SharpCaster)

### What does PlexDL do?
Many different things!

PlexDL uses a Plex Media Server's ability to serve XML API requests, and from the data returned, PlexDL gathers information and displays it in various gridviews to make it easier for you to enjoy your content. It supports the following and more:
- Viewing Metadata about any media type currently supported. To do this, just head over to `Content->Metadata` on the main toolbar.
- Streaming your media. PlexDL utilises a really cool media library known as PVS.MediaPlayer. It allows playback of supported files via the Windows Media Foundation (WMF), and the best part? It's possible to create a custom GUI and skin it how you want. That's what we've done for you in PlexDL, but if you're not wanting to use PVS, you can use VLC or your default browser.
- Downloading your media. PlexDL is named like this for a reason! We've created a specialised framework to allow authorised Plex downloads. Even if you're not a server owner, you can still download if you have an account or an account token.
- Exporting media profiles. You can export a .pmxml file which will store infirmation about your content. At any time (and given your token is still valid), you can import this back into the `Content->Metadata` window and stream it without even logging in!

**Important Notes**

- PVS.MediaPlayer can play some codecs (any that are native to WMF), but the diversity of the container specification makes it unsafe (code-wise) to implement it at this point. Hence, PlexDL will not allow its default player to accept \*.mkv files.
- If you're using VLC, make sure you follow the guide below about setting it up with PlexDL.

### Supported Media
PlexDL currently supports the following media types
- Music - Artists, Albums and Tracks
- TV - Shows, Seasons and Episodes
- Movies

More will be on the way, but please note that photos are current very difficult to implement. As such, we can't support this feature until sufficient resources can be devoted to it.

### Performance?
PlexDL will work for almost any PMS out there (provided you have an account key/valid Plex.tv account). However, there may be instances where the software is underperforming due to a variety of reasons. One such reason, is that the custom interfaces built to interpret the data from the PMS aren't perfect, and may stutter from time to time. PlexDL is also heavily reliant on internet speeds and server reliability, so that is also a factor.

It should be noted, however, that PlexDL does support various forms of caching. This will store downloaded information in the  `%APPDATA%\.plexdl\caching` folder. The structure of the caching folder is as follows:
```
\caching                              -- Root Folder
└───\%TOKEN_HASH%                     -- MD5 of account token
    ├──\%SERVER_HASH%                 -- MD5 of server IP
    │  ├───\thumb                     -- Cached images with *.thumb filename
    │  │   └───%IMAGE_URL_HASH%.thumb -- *.thumb is named as a hashed URL, to be retrieved when a matching request is ID'd.
    │  └───\xml                       -- Cached XML API data with *.xml filename
    │      └───%XML_URL_HASH%.xml     -- *.xml is named as a hashed URL, to be retrieved when a matching request is ID'd.
    └──%TOKEN_HASH%.slst              -- Cached server details list (cached IP, port, etc.)
       
```
Using the Settings dialog in `File->Settings`, you can individually enable/disable the three forms of PlexDL caching:
- Server List Caching
- XML API Caching
- Image Caching

By using caching, you can drastically increase the performance of the application, as PlexDL can skip downloading a new copy of the file each time. However, the obvious downside is remembering to regularly clear the cache, as the stored data will quickly become outdated (leading to future problems with server connections).

### How to get started
#### __1. Building from Source__
[Follow the Build Guide](./BUILDING.md)

#### __2. Downloading from Releases__
Alternatively, you can access the latest official build [here](https://github.com/Brhsoftco/PlexDL/releases/latest). Just download `Release.zip` to get all of the needed dependencies and the binary itself.

### __Using PlexDL__
[Read the Usage Guide](./USAGE.md)

### __Looking for Demo Images?__
[You Can Find Them Here](./demo_images)
