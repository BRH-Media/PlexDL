mkdir white
for %%f in (*) do magick convert %%f -channel RGB -negate white\%%~nxf
pause