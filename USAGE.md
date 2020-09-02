# PlexDL Usage Guide
### __Basic Usage__

---

1. To get started, first obtain your Plex account token. A guide for this may be found [here](https://support.plex.tv/articles/204059436-finding-an-authentication-token-x-plex-token/). Alternatively, you can just use your Plex.tv account (v1.4.1+) to automatically retrieve your account token.
2. Select `Servers->Server Manager` from the main panel. This will load the Server Manager.
3. Select `Authenticate` and choose your preferred method.
4. Upon authenticating successfully, you can then use `Load->Servers` to populate the grid with your registered servers and `Load->Relays` to populate the grid with your registered Plex.tv indirect relays (`*.*.plex.direct` remote-access hostnames).
5. Select the server/relay you would like to connect to, and then select `Connect` on the Server Manager's menu.
6. Once you are connected (and the library is filled), you may start to browse your library.
7. The "Library Content" area is your hub for titles. Here, you can select TV Shows and Movies from their respective tabs.
8. If you select a movie from the "Movies" panel, the options to stream or download the content will become immediately available.
9. If you select a TV Show from the "TV" panel, you may browse the TV seasons (Top-Right grid) and episodes (Bottom-Right grid) associated with that title.
10. Music titles follow the exact same principles as TV titles.
11. You may only stream or download a TV/Movie/Music title upon selecting an item from the appropriate grid.
12. You may browse metadata associated with your selected content by using `Right-click->Metadata` or `Content->Metadata`.
13. PlexDL allows profile loading and saving, which allows you to save your account token for later use, or change internal settings to your liking and then save those changes. To do this, first follow Steps 1-6, and then select `File->Save` (it's also possible to use `Ctrl+S`). You can then edit the *.prof* XML file in any ordinary text editor.
14. Likewise, to load the profile, simply select `File->Load` (you can also use `Ctrl+O`), then browse to your generated XML *.prof* file.

### __Content Filtering__

---

* PlexDL natively filters potentially adult-orientated content.
* It is possible to disable this filter by exporting a profile, then changing "AdultContentProtection" from "true" to "false" inside the resulting _.prof_ file, then reloading that profile back into PlexDL. You can also use `File->Settings` as a marginally faster option.
* For users' convenience, PlexDL will filter content that matches a genre-based criteria by pixelating posters in the metadata section, and warning users before streaming the content. The plot summary is also replaced with `Plot summary censored`, but genre-based filtering first requires the server to provide the correct criteria information.
* PlexDL can also filter adult content based on a keyword list. E.g. a text file that contains terms related to adult content.
* PlexDL includes a blank file named `keywordBlacklist.txt` in the `PlexDL\Resources` section of the source code. However, you must populate this list yourself and build the source from there. The format of this file is simply one blacklisted term per line.
* PlexDL does not provide populated keyword lists by default; please do not ask for any. Also note that there are many available via a quick Google Search.

### __Shortcut Keys__

---

#### __Main App__
* `Control+O` - Load a Profile
* `Control+S` - Save a Profile
* `Control+C` - Launches the Server Manager
* `Control+D` - Disconnects and Clears All Main Grids
* `Control+M` - Allows Viewing Metadata
* `Control+F` - Launches the Search Dialog
* `Control+E` - Allows Exporting a PMXML file of the currently selected content
#### __Server Manager__
* `Control+C` - Launches Token Authentication Dialog
* `Control+L` - Launches Plex.tv Login Dialog
* `Control+E` - Allows Exporting PlexMovie XML Data (for the Currently Selected Title)
* `Control+S` - Populates Grid With Registered Servers
* `Control+R` - Populates Grid With Registered Plex.tv Relays
* `Control+D` - Launches Direct Connection Dialog
* `Control+K` - Launches Local Machine Connection Dialog
#### __Metadata__
* `Control+S` - Export a PMXML File of the Currently Loaded Content
* `Control+O` - Load a PMXML File into the Metadata Window
* `Control+V` - Start Streaming the Currently Loaded Content in VLC Media Player
* `Control+B` - Start Streaming the Currently Loaded Content in your Default Browser
#### __PVS.MediaPlayer__
* `Spacebar`    - Play/Pause Current Content
* `Up Arrow`    - Load Previous Content in Grid
* `Down Arrow`  - Load Next Content in Grid
* `Right Arrow` - Skip Forward
* `Left Arrow`  - Skip Backward
* `F` - Toggle Fullscreen Mode

### Setting up VLC for use with PlexDL

---

Because VLC accepts a vast array of command-line arguments and values, PlexDL only needs to know where the location of `vlc.exe` is. This way, it may execute it on the fly. To point PlexDL to VLC, please follow the following procedure:
1. Load a server of your choice and populate the gridviews.
2. `File->Settings`
3. Expand `Streaming Settings`
4. Set `VLC Path` to the location of `vlc.exe`<br>
i.e. `C:\Program Files (x86)\VideoLAN\VLC\vlc.exe`
5. After setting the value, click `OK` and the window will close.

_**Note: These changes won't be saved on a restart of PlexDL unless you export a profile, then reimport it each time or you commit the settings to default.**_
