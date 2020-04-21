using System;

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
        public StoreInventoryItemFilterUpdated Updated { get; set; }
    }

    public class StoreInventoryItemFilterSKU
    {
        public StoreInventoryItemFilterSKU()
        {
        }

        public Condition Condition { get; set; }
        public string[] Value { get; set; }
    }

    public class StoreInventoryItemFilterUpdated
    {
        public StoreInventoryItemFilterUpdated()
        {
        }

        public Condition Condition { get; set; }
        public DateTime Value { get; set; }
    }

    public enum Condition
    {
        In,
        Gte
    }
}
