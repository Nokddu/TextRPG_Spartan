using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Spartan
{
    internal class Equipment
    {
        private Dictionary<ItemType, int> equipitems;
        private Player owner;

        public Equipment(Player owner) // 생성할때 주인님을 기억한다고 한다.
        {
            this.owner = owner;
            equipitems = new Dictionary<ItemType, int>();
        }

        public void EquipItem(int itemID)
        {
            Items item = ItemDataBase.GetID(itemID);
            equipitems[item.Type] = itemID;

            owner.UpdateStats();
        }

        public void UnEquipItem(ItemType type)
        {
            if(equipitems.ContainsKey(type))
            {
                equipitems.Remove(type);
            }

            owner.UpdateStats();
        }

        public void UnEquipItem(int itemID)
        {
            Items itemType = ItemDataBase.GetID(itemID);
            ItemType type = itemType.Type;

            if(equipitems.ContainsKey(type) && equipitems[type] == itemID)
            {
                UnEquipItem(type);
            }
        }

        public bool IsEquped(int itemID)
        {
            return equipitems.ContainsValue(itemID);
        }

        public List<int> GetEquippedItemIDs()
        {
            return equipitems.Values.ToList();
        }


    }
}
