#!/bin/bash
echo $(ls)
dotnet restore /code/Dashboard.sln
dotnet test /code/Dashboard.UnitTests/Dashboard.UnitTests.csproj