using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Inventory.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory.Controllers
{
    [ApiController]
    [Route("")]
    public class StoreInventoryItemController : ControllerBase
    {
        private readonly ILogger<StoreInventoryItemController> _logger;
        private readonly InventoryContext _context;

        public StoreInventoryItemController(ILogger<StoreInventoryItemController> logger, InventoryContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("stores/{code}/inventory-items")]
        public async Task<List<Store>> Get(string code, [FromQuery]StoreInventoryItemListRequest request)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };

            options.Converters.Add(new JsonStringEnumConverter(new SnakeCaseNamingPolicy()));

            StoreInventoryItemFilters filters = JsonSerializer.Deserialize<StoreInventoryItemFilters>(request.Filters, options);
            return await _context.Store.ToListAsync();
        }

        [HttpPut("stores/{code}/inventory-items/{sku}")]
        public async Task<StatusCodeResult> Put(string code, string sku, [FromBody]StoreInventoryItemUpdateRequest update)
        {
            Store store = await FindOrCreateStoreAsync(code);
            InventoryItem item = await FindOrCreateInventoryItemAsync(sku);
            StoreInventoryItem storeInventoryItem = await FindOrCreateStockInventoryItemAsync(store, item);
            storeInventoryItem.Available = update.Available;

            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<Store> FindOrCreateStoreAsync(string code)
        {
            Store store = await _context.Store.SingleOrDefaultAsync(x => x.Code == code);
            if (store == null)
            {
                store = new Store { Code = code };
                _context.Attach(store);
                await _context.SaveChangesAsync();
            }

            return store;
        }

        private async Task<InventoryItem> FindOrCreateInventoryItemAsync(string sku)
        {
            InventoryItem item = await _context.InventoryItem.SingleOrDefaultAsync(x => x.Sku == sku);
            if (item == null)
            {
                item = new InventoryItem { Sku = sku };
                _context.Attach(item);
                await _context.SaveChangesAsync();
            }

            return item;
        }

        private async Task<StoreInventoryItem> FindOrCreateStockInventoryItemAsync(Store store, InventoryItem item)
        {
            StoreInventoryItem storeInventoryItem = await _context.StoreInventoryItem.SingleOrDefaultAsync(x => x.Store == store.Id && x.Item == item.Id);
            if (storeInventoryItem == null)
            {
                storeInventoryItem = new StoreInventoryItem { StoreNavigation = store, ItemNavigation = item };
                _context.Attach(storeInventoryItem);
                await _context.SaveChangesAsync();
            }

            return storeInventoryItem;
        }
    }
}
