using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using MTCG_Project.MTCG.Cards;
using MTCG_Project.MTCG.Cards.Monsters;
using MTCG_Project.MTCG.Cards.Spells;

namespace MTCG_Project.MTCG.NamespaceStore
{
    public class Pack
    {
        public ICard[] cards;

        public Pack()
        {
            cards = new ICard[5];
            GenerateCards();
        }

        public void GenerateCards()
        {
            Random rd = new Random();
            for (int i = 0; i < 5; i++)
            {
                int rand_num = rd.Next(1, 10);

                if (rand_num == 1)
                {
                    cards[i] = new Dragon();
                }
                else if (rand_num == 2)
                {
                    cards[i] = new FireElf();
                }
                else if (rand_num == 3)
                {
                    cards[i] = new Goblin();
                }
                else if (rand_num == 4)
                {
                    cards[i] = new Knight();
                }
                else if (rand_num == 5)
                {
                    cards[i] = new Kraken();
                }
                else if (rand_num == 6)
                {
                    cards[i] = new Ork();
                }
                else if (rand_num == 7)
                {
                    cards[i] = new Wizard();
                }
                else if (rand_num == 8)
                {
                    cards[i] = new FireSpell();
                }
                else if (rand_num == 9)
                {
                    cards[i] = new WaterSpell();
                }
                else if (rand_num == 10)
                {
                    cards[i] = new NormalSpell();
                }
                else
                {
                    Console.WriteLine("No card could be created");
                }
            }
        }
    }
}
