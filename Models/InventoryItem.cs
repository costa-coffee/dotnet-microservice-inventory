using System;
using System.Collections.Generic;

namespace Inventory
{
    public partial class InventoryItem
    {
        public InventoryItem()
        {
            StoreInventoryItems = new HashSet<StoreInventoryItem>();
        }

        public long Id { get; set; }
        public string Sku { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<StoreInventoryItem> StoreInventoryItems { get; set; }
    }
}
