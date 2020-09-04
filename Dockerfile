FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Pizzaria.csproj", "Pizzaria/"]
RUN dotnet restore "Pizzaria/Pizzaria.csproj"
WORKDIR "/src/Pizzaria"
COPY . .
RUN dotnet build "Pizzaria.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "Pizzaria.csproj" -c Release -o /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Pizzaria.dll
