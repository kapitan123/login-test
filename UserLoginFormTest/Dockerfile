#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserLoginFormTest/UserLoginFormTest.csproj", "UserLoginFormTest/"]
RUN dotnet restore "UserLoginFormTest/UserLoginFormTest.csproj"
COPY . .
WORKDIR "/src/UserLoginFormTest"
RUN dotnet build "UserLoginFormTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserLoginFormTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserLoginFormTest.dll"]