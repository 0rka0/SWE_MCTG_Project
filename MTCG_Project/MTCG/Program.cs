using MTCG_Project.MTCG;
using MTCG_Project.MTCG.Cards.Monster;
using System;

namespace MTCG_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            ICard card1 = new Dragon();

            Console.WriteLine(card1.name);
            Console.WriteLine(card1.damage);
            Console.WriteLine(card1.element_type);
            Console.WriteLine(card1.type);
        }
    }
}
