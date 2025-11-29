# CloudComputingAPI
This project was done for my university course cloud computing.
To run this project you need a mssql server instance.
The database gets created on launch.

## Environment variables

### ASPNETCORE_ENVIRONMENT

Runtime environement

Development, Staging, Production

### WeatherDbConnectionEnv

The connectionstring of the mssql database.
Only needed in production and staging.
All other deployments use the connection string in the appsettings.

### Options

Port 80 is exposed.
So if you want to change the port to access the webapp, simply add -p HOST_PORT:Container_PORT to docker run.

## Settings.json

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedCorsHosts": [
    "http://localhost:4200"
  ],
  "ConnectionStrings": {
    "WeatherDbConnection": "Your MSSQL conenction string"
  },
  "Serilog": {
    "Using": [ "Serilog.Sinks.File", "Serilog.Sinks.Console" ],
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] {Message}{NewLine:1}{Exception:1}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "C:\\Logs\\Weather.Api\\Weather.Api.log-.txt",
          "rollingInterval": "Day",
          "Timestamp": "yyyy-MM-dd HH:mm:ss",
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] {Message}{NewLine:1}{Exception:1}"
        }
      }
    ]
  }
}
```
