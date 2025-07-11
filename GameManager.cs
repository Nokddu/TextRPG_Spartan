using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG_Spartan
{
    public class GameManager
    {
        private Player player;
        private Shop shop;
        public GameManager()
        {
            this.player = new Player();
            shop = new Shop();
        }
        
        // 인트로 화면 간단하게 플레이어의 이름의 정보를 받고 넘겨준다.
        public void Intro()
        {
            Console.WriteLine("==================================");
            Console.WriteLine("    이상한 곳에서 눈을 떳다..");
            Console.WriteLine("정신을 차리고 앞에 비석을 읽어본다..");
            Console.WriteLine("개발자가 되고싶은자 나에게로..");
            Console.WriteLine("");
            Console.WriteLine("                     -스파르탄");
            Console.WriteLine("==================================");
            Console.WriteLine("다음 장으로 넘기려면 아무키나 눌러주세요");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("머리가 깨질듯이 아프다...");
            Console.WriteLine("이것은 아마 과거의 기억인것 같다...");
            Console.WriteLine($"1. 당신은 아마 검술을 쓰는 전사였던거같다.");
            Console.WriteLine($"2. 당신은 아마 활을 사용하는 궁수 였던거같다. ");
            Console.WriteLine("");
            bool isJobselect = false;
            while (isJobselect == false)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();

                switch (Select)
                {
                    case "1":
                        player.SelectJob(Job.Warrior);
                        isJobselect = true;
                        break;
                    case "2":
                        player.SelectJob(Job.Archer);
                        isJobselect = true;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;
                }
            }
            //
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine(" 당신의 이름은 ??? : ");
            Console.WriteLine("==================================");
            player.PlayerSet(Console.ReadLine());
        }

        // 메인메뉴 _State 라는 enum 값을 리턴해줘서 현재 스테이트를 정한다.
        public _State Menu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다");
            Thread.Sleep(500);
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Thread.Sleep(500);
            Console.WriteLine("\n1. 상태보기\n2. 인벤토리\n3. 상점\n4. 휴식하기");


            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();

                switch(Select)
                {
                    case "1":
                        return _State.Status;
                    case "2":
                        return _State.inventory;
                    case "3":
                        return _State.shop;
                    case "4":
                        return _State.Rest;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;
                }
            }
        }

        // 스테이터스 메뉴 여기도 똑같이 _State 라는 enum 값을 리턴해서 현재 스테이트를 정한다.
        public _State ShowMyStatus()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            List<int> InventoryID = player.Inventory.GetItemIDs();

            Console.WriteLine($"\nLv. {player.TotalLevel}");
            Console.WriteLine($"\n{player.Name}  {player.currentState}");

            if (player.TotalAttack > player.BaseAttack || player.TotalDefend > player.BaseDefend)
            {
                Console.WriteLine($"\n공격력 : {player.TotalAttack} +[{player.TotalAttack - player.BaseAttack}]\n방어력 : {player.TotalDefend} +[{player.TotalDefend - player.BaseDefend}]");
            }
            else
            {
                Console.WriteLine($"\n공격력 : {player.TotalAttack}\n방어력 : {player.TotalDefend} ");
            }
            Console.WriteLine($"체력 : {player.TotalHP}");
            Console.WriteLine($"Gold : {player.TotalGold}");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");


            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();


                if (Select == "0")
                {
                    return _State.main;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 한번 입력해주세요");
                }

            }
        }

        public _State Inventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            List<int> InventoryID = player.Inventory.GetItemIDs();
            foreach (var itemID in InventoryID)
            {
                Items item = ItemDataBase.GetID(itemID);
                if(player.equipment.IsEquped(itemID))
                {
                    Console.WriteLine($"-[E]{item.Name}   | 공격력 +{item.Attack}  | 방어력 +{item.Defend}    | {item.Description}");
                }
                else
                {
                    Console.WriteLine($"-{item.Name}   | 공격력 +{item.Attack}  | 방어력 +{item.Defend}    | {item.Description}");
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("1.");
            Console.ResetColor();
            Console.WriteLine(" 장착 관리");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("0.");
            Console.ResetColor();
            Console.WriteLine(" 나가기");
            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();

                switch (Select)
                {
                    case "0":
                        return _State.main;
                    case "1":
                        return _State.equipment;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;
                }
            }
        }


        public _State EquipMentPage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            List<int> InventoryID = player.Inventory.GetItemIDs();
            int count = 1;
            foreach (var itemID in InventoryID)
            {
                Items item = ItemDataBase.GetID(itemID);
                if (player.equipment.IsEquped(itemID))
                {
                    Console.WriteLine($"{count}.[E]{item.Name}  | 공격력 : +{item.Attack}    | 방어력 +{item.Defend}    | {item.Description}");
                }
                else
                {
                    Console.WriteLine($"{count}. {item.Name}   | 공격력 : +{item.Attack}  | 방어력 +{item.Defend}    | {item.Description}");
                }
                count++;
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("0.");
            Console.WriteLine(" 나가기");
            Console.ResetColor();
            while (true)
            {
                Console.WriteLine("\n장착하고 싶은 아이템의 번호를 적어주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();
                if(!int.TryParse(Select, out int convert))
                {
                    Console.WriteLine("숫자를 입력해주세요");
                    continue;
                }

                if(convert == 0)
                {
                   return _State.inventory;
                }

                if(convert > 0 && convert <= InventoryID.Count)
                {
                    int targetID = InventoryID[convert - 1];

                    if (player.equipment.IsEquped(targetID))
                    {
                        player.equipment.UnEquipItem(targetID);
                    }
                    else
                    {
                        player.equipment.EquipItem(targetID);
                    }
                }

                Thread.Sleep(500);
                return _State.equipment;
            }
        }
       

        public _State Shop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.TotalGold}G\n\n");
            Console.WriteLine("아이템 목록");
            // 아이템 목록 보여주는 코드 foreach면 될거같은데.
            //리스트 참조형 변수
            List<int> shopID = shop.GetShopID();
            //foreach로 아이템목록 추가
            foreach(var itemId in shopID)
            {
                Items item = ItemDataBase.GetID(itemId);
                Console.WriteLine($"-{item.Name}     | 방어력 {item.Defend} | {item.Description}          |  {item.Gold}G");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("1. ");
            Console.ResetColor();
            Console.WriteLine("아이템 구매");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("2.");
            Console.ResetColor();
            Console.WriteLine(" 아이템 판매");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("0.");
            Console.ResetColor();
            Console.WriteLine(" 나가기");
            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();

                switch (Select)
                {
                    case "0":
                        return _State.main;
                    case "1":
                        return _State.buyshop;
                    case "2":
                        return _State.sellshop;
                    case "99":
                        player.Inventory.AddItem(119);
                        Console.WriteLine("무언가 알수 없는 힘이 나를 이끌었다. 그리고 무언가를 얻었다..\n 그리고 잠시 정신을 잃었다...");
                        Thread.Sleep(5000); // 이거 만든 이유는 추가 임무에 내 무기 추가 있어서 해본겁니다..
                        return _State.main;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;
                }
            }
        }

        public _State BuyShop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.TotalGold}G\n\n");
            Console.WriteLine("아이템 목록");
            // 아이템 목록 보여주는 코드 foreach면 될거같은데.
            //리스트 참조형 변수
            List<int> shopID = shop.GetShopID();
            //foreach로 아이템목록 추가
            int count = 1;
            foreach (var itemId in shopID)
            {
                Items item = ItemDataBase.GetID(itemId);
                if(player.Inventory.HasItem(itemId))
                {
                    Console.WriteLine($"-{count}. {item.Name}     | 방어력 {item.Defend} | {item.Description}          |  구매완");
                }
                else
                {
                    Console.WriteLine($"-{count}. {item.Name}     | 방어력 {item.Defend} | {item.Description}          |  {item.Gold}G");
                }
                count++;
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("0.");
            Console.ResetColor();
            Console.WriteLine(" 나가기");
            while (true)
            {
                Console.WriteLine("\n구매하고 싶은 아이템의 번호를 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();
                if (!int.TryParse(Select, out int convert))
                {
                    Console.WriteLine("숫자를 입력해주세요");
                    continue;
                }

                if(convert == 0)
                {
                    return _State.shop;
                }

                if (convert > 0 && convert <= shopID.Count)
                {
                    int targetID = shopID[convert - 1];
                    Items itembuy = ItemDataBase.GetID(targetID);

                    if (player.Inventory.HasItem(targetID))
                    {
                        Console.WriteLine("이미 구매한 제품입니다");
                    }
                    else if(player.TotalGold >= itembuy.Gold)
                    {
                        player.DecreaseGold(itembuy.Gold);
                        player.Inventory.AddItem(targetID);
                        /*shop.BuyItem(targetID); */// 아예 사라져 버린다. 남아있고 구매한 제품이면 구매했다고 알수있게

                        Console.WriteLine($"{itembuy.Name}을 구매하셨습니다");
                    }
                }

                Thread.Sleep(500);
                return _State.buyshop;
            }
        }

        public _State SellShop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("판매할 아이템을 선택해주세요.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.TotalGold}G\n\n");
            Console.WriteLine("아이템 목록");
            // 아이템 목록 보여주는 코드 foreach면 될거같은데.
            //리스트 참조형 변수
            List<int> itemsell = player.Inventory.GetItemIDs();
            //foreach로 아이템목록 추가
            int count = 1;
            foreach (var itemId in itemsell)
            {
                Items item = ItemDataBase.GetID(itemId);
                if (player.Inventory.HasItem(itemId))
                {
                    Console.WriteLine($"-{count}. {item.Name}     | 방어력 {item.Defend} | {item.Description}          |  {(item.Gold * 85) / 100 }G");
                }
                count++;
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("0.");
            Console.ResetColor();
            Console.WriteLine(" 나가기");
            while (true)
            {
                Console.WriteLine("\n판매하고 싶은 아이템의 번호를 적어주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();
                if (!int.TryParse(Select, out int convert))
                {
                    Console.WriteLine("숫자를 입력해주세요");
                    continue;
                }

                if (convert == 0)
                {
                    return _State.shop;
                }

                if (convert > 0 && convert <= itemsell.Count)
                {
                    int targetID = itemsell[convert - 1];

                    if (player.Inventory.HasItem(targetID))
                    {
                        player.equipment.UnEquipItem(targetID);
                        player.Sell_Item(targetID);
                        player.Inventory.DeleteItem(targetID);
                    }
                }

                Thread.Sleep(500);
                return _State.sellshop;
            }
        }

        public _State RestPage()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("휴식하기");
            Console.ResetColor();
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유골드 : {player.TotalGold} G)\n\n");
            Console.WriteLine("회복하는 HP : 30");
            Console.WriteLine("%주의% 회복량이 초과할경우 남은 회복은 사라집니다");

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");

            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                string Select = Console.ReadLine();
                Console.ResetColor();

                switch (Select)
                {
                    case "0":
                        return _State.main;
                    case "1":
                        if(player.TotalGold >= 500)
                        {
                            player.DecreaseGold(500);
                            player.HealHP(30);
                        }
                        else
                        {
                            Console.WriteLine("돈이 부족합니다");
                            break;
                        }
                        Console.Clear();
                        Thread.Sleep(1000);
                        Console.WriteLine("휴식중.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Console.WriteLine("휴식중..");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Console.WriteLine("휴식중...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Console.WriteLine("휴식중....");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Console.WriteLine("회복이 완료되었습니다! 나가시려면 아무키나 눌러주세요");
                        Console.ReadKey();
                        return _State.main;
                    case "99":
                        player.Inventory.AddItem(219);
                        Console.WriteLine("잠시 쉬고 있을때 옆을 돌아보니 이상한 검이 꽂혀있었다.");
                        Console.WriteLine($"{player.Name}(은)는 검에 손을 가져다 댄다..");
                        Console.WriteLine("검에서 이상한 소리가 들린다..");
                        Thread.Sleep(2000);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("§");
                        Thread.Sleep(500);
                        Console.Write("내");
                        Thread.Sleep(500);
                        Console.Write(" ");
                        Thread.Sleep(500);
                        Console.Write("▦");
                        Thread.Sleep(500);
                        Console.Write("을 ");
                        Thread.Sleep(500);
                        Console.Write("＆");
                        Thread.Sleep(500);
                        Console.Write("아");                             // 이거 만든 이유는 추가 임무에 내 무기 추가 있어서 해본겁니다..
                        Thread.Sleep(500);
                        Console.Write("들");
                        Thread.Sleep(500);
                        Console.Write("▨");
                        Thread.Sleep(500);
                        Console.Write("라");
                        Thread.Sleep(500);
                        Console.Write(".");
                        Thread.Sleep(500);
                        Console.Write(".");
                        Thread.Sleep(500);
                        Console.Write(".");
                        Thread.Sleep(500);
                        Console.Write("§");
                        Console.ResetColor();
                        Console.WriteLine("\n그 검의 엄청난 힘에 잠시 정신을 잃었다...");
                        Thread.Sleep(3000);
                        return _State.main;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;
                }
            }

        }

    }
}
