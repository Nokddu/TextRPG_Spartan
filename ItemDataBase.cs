using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Spartan
{
    internal class ItemDataBase
    {
        private static Dictionary<int, Items> ItemID;

        static ItemDataBase()
        {
            ItemID = new Dictionary<int, Items>();

            // 갑옷의 ID
            ItemID.Add(100, new Items("무쇠갑옷", "무쇠로 만들어진 튼튼한 갑옷입니다.", ItemType.armor, 0, 9,2000));
            ItemID.Add(101, new Items("수련자 갑옷", "수련에 도움을 주는 갑옷입니다", ItemType.armor, 0, 5,1000));
            ItemID.Add(102, new Items("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다",ItemType.armor,0,15,3500 ));
            ItemID.Add(119, new Items("숨겨져 있는 히든 방어구" ,"아무도 존재를 몰랐지만 이젠 알수있다. 어떤가 나의 존재가..",ItemType.armor, 5, 50,0));

            // 무기의 ID
            ItemID.Add(200, new Items("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다",ItemType.Weapon, 7, 0,3500));
            ItemID.Add(201, new Items("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다",ItemType.Weapon,2,0,600));
            ItemID.Add(202, new Items("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다", ItemType.Weapon, 5, 0,1500));
            ItemID.Add(219, new Items("숨겨져 있는 히든 무기", "아무도 존재를 몰랐지만 이젠 알수있다. 어떤가 나의 존재가..", ItemType.Weapon, 50, 5, 0));
        }

        public static Items GetID(int id)
        {
            return ItemID[id].Clone();
        }
    }
}
