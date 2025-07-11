using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Spartan
{
    internal class Shop
    {
        private List<int> Sellitems;
        public Shop()
        {
            Sellitems = new List<int>();
            Sellitems.Add(100);
            Sellitems.Add(101);
            Sellitems.Add(102);
            Sellitems.Add(200);
            Sellitems.Add(201);
            Sellitems.Add(202);
        }

        public List<int> GetShopID()
        {
            return new List<int>(this.Sellitems);
        }


        public bool SearchItem(int itemid)
        {
            return Sellitems.Contains(itemid);
        }

        public void BuyItem(int itemid)
        {
            Sellitems.Remove(itemid);
        }
    }
}
