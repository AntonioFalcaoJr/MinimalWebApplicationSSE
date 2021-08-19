FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApplicationSSE/WebApplicationSSE.csproj", "WebApplicationSSE/"]
RUN dotnet restore "WebApplicationSSE/WebApplicationSSE.csproj"
COPY . .
WORKDIR "/src/WebApplicationSSE"
RUN dotnet build "WebApplicationSSE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplicationSSE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplicationSSE.dll"]
