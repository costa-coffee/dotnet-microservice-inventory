using System;

namespace Inventory.Models
{
    public class StoreInventoryItemTransferRequest
    {
        public Item[] Items { get; set; }
    }

    public class Item
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public string[] Modifiers { get; set; }
    }
}
