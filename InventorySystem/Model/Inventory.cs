using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Model
{
    public class Inventory
    {
        public Dictionary<Item, decimal> Stock { get; } = new();

        public void Add(Item item, decimal qty)
        {
            if (!Stock.ContainsKey(item)) Stock[item] = 0;
            Stock[item] += qty;
        }

        public bool Remove(Item item, decimal qty)
        {
            if (!Stock.ContainsKey(item) || Stock[item] < qty) return false;
            Stock[item] -= qty;
            return true;
        }

        public List<Item> LowStockItems(decimal threshold = 5)
            => Stock.Where(p => p.Value < threshold).Select(p => p.Key).ToList();
    }
}