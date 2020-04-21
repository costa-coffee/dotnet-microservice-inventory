using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory.Models
{
    public class StoreInventoryItemListRequest
    {
        public StoreInventoryItemListRequest()
        {
        }

        public string Filters { get; set; }
    }

    public class StoreInventoryItemFilters
    {
        public StoreInventoryItemFilters()
        {
        }

        public StoreInventoryItemFilterSKU Sku { get; set; }
    }

    public class StoreInventoryItemFilterSKU
    {
        public StoreInventoryItemFilterSKU()
        {
        }

        public string Condition { get; set; }
        public string[] Value { get; set; }
    }
}
