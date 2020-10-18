using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.MTCG.NamespaceUser
{
   public class Deck
    {
        ICard[] cards;
        bool deckSet;

        public Deck()
        {
            cards = new ICard[4];
            deckSet = false;
        }

        public void UpdateDeck(Stack stack)
        {
            List<ICard> sorted_cards = stack.cards.OrderBy(c => c.damage).ToList();
            sorted_cards.Reverse();
            
            for (int i = 0; i < 4; i++)
            {
                cards[i] = sorted_cards[i];
            }

            deckSet = true;
        }

        public void ListCards()
        {
            if (deckSet)
            {
                for (int i = 1; i < 5; i++)
                {
                    Console.WriteLine(i - 1 + " " + cards[i - 1].name + " " + cards[i - 1].damage);
                }
            }
            else
            {
                Console.WriteLine("The deck ist still empty.");
            }
            Console.WriteLine();
        }
    }
}
