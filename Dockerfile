FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY HOVStory.API.sln ./
COPY DAOLibrary/*.csproj ./DAOLibrary/
COPY DTOLibrary/*.csproj ./DTOLibrary/
COPY HOVStoryConfiguration/*.csproj ./HOVStoryConfiguration/
COPY DTOTest/*.csproj ./DTOTest/
COPY HOVStory/*.csproj ./HOVStory/
COPY Utils/*.csproj ./Utils/

RUN dotnet restore
COPY . .
WORKDIR /src/Utils
RUN dotnet build -c Release -o /app

WORKDIR /src/DAOLibrary
RUN dotnet build -c Release -o /app

WORKDIR /src/DTOLibrary
RUN dotnet build -c Release -o /app

WORKDIR /src/HOVStoryConfiguration
RUN dotnet build -c Release -o /app

WORKDIR /src/DTOTest
RUN dotnet build -c Release -o /app

WORKDIR /src/HOVStory
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
# ENTRYPOINT [ "dotnet", "HOVStory.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HOVStory.dll
