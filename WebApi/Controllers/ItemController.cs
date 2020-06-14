using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using WebApi.Services;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICosmosDbRepo _cosmosDbRepo;
        IConfiguration configuration;
        public ItemController(ICosmosDbRepo cosmosDbService, IMapper mapper, IConfiguration iConfig)
        {
            _cosmosDbRepo = cosmosDbService;
            _mapper = mapper;
            configuration = iConfig;
        }


        /// <summary>
        /// HTTP Post endpoint
        /// </summary>
        /// <param name="itemPayload"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Item>> PostAsync([FromBody] ItemPayload itemPayload)
        {

            Item item = _mapper.Map<Item>(itemPayload);

            await _cosmosDbRepo.AddItemAsync(item);

            return CreatedAtAction(nameof(PostAsync), new { id = item.Id }, item);
        }


        /// <summary>
        /// HTTP GET endpoint that has an optional route parameter (integer: start)
        /// that returns an array of those json objects from the store with a maximum of 5 entries.
        /// When the optional parameter (start) is a positive number,
        /// it shall skip that amount of entries from the store and return the next 5 entries.
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        [HttpGet("{start?}")]
        public async Task<IEnumerable<ItemResult>> Get(int? start)
        {
            int actualStart = start.HasValue && start.Value > 0 ? start.Value : 0;
            int size;
            int.TryParse(configuration.GetSection("MySettings").GetSection("PageSize").Value,out size);
            return _mapper.Map<IEnumerable<ItemResult>>(await _cosmosDbRepo.GetItemsAsync(actualStart, size));
        }
     
    }
}
