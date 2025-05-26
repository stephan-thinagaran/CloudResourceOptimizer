# Advanced Environment Switching Guide

To use environment switching in REST Client, follow these steps:

## 1. Create a .http file with environment variables

```http
@dev = http://localhost:5176
@staging = https://staging-api.example.com
@prod = https://api.example.com

# Set active environment
@baseUrl = {{dev}}
```

## 2. Create a REST Client Environment file

1. Create a file named `.vscode/settings.json` in your project
2. Add this configuration:

```json
{
  "rest-client.environmentVariables": {
    "$shared": {
      "version": "v1"
    },
    "development": {
      "baseUrl": "http://localhost:5176",
      "subscriptionId": "390001b3-ed83-44ac-b78c-4c3184d3b401"
    },
    "staging": {
      "baseUrl": "https://staging-resource-optimizer.azurewebsites.net",
      "subscriptionId": "00000000-0000-0000-0000-000000000000"
    },
    "production": {
      "baseUrl": "https://resource-optimizer.azurewebsites.net",
      "subscriptionId": "00000000-0000-0000-0000-000000000000"
    }
  }
}
```

## 3. Switch environments

1. Press `Ctrl+Shift+P` to open the Command Palette
2. Type "Rest Client: Switch Environment"
3. Select an environment from the list

## 4. Use environment variables in requests

```http
GET {{baseUrl}}/api/resource
```

With this setup, you can easily switch between environments without changing your .http files.
