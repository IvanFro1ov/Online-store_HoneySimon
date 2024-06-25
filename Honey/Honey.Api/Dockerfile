FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5433

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["Honey.Api/.", "Honey.Api/"]
COPY ["Honey.BL/.", "Honey.BL/"]
COPY ["Honey.Common/.", "Honey.Common/"]
COPY ["Honey.DB/.", "Honey.DB/"]
COPY ["Honey.Domain/.", "Honey.Domain/"]
#COPY nuget.config .
RUN dotnet restore "Honey.Api/Honey.Api.csproj"
COPY . .
WORKDIR "/src/Honey.Api"
RUN dotnet build "Honey.Api.csproj" -c Debug -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "Honey.Api.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Honey.Api.dll"]
