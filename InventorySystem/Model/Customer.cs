using System.Collections.Generic;

namespace InventorySystem.Model
{
    public class Customer
    {
        public string Name { get; set; }
        public List<Order> Orders { get; } = new();
        public Customer(string name) { Name = name; }
        public void CreateOrder(OrderBook book, Order order)
        {
            Orders.Add(order);
            book.QueueOrder(order);
        }
    }
}