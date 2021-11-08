#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["SchoolMgtAPI/SchoolMgtAPI.csproj", "SchoolMgtAPI/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Seeder/Seeder.csproj", "Seeder/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Utilities/Utilities.csproj", "Utilities/"]
RUN dotnet restore "SchoolMgtAPI/SchoolMgtAPI.csproj"
COPY . .
WORKDIR /src/SchoolMgtAPI
RUN dotnet build

FROM build AS publish
WORKDIR /src/SchoolMgtAPI
RUN dotnet publish -c Release -o /src/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .
#ENTRYPOINT ["dotnet", "SchoolMgtAPI.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SchoolMgtAPI.dll 