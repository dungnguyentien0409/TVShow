#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TheConsoleProgram/TheConsoleProgram.csproj", "TheConsoleProgram/"]
COPY ["DataAccessEF/DataAccessEF.csproj", "DataAccessEF/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "TheConsoleProgram/TheConsoleProgram.csproj"
COPY . .
WORKDIR "/src/TheConsoleProgram"
RUN dotnet build "TheConsoleProgram.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheConsoleProgram.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheConsoleProgram.dll"]
