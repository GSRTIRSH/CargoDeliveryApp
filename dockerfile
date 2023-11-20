FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY CargoApp.Presentation/*.csproj CargoApp.Presentation/
RUN dotnet restore "CargoApp.Presentation/CargoApp.Presentation.csproj"
COPY . .
WORKDIR "/CargoApp.Presentation"
RUN dotnet build "CargoApp.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CargoApp.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CargoApp.Presentation.dll"]