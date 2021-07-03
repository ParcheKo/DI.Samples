#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Samples.DI.CastleWindsor.WebAPI/Samples.DI.CastleWindsor.WebAPI.csproj", "Samples.DI.CastleWindsor.WebAPI/"]
RUN dotnet restore "Samples.DI.CastleWindsor.WebAPI/Samples.DI.CastleWindsor.WebAPI.csproj"
COPY . .
WORKDIR "/src/Samples.DI.CastleWindsor.WebAPI"
RUN dotnet build "Samples.DI.CastleWindsor.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samples.DI.CastleWindsor.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Samples.DI.CastleWindsor.WebAPI.dll"]