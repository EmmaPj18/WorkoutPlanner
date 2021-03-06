#
# copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app 
#
EXPOSE 3000
ENV ASPNETCORE_URLS=http://*:3000
#
COPY *.sln .
COPY WorkoutPlanner.API/*.csproj ./WorkoutPlanner.API/
COPY WorkoutPlanner.API.Tests/*.csproj ./WorkoutPlanner.API.Tests/
COPY WorkoutPlanner.Common/*.csproj ./WorkoutPlanner.Common/
COPY WorkoutPlanner.Common.Tests/*.csproj ./WorkoutPlanner.Common.Tests/
COPY WorkoutPlanner.Domain/*.csproj ./WorkoutPlanner.Domain/
COPY WorkoutPlanner.Services/*.csproj ./WorkoutPlanner.Services/
COPY WorkoutPlanner.Services.Tests/*.csproj ./WorkoutPlanner.Services.Tests/
COPY WorkoutPlanner.Infrastructure/*.csproj ./WorkoutPlanner.Infrastructure/
COPY WorkoutPlanner.Infrastructure.Tests/*.csproj ./WorkoutPlanner.Infrastructure.Tests/
COPY WorkoutPlanner.Migrations/*.csproj ./WorkoutPlanner.Migrations/
COPY WorkoutPlanner.Persistence/*.csproj ./WorkoutPlanner.Persistence/
#
RUN dotnet restore 

#
# copy everything else and build app
COPY WorkoutPlanner.API/. ./WorkoutPlanner.API/
COPY WorkoutPlanner.Common/. ./WorkoutPlanner.Common/
COPY WorkoutPlanner.Domain/. ./WorkoutPlanner.Domain/
COPY WorkoutPlanner.Services/. ./WorkoutPlanner.Services/
COPY WorkoutPlanner.Infrastructure/. ./WorkoutPlanner.Infrastructure/
COPY WorkoutPlanner.Migrations/. ./WorkoutPlanner.Migrations/
COPY WorkoutPlanner.Persistence/. ./WorkoutPlanner.Persistence/
#
WORKDIR /app/WorkoutPlanner.API
RUN dotnet publish -c Release -o out 

#
# Run application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app 
#
COPY --from=build /app/WorkoutPlanner.API/out ./
ENTRYPOINT ["dotnet", "WorkoutPlanner.API.dll"]