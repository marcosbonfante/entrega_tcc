#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Services/SGM.Solicitacao.API/SGM.Solicitacao.API.csproj", "Services/SGM.Solicitacao.API/"]
COPY ["./Building Blocks/SGM.WebAPI.Core/SGM.WebAPI.Core.csproj", "Building Blocks/SGM.WebAPI.Core/"]
COPY ["./Building Blocks/SGM.MessageBus/SGM.MessageBus.csproj", "Building Blocks/SGM.MessageBus/"]
RUN dotnet restore "Services/SGM.Solicitacao.API/SGM.Solicitacao.API.csproj"
COPY . .
WORKDIR "Services/SGM.Solicitacao.API"
RUN dotnet build "SGM.Solicitacao.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.Solicitacao.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.Solicitacao.API.dll"]