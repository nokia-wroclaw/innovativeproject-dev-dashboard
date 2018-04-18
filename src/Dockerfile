FROM microsoft/dotnet:2.1.300-preview2-sdk AS base
WORKDIR /app
EXPOSE 5001

FROM microsoft/dotnet:2.1.300-preview2-sdk AS build
WORKDIR /src
COPY Dashboard.sln ./
COPY Dashboard.WebApi/Dashboard.WebApi.csproj Dashboard.WebApi/
COPY Dashboard.Application/Dashboard.Application.csproj Dashboard.Application/
COPY Dashboard.Core/Dashboard.Core.csproj Dashboard.Core/
COPY Dashboard.Data/Dashboard.Data.csproj Dashboard.Data/
COPY Dashboard.UnitTests/Dashboard.UnitTests.csproj Dashboard.UnitTests/
COPY . .
RUN dotnet restore -nowarn:msb3202,nu1503 ./Dashboard.sln
WORKDIR /src/Dashboard.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ["dotnet", "Dashboard.WebApi.dll"]
