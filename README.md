# Azure CDN Project

Project for static website using Azure CDN, Azure Functions and Cosmos DB.

## Documentation

### Requirements

- Install .NET 3.1
- Install Azure Functions Core Tools

### Cloning templates and making changes to the index.html file

First cloned the cgc repo and copied only the frontend and backend folder to the current repository.

Then I opened the index.html file to change all the personal information that was inside to match my personal information.

### Frontend

First step is to create a `main.js` file to the main folder.

The purpose initially is to add to a counter that will go up in value each time the main page of the website is reloaded and also display that number on the same page.

To do this, I specified a function called `getVisitCount` that has a initial `counter` variable that gets updated through a `response` that populates a JSON document. Then, inside the document, the method `getElementById` will return a reference to the first value `counter` encountered in the document. At the end, there is a error handling condition.

### Backend

#### Setting up Cosmos DB

Create a new Cosmos DB account, the API set to Core (SQL) and capacity mode set to Serverless.

Go to the new resource, open the Data Explorer page. Go to New Database and create a new database inside the account. Select the new database and create a new container in it.

Inside the database, go to the container created to store the counter variable values. Add a new item so that the database has a initial `count` value of zero, since there has not been any visitors to the website yet.

```bash
{
    "id": "1",
    "count": 0 
}
```

#### Setting up the Azure Function

Using the Azure Functions Core Tools extension for VSCode I am able to create functions inside VSCode.

I created the function inside the resourcegroup `azurecdnproject`, where it is also the Cosmos DB. I created the function and called it `GetContentLoadCounter`. The `.cs` is populated automatically.

So I went do the `backed/api` directory and ran on the terminal the following command to start the Azure Function:

```bash
func host start
```

since I'm using a GitHub Codespace for this project, after I run this command, a pop-up appeared on the bottom right asking if I wanted to see it in the localhost:7071. If I try to access through the host I will not be able to access it because it is using the codespace host. So the only option to accesss it is through this link.

#### Adding Cosmos DB trigger and bindings for Azure Function

Going to the page on [Cosmos DB triggers](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2?tabs=in-process%2Cfunctionsv2&pivots=programming-language-csharp) and go to the Install extension. There the following code must be added to the `host.json`:

```json
{
    "version": "2.0",
    "extensionBundle": {
        "id": "Microsoft.Azure.Functions.ExtensionBundle",
        "version": "[3.3.0, 4.0.0)"
    }
}
```

At the end of the page, there is going to be an example o [Azure Functions trigger and binding example](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-example?tabs=csharp).

Make sure that your are at the `backend/api` folder first. Another way to do it is through manually installing extensions. For Azure Cosmos DB, the code to install the extension is:

```bash
dotnet add package Microsoft.Azure.WebJobs.Extensions.CosmosDB --version 3.0.10
```

Open the `local.settings.json` file and add the following line that will contain the key to Cosmos DB account. Then go to the Azure portal inside the Cosmos DB>Keys and copy the primary connection string.

```json
"AzureResumeConnectionString": "key"
```

After that, create a file called `Counter.cs` inside the `backed/api` folder, which will be the C# class to describe the counter option. There it will reference the same columns name contained in the table in the database.

#### Creating the binding

Open the `GetContentLoadCounter.cs` file.

