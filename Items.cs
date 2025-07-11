using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Spartan
{
    public enum ItemType
    {
        Weapon,
        armor
    }

    internal class Items
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ItemType Type { get; private set; }
        public int Attack { get; private set; }
        public int Defend { get; private set; }
        public int Gold { get; private set; }
        public Items(string name, string description, ItemType itemType, int attack, int defend,int gold)
        {
            Name = name;
            Description = description;
            Type = itemType;
            Attack = attack;
            Defend = defend;
            Gold = gold;
        }

        public Items Clone()
        {
            return new Items(this.Name,this.Description,this.Type,this.Attack,this.Defend,this.Gold);
        }
    }
}
