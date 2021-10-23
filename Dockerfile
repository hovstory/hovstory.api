FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY HOVStory.API.sln ./
COPY DAOLibrary/*.csproj ./DAOLibrary/
COPY DTOLibrary/*.csproj ./DTOLibrary/
COPY HOVStoryConfiguration/*.csproj ./HOVStoryConfiguration/
COPY DTOTest/*.csproj ./DTOTest/
COPY HOVStory/*.csproj ./HOVStory/

RUN dotnet restore
COPY . .
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