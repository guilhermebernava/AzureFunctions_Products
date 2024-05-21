# 📦 Azure Functions CRUD Project with .NET 8 and CosmosDB

Welcome to the Azure Functions CRUD project! This project demonstrates how to create serverless functions using .NET 8 to perform CRUD (Create, Read, Update, Delete) operations on products, with CosmosDB as the database.

## 🌟 Features

- 🚀 **Serverless Architecture**: Built with Azure Functions for a scalable and cost-effective solution.
- 💾 **CosmosDB Integration**: Efficiently stores and retrieves product data.
- 🔄 **CRUD Operations**: Comprehensive implementation of Create, Read, Update, and Delete functionalities.
- 🛠️ **.NET 8**: Leveraging the latest features and improvements in .NET 8.

## 📚 Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Endpoints](#endpoints)
- 
## 🚀 Getting Started

Follow these instructions to get the project up and running on your local machine.

### Prerequisites

- 🌐 [Azure Subscription](https://azure.microsoft.com/en-us/free/)
- 💻 .NET 8 SDK installed on your development machine
- 📦 [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local) for local development and testing
- 🛠️ [Azure CosmosDB Account](https://docs.microsoft.com/en-us/azure/cosmos-db/create-sql-api-dotnet) with a database and container set up

### Installation

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/azure-functions-crud.git
    cd azure-functions-crud
    ```

2. **Configure local settings**:
    Create a `local.settings.json` file in the root of your project with the following content:
    ```json
    {
        "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "CosmosDBEndpoint": "",
        "CosmosDBKey": "",
        "CosmosDBDatabaseName": "",
        "CosmosDBContainerName": ""
      }
    }
    ```

3. **Restore dependencies**:
    ```bash
    dotnet restore
    ```

4. **Run the functions locally**:
    ```bash
    func start
    ```

## 🛠️ Usage

### Endpoints

- **Create Product**
  - **URL**: `POST /api/product`
  - **Description**: Adds a new product to the CosmosDB.
  - **Body**: JSON representation of the product.
    ```json
    {
      "id": 1,
      "name": "name",
      "description": "description",
      "price": 100.0
    }

    ```

- **Get Products**
  - **URL**: `GET /api/product`
  - **Description**: Retrieves all products from the CosmosDB.

- **Get Product by ID**
  - **URL**: `GET /api/product/{id}`
  - **Description**: Retrieves a product by its ID from the CosmosDB.

- **Update Product**
  - **URL**: `PUT /api/product/{id}`
  - **Description**: Updates an existing product in the CosmosDB.
  - **Body**: JSON representation of the updated product.

- **Delete Product**
  - **URL**: `DELETE /api/product/{id}`
  - **Description**: Deletes a product by its ID from the CosmosDB.
