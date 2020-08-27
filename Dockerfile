FROM mcr.microsoft.com/dotnet/framework/sdk:4.8

WORKDIR ./

# copy all files necessary to resolve dependencies
COPY    PlexDL.sln .
COPY    PlexDL/PlexDL.csproj ./PlexDL/
COPY    inet/inet.csproj ./inet/
COPY    UIHelpers/UIHelpers.csproj ./UIHelpers/
COPY    SharpCaster/SharpCaster.csproj ./SharpCaster/
COPY    PlexDL.WaitWindow/PlexDL.WaitWindow.csproj ./PlexDL.WaitWindow/
COPY    PlexDL.PlexAPI/PlexDL.PlexAPI.csproj ./PlexDL.PlexAPI/
COPY    PlexDL.PlexAPI.LoginHandler/PlexDL.PlexAPI.LoginHandler.csproj ./PlexDL.PlexAPI.LoginHandler/
COPY    PlexDL.Player/PlexDL.Player.csproj ./PlexDL.Player/
COPY    PlexDL.Common.Security/PlexDL.Common.Security.csproj ./PlexDL.Common.Security/
COPY    PlexDL.Common.Pxz/PlexDL.Common.Pxz.csproj ./PlexDL.Common.Pxz/
COPY    PlexDL.Common.Enums/PlexDL.Common.Enums.csproj ./PlexDL.Common.Enums/
COPY    PlexDL.Common.Components/PlexDL.Common.Components.csproj ./PlexDL.Common.Components/
COPY    PlexDL.Animation/PlexDL.Animation.csproj ./PlexDL.Animation/
COPY    PlexDL.AltoHTTP/PlexDL.AltoHTTP.csproj ./PlexDL.AltoHTTP/
COPY    LogDel/LogDel.csproj ./LogDel/
COPY    GitHubUpdater/GitHubUpdater.csproj ./GitHubUpdater/

# resolve dependencies
RUN     dotnet restore --verbosity normal

# copy all files
COPY    . .

# Build the app
WORKDIR /PlexDL
RUN     dotnet build
