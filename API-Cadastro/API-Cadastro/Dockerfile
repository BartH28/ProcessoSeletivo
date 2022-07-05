#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["API-Cadastro/API-Cadastro.csproj", "API-Cadastro/"]
RUN dotnet restore "API-Cadastro/API-Cadastro.csproj"
COPY . .
WORKDIR "/src/API-Cadastro"
RUN dotnet build "API-Cadastro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API-Cadastro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API-Cadastro.dll"]