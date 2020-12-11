#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Hubee.Notification.App/Hubee.NotificationApp.Api.csproj", "src/Hubee.Notification.App/"]
COPY ["src/Hubee.Notification.Infra/Hubee.NotificationApp.Infra.csproj", "src/Hubee.Notification.Infra/"]
COPY ["src/Hubee.Notification.Core/Hubee.NotificationApp.Core.csproj", "src/Hubee.Notification.Core/"]
RUN dotnet restore "src/Hubee.Notification.App/Hubee.NotificationApp.Api.csproj"
COPY . .
WORKDIR "/src/src/Hubee.Notification.App"
RUN dotnet build "Hubee.NotificationApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hubee.NotificationApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hubee.NotificationApp.Api.dll"]
