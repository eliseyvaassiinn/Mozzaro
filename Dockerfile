FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Mozzaro.API/Mozzaro.API.csproj", "Mozzaro.API/"]
COPY ["Mozzaro.Application/Mozzaro.Application.csproj", "Mozzaro.Application/"]
COPY ["Mozzaro.Domain/Mozzaro.Domain.csproj", "Mozzaro.Domain/"]
COPY ["Mozzaro.Infrastructure/Mozzaro.Infrastructure.csproj", "Mozzaro.Infrastructure/"]

RUN dotnet restore "Mozzaro.API/Mozzaro.API.csproj"

COPY . .

WORKDIR /src/Mozzaro.API
RUN dotnet publish "Mozzaro.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "Mozzaro.API.dll"]