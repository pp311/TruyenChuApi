FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
#COPY ["WebTruyenChu_Backend/WebTruyenChu_Backend.csproj", "WebTruyenChu_Backend/"]
COPY ["WebTruyenChu_Backend.csproj", "./"]
RUN dotnet restore "WebTruyenChu_Backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebTruyenChu_Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebTruyenChu_Backend.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebTruyenChu_Backend.dll"]
