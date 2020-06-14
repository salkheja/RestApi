
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace WebApi.Services
{
    /// <summary>
    /// This repository is used to create an abstraction layer between database access and business logic.
    /// Thereby usage of data, and access of data is separated where repository will manage interaction with database (Azure Cosmos DB). 
    /// This class represents the implementation of the Repository
    /// </summary>


    public class CosmosDbRepo : ICosmosDbRepo
    {
        private Container _container;

        public CosmosDbRepo(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Item t)
        {
            await this._container.CreateItemAsync<Item>(t, new PartitionKey(t.InternalId));
          
        }


        public async Task DeleteItemAsync(string internalid)
        {
            await this._container.DeleteItemAsync<Item>(internalid, new PartitionKey(internalid));
        }

        public async Task<Item> GetItemAsync(string internalid)
        {
            try
            {
                ItemResponse<Item> response = await this._container.ReadItemAsync<Item>(internalid, new PartitionKey(internalid));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Item>> GetItemsAsync(int pageNumber, int pageSize)
        {
            return this._container.GetItemLinqQueryable<Item>(true).AsEnumerable().Skip(pageNumber).Take(pageSize).ToList();
        }

        public async Task UpdateItemAsync(string internalid, Item item)
        {
            await this._container.UpsertItemAsync<Item>(item, new PartitionKey(internalid));
        }

     


    }
}


