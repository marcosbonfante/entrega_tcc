#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Services/SGM.Identity.API/SGM.Identity.API.csproj", "Services/SGM.Identity.API/"]
COPY ["./Building Blocks/SGM.MessageBus/SGM.MessageBus.csproj", "Building Blocks/SGM.MessageBus/"]
COPY ["./Building Blocks/SGM.WebAPI.Core/SGM.WebAPI.Core.csproj", "Building Blocks/SGM.WebAPI.Core/"]
RUN dotnet restore "Services/SGM.Identity.API/SGM.Identity.API.csproj"
COPY . .
WORKDIR "Services/SGM.Identity.API"
RUN dotnet build "SGM.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.Identity.API.dll"]