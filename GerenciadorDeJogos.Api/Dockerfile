#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["GerenciadorDeJogos.Api/GerenciadorDeJogos.Api.csproj", "GerenciadorDeJogos.Api/"]
COPY ["GerenciadorDeJogos.Infrastructure/GerenciadorDeJogos.Infrastructure.csproj", "GerenciadorDeJogos.Infrastructure/"]
COPY ["GerenciadorDeJogos.Domain/GerenciadorDeJogos.Domain.csproj", "GerenciadorDeJogos.Domain/"]
COPY ["GerenciadorDeJogos.Application/GerenciadorDeJogos.Application.csproj", "GerenciadorDeJogos.Application/"]
RUN dotnet restore "GerenciadorDeJogos.Api/GerenciadorDeJogos.Api.csproj"
COPY . .
WORKDIR "/src/GerenciadorDeJogos.Api"
RUN dotnet build "GerenciadorDeJogos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GerenciadorDeJogos.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GerenciadorDeJogos.Api.dll"]