using MTCG_Project.MTCG;
using MTCG_Project.MTCG.Cards;
using MTCG_Project.MTCG.Cards.Monsters;
using MTCG_Project.MTCG.Cards.Spells;
using MTCG_Project.MTCG.NamespaceStore;
using MTCG_Project.MTCG.NamespaceUser;
using System;

namespace MTCG_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Start();
        }
    }
}
