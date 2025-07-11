using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Spartan
{

    
    internal class MyInventory
    {
        private List<int> InventoryID;

        public MyInventory()
        {
            InventoryID = new List<int>();
        }

        public bool HasItem(int itemID)
        {
            return InventoryID.Contains(itemID);
        }

        public List<int> GetItemIDs()
        {
            return new List<int>(this.InventoryID);
        }

        public void AddItem(int itemid) // 상점에서 옮겨올때
        {
            InventoryID.Add(itemid);
        }

        public int GetItemID(int input)
        {
            if(input > 0 && input <= InventoryID.Count  )
            {
                return InventoryID[input-1];
            }
            else
            {
                return -1;
            }    
        }
    }
}
