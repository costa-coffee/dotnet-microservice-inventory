using System;

namespace Inventory
{
    public partial class StoreInventoryItem
    {
        public long Id { get; set; }
        public long Store { get; set; }
        public long Item { get; set; }
        public bool Available { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual InventoryItem ItemNavigation { get; set; }
        public virtual Store StoreNavigation { get; set; }
    }
}
