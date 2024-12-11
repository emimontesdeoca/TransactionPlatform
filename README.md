# TransactionPlatform Solution

## Overview

The TransactionPlatform solution is a distributed application composed of multiple projects, including an API service, a client, and an application host. This solution is designed to run on .NET 9.0 and leverages the Aspire framework for hosting and service management.

## Project Structure

The solution consists of the following projects:

- **TransactionPlatform.ApiService**: This project contains the API service for the TransactionPlatform.
- **TransactionPlatform.AppHost**: This project serves as the application host, orchestrating the various services.
- **TransactionPlatform.Client**: This project contains the client application for the TransactionPlatform.
- **TransactionPlatform.ServiceDefaults**: This project contains default configurations and services for the TransactionPlatform.

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or later

### Building the Solution

To build the solution, open the `TransactionPlatform.sln` file in Visual Studio and build the solution using the Build menu or by pressing `Ctrl+Shift+B`.

### Running the Application

To run the application, you can start the `TransactionPlatform.AppHost` project. This project is configured to run the API service and the client with the specified replicas.

#### Running with HTTPS

To run the application with HTTPS, use the following command:

#### Running with HTTPS

Running with HTTP
To run the application with HTTP, use the following command:

`dotnet run --project [TransactionPlatform.AppHost](http://_vscodecontentref_/1) --launch-profile http`

### Configuration
The application settings are configured in the appsettings.json and appsettings.Development.json files located in each project directory. You can modify these files to change the configuration settings.

### Launch Settings
The launch settings for each project are defined in the Properties/launchSettings.json file. These settings include environment variables, application URLs, and other configurations for running the projects.

## Project References
The `TransactionPlatform.AppHost` project references the following projects:

- `TransactionPlatform.ApiService`
- `TransactionPlatform.Client`


## Dependencies
The solution uses the following NuGet packages:

- `Aspire.Hosting.AppHost` (Version 9.0.0-rc.1.24511.1)

## License
This project is licensed under the MIT License. See the LICENSE file for more details.
