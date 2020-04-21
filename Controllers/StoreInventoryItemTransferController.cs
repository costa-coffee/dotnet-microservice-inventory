using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System.Net.Http;

namespace Inventory.Controllers
{
    [ApiController]
    [Route("")]
    public class StoreInventoryItemTransferController : ControllerBase
    {
        public StoreInventoryItemTransferController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost("stores/{code}/inventory-item-transfers")]
        public async Task<StatusCodeResult> Post(string code, [FromBody]StoreInventoryItemTransferRequest transfer)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            return Ok();
        }

        private readonly IHttpClientFactory _clientFactory;
    }
}
