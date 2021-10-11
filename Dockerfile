﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY BoardLobbyServer/published/ ./
ENTRYPOINT ["dotnet", "BoardLobbyServer.dll"]