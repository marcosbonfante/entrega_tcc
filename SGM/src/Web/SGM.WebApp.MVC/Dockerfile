#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Web/SGM.WebApp.MVC/SGM.WebApp.MVC.csproj", "Web/SGM.WebApp.MVC/"]
COPY ["./Building Blocks/SGM.WebAPI.Core/SGM.WebAPI.Core.csproj", "Building Blocks/SGM.WebAPI.Core/"]
RUN dotnet restore "Web/SGM.WebApp.MVC/SGM.WebApp.MVC.csproj"
COPY . .
WORKDIR "Web/SGM.WebApp.MVC"
RUN dotnet build "SGM.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.WebApp.MVC.dll"]