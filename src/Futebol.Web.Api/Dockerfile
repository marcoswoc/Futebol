FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ./src .

RUN dotnet restore "Futebol.Web.Api/Futebol.Web.Api.csproj"
RUN dotnet build "Futebol.Web.Api/Futebol.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Futebol.Web.Api/Futebol.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Futebol.Web.Api.dll