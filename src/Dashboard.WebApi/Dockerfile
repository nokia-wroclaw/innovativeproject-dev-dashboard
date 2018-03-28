FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 5001

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Dashboard.sln ./
COPY Dashboard.WebApi/Dashboard.WebApi.csproj Dashboard.WebApi/
COPY Dashboard.Application/Dashboard.Application.csproj Dashboard.Application/
COPY Dashboard.Core/Dashboard.Core.csproj Dashboard.Core/
COPY Dashboard.Data/Dashboard.Data.csproj Dashboard.Data/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Dashboard.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dashboard.WebApi.dll"]
