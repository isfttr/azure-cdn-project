using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;


namespace Company.Function
{
    public static class NewBaseType
    {
        [FunctionName("GetLoadCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "azurecdnproject",
                collectionName: "Counter",
                ConnectionStringSetting = "CosmosDBConnectionString",
                Id = "1",
                PartitionKey = "1")] Counter counter,

            [CosmosDB(
                databaseName: "azurecdnproject",
                collectionName: "Counter",
                ConnectionStringSetting = "CosmosDBConnectionString",
                Id = "1", PartitionKey = "1")] out Counter updatedCounter,

            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            updatedCounter = counter;
            updatedCounter.count += 1;

            var JsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
