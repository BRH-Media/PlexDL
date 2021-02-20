# PlexDL XML-Zipped Format
### What is it?
PXZ is a dynamic format purely designed to Zip together XML-based "records" with an indexing register. 
The aim is to provide an easily accessible format for data management, and basic security.

### Format Architecture
#### PXZ Index
Is an XML file detailing the author of the file, all records contained and misc. auxillary values.

#### PXZ Record
Is an XML file containing GZipped binary, image, or text data. It contains an unencrypted header used to specify content attributes, security flags and checksums.

#### PXZ Archive
Is purely a Zip file renamed to *.pxz. It is designed to be accessed by ordinary file managers; the parsing of the files within must be handled by the `PlexDL.Common.Pxz` module, however. This is free and open to include in your projects.

### Important Points
* Security is applied via the Windows Data Protection API (WDPAPI); this is handled by the `PlexDL.Common.Security` module.
* Encrypted PXZ records cannot be viewed if you are not logged in as the user who created them. You may still view unencrypted files in the same session as encrypted ones, however.
* Compression is applied on three different levels:
    * The data itself is GZipped and Base64 encoded
    * The XML (containing the data and the header) is GZipped and Base64 encoded to make a `*.record` file
    * The entire archive is compressed using Zip when packaged into a `*.pxz` file.
* `*.record` files are named using a random GUID (including hyphens); this ensures duplicate data may still be accepted by the parser if it were to ever occur.
