using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    /// <summary>
    /// This repository is used to create an abstraction layer between database access and business logic.
    /// Thereby usage of data, and access of data is separated where repository will manage interaction with database (Azure Cosmos DB). 
    /// </summary>
    
    public interface ICosmosDbRepo
    {
        Task<IEnumerable<Item>> GetItemsAsync(int pageNumber, int pageSize);
        Task<Item> GetItemAsync(string internalid);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(string internalid, Item item);
        Task DeleteItemAsync(string internalid);
    }
}
