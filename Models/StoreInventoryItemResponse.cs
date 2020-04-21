using System;

namespace Inventory.Models
{
    public class StoreInventoryItemResponse
    {
        public string Store { get; set; }
        public string Item { get; set; }
        public bool Available { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
