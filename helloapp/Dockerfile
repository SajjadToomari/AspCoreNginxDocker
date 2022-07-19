#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["helloapp.csproj", "helloapp/"]
RUN dotnet restore "helloapp/helloapp.csproj"

WORKDIR "/src/helloapp"
COPY . .
RUN dotnet build "helloapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "helloapp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "helloapp.dll"]