# Workout Planner

Tracking your workouts or your members's' workouts on a single app.

## Getting Started

### Tech Stack

* [Sql Server](https://www.microsoft.com/en-us/sql-server/)
* [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
    * [Minimal API](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
* [Dotnet EF](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
* [FastEndpoints](https://fast-endpoints.com/)
* [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Swashbuickle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

### Dependencies

* Docker

### Installing

* [Install Docker](https://docs.docker.com/install/)
* Run this command to create the image from the base directory, where the .sln file and dockerfile are, and test that is everything ok
```
docker build -t workout_planner_api .
```

### Executing program

* Run this command to run it from docker compose at the same directory to get it up and 
```
docker compose up
```

## Authors

[@EmmaPj18](https://twitter.com/EmmaPJ18)

## Version History

* 0.1
    * Initial Release

## Acknowledgments