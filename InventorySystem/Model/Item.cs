namespace InventorySystem.Model
{
    public abstract class Item
    {
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }

        protected Item(string name, decimal pricePerUnit)
        {
            Name = name;
            PricePerUnit = pricePerUnit;
        }

        public override string ToString() => Name;
    }

    public class BulkItem : Item
    {
        public string MeasurementUnit { get; set; }

        public BulkItem(string name, decimal pricePerUnit, string unit)
            : base(name, pricePerUnit)
        {
            MeasurementUnit = unit;
        }
    }

    public class UnitItem : Item
    {
        public decimal Weight { get; set; }
        public uint InventoryLocation { get; set; }

        public UnitItem(string name, decimal pricePerUnit, decimal weight = 0, uint inventoryLocation = 1)
            : base(name, pricePerUnit)
        {
            Weight = weight;
            InventoryLocation = inventoryLocation;
        }
    }
}