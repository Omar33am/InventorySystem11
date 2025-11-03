namespace InventorySystem.Model
{
    public class OrderLine
    {
        public Item Item { get; set; }
        public decimal Quantity { get; set; }
        public OrderLine(Item item, decimal quantity) { Item = item; Quantity = quantity; }
        public decimal LinePrice() => Item.PricePerUnit * Quantity;
        public override string ToString() => $"{Item.Name} x {Quantity}";
    }
}