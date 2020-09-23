# What's this?
### `PlexDL.Common` is a module for the core project
---
It is designed to contain all core functionality and provide an interface for future modules. A breakdown of the architecture may be found below:

How does the data retention system work?
<br />
(Chronologically):
<br />
1. PlexDL's data 'renderer' takes a raw table (produced from the Plex API's XML format)

2. Once fed into the 'GenericRenderer' class, the data has a mask applied that seeks to whitelist certain relevant columns.
<br>This mask is known as the 'ViewTable' - it contains identical data, but does not include every column.

3. The view table is kept in sync with the 'RawTable' - this table contains the raw data in its entirety.
<br />
What this means is that there are always two tables when a table is drawn, and each table is kept in a 'Provider' within the 'DataProvider' 
class (providers are just DataSet objects).

4. When the user selects a new entry or otherwise prompts a table update, all previous data is flushed out and replaced with a new DataSet containing view and raw data.

5. This system works...if you don't need filtering; PlexDL needs this. Therefore, the 'SearchFramework' namespace contains code essential for running an efficient search process.
<br />
The way that the searching system works is simple: there exists another DataSet entitled 'FilteredProvider'. Upon a search operation, all existing RawData is copied to it from 'TitlesProvider', 
then immediately filtered via an SQL/LINQ procedure. When the data is 'rendered' by 'GenericRenderer' into the DataGridView, it of course produces a view table - this replaces the grid's tie to 'TitlesProvider'. 
Because there now exists a view table and a raw data table (filtered) in 'FiltersProvider', the system can now use these for its data references. However, the 'TitlesProvider' must remain 
filled because when the search ends, the data must be flushed from FilteredProvider (making it completely empty) and the 'TitlesProvider' data restored to the grid. This happens within milliseconds, 
due to the existing data being in memory.
