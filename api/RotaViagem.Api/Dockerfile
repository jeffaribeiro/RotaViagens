FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["api/RotaViagem.Api/RotaViagem.Api.csproj", "api/RotaViagem.Api/"]
COPY ["api/RotaViagem.Application/RotaViagem.Application.csproj", "api/RotaViagem.Application/"]
COPY ["api/RotaViagem.Infra.CrossCutting.IoC/RotaViagem.Infra.CrossCutting.IoC.csproj", "api/RotaViagem.Infra.CrossCutting.IoC/"]
COPY ["api/RotaViagem.Data/RotaViagem.Data.csproj", "api/RotaViagem.Data/"]
COPY ["api/RotaViagem.Data/RotaViagem.Domain.csproj", "api/RotaViagem.Domain/"]
RUN dotnet restore "./api/RotaViagem.Api/./RotaViagem.Api.csproj"
COPY . .

WORKDIR "/src/api/RotaViagem.Api"
RUN dotnet build "./RotaViagem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RotaViagem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RotaViagem.Api.dll"]