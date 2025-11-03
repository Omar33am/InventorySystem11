using System.Globalization;

namespace InventorySystem
{
    public class ItemSorterRobot : Robot
    {
        public const string UrscriptTemplate = @"
def move_item_to_shipment_box():
  SBOX_X = 0.5
  SBOX_Y = 0.1
  ITEM_X = {0}
  ITEM_Y = 0.1
  DOWN_Z = 0.10
  def moveto(x, y, z = 0.30):
    movel(p[x,y,z,0,0,0], a=0.5, v=0.25, r=0)
  moveto(ITEM_X, ITEM_Y, 0.30)
  moveto(ITEM_X, ITEM_Y, 0.20)
  moveto(ITEM_X, ITEM_Y, 0.30)
  moveto(SBOX_X, SBOX_Y, 0.30)
  moveto(SBOX_X, SBOX_Y, 0.20)
  moveto(SBOX_X, SBOX_Y, 0.30)
end

move_item_to_shipment_box()
";

        private static double ToRobotX(uint location) => 0.1 + (location - 1) * 0.2;

        public void PickUp(uint inventoryLocation)
        {
            var x = ToRobotX(inventoryLocation).ToString(CultureInfo.InvariantCulture);
            var program = string.Format(UrscriptTemplate, x);
            SendUrscript(program);
        }
    }
}