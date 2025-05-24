# Serilog and Seq Integration

This project implements structured logging using Serilog with multiple sinks including Seq for centralized log analysis.

## Features

- **Console Logging**: Real-time logs in the console with custom formatting
- **File Logging**: Persistent logs stored in `logs/` directory with daily rolling
- **Seq Integration**: Centralized structured logging for enhanced debugging and monitoring
- **Structured Logging**: Rich context and properties for better log analysis

## Setup

### 1. Install Seq (Local Development)

#### Option A: Using Docker (Recommended)
```bash
docker-compose up -d
```

#### Option B: Direct Installation
Download and install Seq from [https://datalust.co/seq](https://datalust.co/seq)

### 2. Access Seq Dashboard
- URL: http://localhost:5341
- Default credentials: No authentication required for local development

### 3. Run the Application
```bash
dotnet run --project src/CloudResourceOptimizer.API
```

## Configuration

Logging configuration is defined in `appsettings.json`:

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.AspNetCore": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day"
        }
      },
      {
        "Name": "Seq",
        "Args": {
          "serverUrl": "http://localhost:5341"
        }
      }
    ]
  }
}
```

## Log Levels

- **Fatal**: Application crashes or critical errors
- **Error**: Error conditions that need attention
- **Warning**: Potentially harmful situations
- **Information**: General application flow information
- **Debug**: Detailed information for debugging
- **Verbose**: Very detailed trace information

## Example Usage

The application includes structured logging throughout:

```csharp
_logger.LogInformation("Starting resource optimization process");
_logger.LogInformation("Analyzing costs for {ResourceCount} resources", resources.Count);
_logger.LogError(ex, "Failed to fetch Azure resources");
```

## Benefits of Seq

1. **Structured Query Language**: Query logs using SQL-like syntax
2. **Real-time Monitoring**: Live log streaming and alerts
3. **Rich Filtering**: Filter by properties, time ranges, and log levels
4. **Dashboards**: Create custom dashboards for monitoring
5. **Correlation**: Track requests across multiple services

## Troubleshooting

### Seq Connection Issues
- Ensure Seq is running on port 5341
- Check Docker container status: `docker ps`
- Verify network connectivity to localhost:5341

### Log File Permissions
- Ensure the application has write permissions to the `logs/` directory
- Check disk space availability

### Missing Logs
- Verify log level configuration in appsettings.json
- Check if Serilog is properly configured in Program.cs
- Ensure using statements include proper Serilog namespaces
