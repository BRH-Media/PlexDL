# A Few Brief Notes on PlexDL Encryption

**For transparency, I have decided to detail how the security of the application works for those that can't/don't want to read the C# code**

### Why is it necessary?
Unfortunately, people love taking advantage over others - this is an obvious issue. However, this extends to solutions like PlexDL as well, since it stores crucial information like your Plex.tv token.

### How does it work?
PlexDL utilises the Windows Data Protection API (WDPAPI) to encrypt/decrypt your data; this also makes it trivial to recover your PlexDL data from user certificates. 
Essentially, the WDPAPI works by utilising your user account as a 'seed' for a key; hence, you need to be the user that originally encrypted the file in order to decrypt it.

### Implementation?
It's a standard implementation of the WDPAPI, excepted that it uses pseudorandom 20-byte entropy data to aid the protection. The PXZ format also maintains support for the WDPAPI, in that the 'RawContent' field can be encrypted/decrypted directly on read. 
Currently, however, no native PXZ records produced by PlexDL maintain WDPAPI protection. This is due to the assumption that exported files will be shared around beyond the current scope of the user.
