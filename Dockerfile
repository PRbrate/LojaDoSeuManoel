
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/LojaDoSeuManoel/LojaDoSeuManoel.Api/LojaDoSeuManoel.Api.csproj", "Src/LojaDoSeuManoel/LojaDoSeuManoel.Api/"]
COPY ["Src/Core/LojaDoSeuManoel.Core/LojaDoSeuManoel.Core.csproj", "Src/Core/LojaDoSeuManoel.Core/"]
RUN dotnet restore "./Src/LojaDoSeuManoel/LojaDoSeuManoel.Api/LojaDoSeuManoel.Api.csproj"
COPY . .
WORKDIR "/src/Src/LojaDoSeuManoel/LojaDoSeuManoel.Api"
RUN dotnet build "./LojaDoSeuManoel.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LojaDoSeuManoel.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LojaDoSeuManoel.Api.dll"]