#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Ambev.Sales.WebApi/Ambev.Sales.WebApi.csproj", "src/Ambev.Sales.WebApi/"]
COPY ["src/Ambev.Sales.IoC/Ambev.Sales.IoC.csproj", "src/Ambev.Sales.IoC/"]
COPY ["src/Ambev.Sales.Domain/Ambev.Sales.Domain.csproj", "src/Ambev.Sales.Domain/"]
COPY ["src/Ambev.Sales.Common/Ambev.Sales.Common.csproj", "src/Ambev.Sales.Common/"]
COPY ["src/Ambev.Sales.Application/Ambev.Sales.Application.csproj", "src/Ambev.Sales.Application/"]
COPY ["src/Ambev.Sales.ORM/Ambev.Sales.ORM.csproj", "src/Ambev.Sales.ORM/"]
RUN dotnet restore "./src/Ambev.Sales.WebApi/Ambev.Sales.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Ambev.Sales.WebApi"
RUN dotnet build "./Ambev.Sales.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ambev.Sales.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ambev.Sales.WebApi.dll"]