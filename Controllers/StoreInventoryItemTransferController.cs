using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using WRS;
using System.Net.Http;

namespace Inventory.Controllers
{
    [ApiController]
    [Route("")]
    public class StoreInventoryItemTransferController : ControllerBase
    {
        [HttpPost("stores/{code}/inventory-item-transfers")]
        public async Task<StatusCodeResult> Post(string code, [FromBody]StoreInventoryItemTransferRequest transfer)
        {
            var client = new WRSClient(
                "https://coseq-uat01.datasym.co.uk:44333/StockAPIUAT/",
                "5D310E47-50D9-4B80-B345-31622C70BC09",
                "23df81fa-928c-4e37-a5c2-dd2af7a97196"
            );

            var response = await client.Authenticate();

            return Ok();
        }
    }
}
