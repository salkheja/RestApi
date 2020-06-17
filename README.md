# Rest API Sample in .Net Core 3.0 and Azure Cosmos DB
This repository is an implementation of a small REST API in .net core 3.0 using Azure Cosmos DB.
This API offers two endpoints:
- HTTP Post endpoint that retrieves a JSON Object in the Post Body.
- HTTP GET endpoint which has an optional route parameter ***integer: start*** that returns an array of json objects from the Azure Cosmos DB with a maximum of 5 entries. When the optional parameter ***start*** is a positive number, it shall skip that amount of entries from the store and return the next 5 entries.


## Getting Started
Clone or download the repository using this link [GitHub Repository](https://github.com/salkheja/RestApi.git)

### Prerequisites

- **Visual Studio 2019**
- **.Net Core 3.0**
- **Azure Consmos DB Emulator**



