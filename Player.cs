using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Spartan
{
    public enum Job
    {
        Warrior,
        Archer
    }

    internal class Player
    {
        
        public string Name { get; private set; }
        public int BaseLevel { get; private set; }
        public int BaseHP { get; private set; }
        public int BaseAttack { get; private set; }
        public int BaseDefend { get; private set; }
        public int BaseGold { get; private set; }

        public int TotalLevel { get; private set; }
        public int TotalHP { get; private set; }
        public int TotalAttack { get; private set; }
        public int TotalDefend { get; private set; }
        public int TotalGold { get; private set; }

        public Job currentState { get; private set; }

        public MyInventory Inventory { get; private set; }
        public Equipment equipment { get; private set; }
        public Shop shop { get; private set; }
        public Player()
        {
            Inventory = new MyInventory();
            equipment = new Equipment(this);
            shop = new Shop();
        }

        public void PlayerSet(string name)
        {
            Name = name;
            TotalGold = BaseGold;
            TotalHP = BaseHP;
            TotalLevel = BaseLevel;
            TotalAttack = BaseAttack;
            TotalDefend = BaseDefend;
        }

        public void DecreaseGold(int itemGold)
        {
            TotalGold -= itemGold;
        }

        // 얘를 먼저 해야됨
        public void Sell_Item(int itemid)
        {
            Items item = ItemDataBase.GetID(itemid);
            TotalGold += (item.Gold * 85) / 100;
        }

        // 현재 장착 하고 해제 할 경우 아래가 실행이 된다.
        public void UpdateStats()
        {
            TotalAttack = BaseAttack;
            TotalDefend = BaseDefend;

            List<int> equipID = equipment.GetEquippedItemIDs();

            foreach (var equip in equipID)
            {
                Items item = ItemDataBase.GetID(equip);

                TotalAttack += item.Attack;
                TotalDefend += item.Defend;

            }
        }

        public void SelectJob(Job job)
        {
            currentState = job;

           if(job == Job.Warrior)
            {
                BaseLevel = 1;
                BaseHP = 100;
                BaseAttack = 10;
                BaseDefend = 5;
                BaseGold = 1500;
            }
            else if (job == Job.Archer)
            {
                BaseLevel = 1;
                BaseHP = 80;
                BaseAttack = 15;
                BaseDefend = 3;
                BaseGold = 1500;
            }
        }

        public void HealHP(int heal)
        {
            int nowHP = BaseHP - TotalHP;
            if(nowHP < heal)
            {
                TotalHP += nowHP;
            }
            else
            {
                TotalHP += heal;
            }
        }
    }
}
