using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using InventorySystem.Model;

namespace InventorySystem
{
    public partial class MainWindow : Window
    {
        private Inventory _inventory = null!;
        private OrderBook _orderBook = null!;
        private UnitItem _item1 = null!;
        private UnitItem _item2 = null!;
        private UnitItem _item3 = null!;
        private readonly ItemSorterRobot _robot = new();

        public MainWindow()
        {
            InitializeComponent();
            SetupData();
            RefreshUI();
            ProcessButton.Click += ProcessButton_Click;
        }

        private void SetupData()
        {
            _item1 = new UnitItem("M3 screw", 1m, inventoryLocation: 1);
            _item2 = new UnitItem("M3 nut", 1.5m, inventoryLocation: 2);
            _item3 = new UnitItem("pen", 1m, inventoryLocation: 3);

            _inventory = new Inventory();
            _inventory.Add(_item1, 50);
            _inventory.Add(_item2, 50);
            _inventory.Add(_item3, 50);

            _orderBook = new OrderBook(_inventory);

            var orderLine1 = new OrderLine(_item1, 1);
            var orderLine2 = new OrderLine(_item2, 2);
            var orderLine3 = new OrderLine(_item3, 1);
            var order1 = new Order { OrderLines = { orderLine1, orderLine2, orderLine3 }, Time = System.DateTime.Now - System.TimeSpan.FromDays(2) };

            var order2 = new Order { OrderLines = { new OrderLine(_item2, 1) }, Time = System.DateTime.Now };

            var customer1 = new Customer("Ramanda");
            var customer2 = new Customer("Totoro");
            customer1.CreateOrder(_orderBook, order1);
            customer2.CreateOrder(_orderBook, order2);
        }

        private async void ProcessButton_Click(object? sender, RoutedEventArgs e)
        {
            var processed = _orderBook.ProcessNextOrder();
            if (processed == null)
            {
                StatusMessages.Text += "Cannot process: empty queue or not enough stock\n";
                return;
            }

            StatusMessages.Text += "Processing order\n";
            foreach (var orderLine in processed.OrderLines)
            {
                for (var i = 0; i < orderLine.Quantity; i++)
                {
                    StatusMessages.Text += $"Picking up {orderLine.Item.Name}\n";
                    if (orderLine.Item is UnitItem ui)
                        _robot.PickUp(ui.InventoryLocation);
                    await Task.Delay(9500);
                }
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            QueuedOrdersList.ItemsSource = _orderBook.QueuedOrders.ToList();
            ProcessedOrdersList.ItemsSource = _orderBook.ProcessedOrders
                .Select(p => $"{p.TotalPrice():0.00} kr | {p.Time:MM/dd/yyyy HH:mm:ss}")
                .ToList();
            RevenueText.Text = $"{_orderBook.TotalRevenue:0.00} kr";
        }
    }
}

