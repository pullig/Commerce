#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 40
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Commerce.WebApi/Commerce.WebApi.csproj", "Commerce.WebApi/"]
RUN dotnet restore "Commerce.WebApi/Commerce.WebApi.csproj"
COPY . .
WORKDIR "/src/Commerce.WebApi"
RUN dotnet build "Commerce.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Commerce.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Commerce.WebApi.dll"]




