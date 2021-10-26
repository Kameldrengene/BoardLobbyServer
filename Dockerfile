#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
#WORKDIR /app
#COPY BoardLobbyServer/published/ ./
#ENTRYPOINT ["dotnet", "BoardLobbyServer.dll"]




FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BoardLobbyServer/*.csproj ./BoardLobbyServer/
RUN dotnet restore

# copy everything else and build app
COPY BoardLobbyServer/. ./BoardLobbyServer/
WORKDIR /source/BoardLobbyServer
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "BoardLobbyServer.dll"]