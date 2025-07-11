using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG_Spartan
{
    public enum _State
    {
        main,
        Status,
        inventory,
        shop,
        buyshop,
        sellshop,
        equipment,
        Rest,
        exit
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            _State currentState = _State.main;

            gameManager.Intro();

            while (currentState != _State.exit)
            {
                switch (currentState)
                {
                    case _State.main:
                        currentState = gameManager.Menu();
                        break;
                    case _State.Status:
                        currentState = gameManager.ShowMyStatus();
                        break;
                    case _State.inventory:
                        currentState = gameManager.Inventory();
                        break;
                    case _State.shop:
                        currentState = gameManager.Shop();
                        break;
                    case _State.equipment:
                        currentState = gameManager.EquipMentPage();
                        break;
                    case _State.buyshop:
                        currentState = gameManager.BuyShop();
                        break;
                    case _State.sellshop:
                        currentState = gameManager.SellShop();
                        break;
                    case _State.Rest:
                        currentState = gameManager.RestPage();
                        break;
                    case _State.exit:
                        break;
                    default:
                        Console.WriteLine("매뉴에 있는 숫자만 적어주세요");
                        break;
                }

            }
        }
    }
}
