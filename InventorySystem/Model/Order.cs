using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Model
{
    public class Order
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public List<OrderLine> OrderLines { get; } = new();
        public decimal TotalPrice() => OrderLines.Sum(l => l.LinePrice());
        public override string ToString()
        {
            var first = OrderLines.Count > 0 ? OrderLines[0].ToString() : "(empty)";
            var extra = OrderLines.Count > 1 ? $" +{OrderLines.Count - 1} more" : "";
            return $"{first}{extra} | {Time:MM/dd/yyyy HH:mm:ss}";
        }
    }
}