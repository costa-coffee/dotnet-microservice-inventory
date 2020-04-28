using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using WRS;
using System.Net.Http;
using System.Linq;

namespace Inventory.Controllers
{

    [ApiController]
    [Route("")]
    public class StoreInventoryItemTransferController : ControllerBase
    {
        public StoreInventoryItemTransferController(WRSClient client)
        {
            _Client = client;
        }

        private readonly WRSClient _Client;

        [HttpPost("stores/{code}/inventory-item-transfers")]
        public async Task<StatusCodeResult> Post(string code, [FromBody]StoreInventoryItemTransferRequest transfer)
        {
            var payload = new StockTransferRequest
            {
                Store = code,
                Items = transfer.Items.Select((a, index) => new StockTransferItem
                {
                    Code = a.Sku,
                    Quantity = a.Quantity,
                    Modifiers = a.Modifiers,
                })
                .ToArray()
            };

            var response = await _Client.CreateStockTransfer(payload);
            var body = await response.Content.ReadAsStringAsync();

            return Ok();
        }
    }
}
