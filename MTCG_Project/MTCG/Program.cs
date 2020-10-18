using MTCG_Project.MTCG;
using MTCG_Project.MTCG.Cards;
using MTCG_Project.MTCG.Cards.Monsters;
using MTCG_Project.MTCG.Cards.Spells;
using MTCG_Project.MTCG.Store;
using MTCG_Project.MTCG.User;
using System;

namespace MTCG_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            User user = new User();
            Pack pack = new Pack();
            Pack pack2 = new Pack();
            user.GetPack(pack);
            user.GetPack(pack2);
            user.stack.ListCards();
            user.deck.ListCards();
        }
    }
}
