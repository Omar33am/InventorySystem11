using System.Collections.Generic;

namespace InventorySystem.Model
{
    public class OrderBook
    {
        public Queue<Order> QueuedOrders { get; } = new();
        public List<Order> ProcessedOrders { get; } = new();
        public decimal TotalRevenue { get; private set; } = 0;
        private readonly Inventory _inventory;
        public OrderBook(Inventory inventory) { _inventory = inventory; }

        public void QueueOrder(Order order) => QueuedOrders.Enqueue(order);

        public Order? ProcessNextOrder()
        {
            if (QueuedOrders.Count == 0) return null;
            var order = QueuedOrders.Peek();

            foreach (var line in order.OrderLines)
            {
                if (!_inventory.Stock.ContainsKey(line.Item)) return null;
                if (_inventory.Stock[line.Item] < line.Quantity) return null;
            }

            foreach (var line in order.OrderLines) _inventory.Remove(line.Item, line.Quantity);

            TotalRevenue += order.TotalPrice();
            ProcessedOrders.Add(order);
            QueuedOrders.Dequeue();
            return order;
        }
    }
}