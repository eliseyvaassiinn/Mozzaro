FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Mozzaro.Client/Mozzaro.Client.csproj", "Mozzaro.Client/"]

RUN dotnet restore "Mozzaro.Client/Mozzaro.Client.csproj"

COPY . .

WORKDIR /src/Mozzaro.Client
RUN dotnet publish "Mozzaro.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "Mozzaro.Client.dll"]