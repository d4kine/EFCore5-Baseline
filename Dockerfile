#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/EFCore5Baseline.WebAPI/EFCore5Baseline.WebAPI.csproj", "src/EFCore5Baseline.WebAPI/"]
RUN dotnet restore "src/EFCore5Baseline.WebAPI/EFCore5Baseline.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/EFCore5Baseline.WebAPI"
RUN dotnet build "EFCore5Baseline.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EFCore5Baseline.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFCore5Baseline.WebAPI.dll"]