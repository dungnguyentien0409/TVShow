#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UnitTest/UnitTest.csproj", "UnitTest/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["DataAccessEF/DataAccessEF.csproj", "DataAccessEF/"]
COPY ["TheConsoleProgram/TheConsoleProgram.csproj", "TheConsoleProgram/"]
COPY ["TheWebApplication/TheWebApplication.csproj", "TheWebApplication/"]
RUN dotnet restore "UnitTest/UnitTest.csproj"
COPY . .
WORKDIR "/src/UnitTest"
RUN dotnet build "UnitTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnitTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnitTest.dll"]
