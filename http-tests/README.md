# HTTP Client Tests

This directory contains HTTP client test files for testing the CloudResourceOptimizer API.

## Requirements

- VS Code with REST Client extension installed (https://marketplace.visualstudio.com/items?itemName=humao.rest-client)

## Usage

1. Make sure your application is running
2. Open one of the `.http` files
3. Click "Send Request" above any request
4. View the response in the split panel

## Files

- `environments.http`: Contains environment variables and base URLs
- `optimization.http`: Tests for resource optimization endpoints
- `environments-api.http`: Tests for environment/subscription endpoints
- `notifications.http`: Tests for notification subscription endpoints

## Environment Configuration

To switch between environments:
1. Open VS Code Command Palette (Ctrl+Shift+P)
2. Type "Rest Client: Switch Environment"
3. Select "dev", "staging", or "production"

Or edit the `.env.json` file to customize environment settings.
