using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTCG_Project.MTCG.User;

namespace MTCG_Project.MTCG.User
{
   public class Deck
    {
        ICard[] cards;

        public Deck()
        {
            cards = new ICard[4];   
        }

        public void UpdateDeck(Stack stack)
        {
            List<ICard> sorted_cards = stack.cards.OrderBy(c => c.damage).ToList();
            sorted_cards.Reverse();
            
            for (int i = 0; i < 4; i++)
            {
                cards[i] = sorted_cards[i];
            }
        }

        public void ListCards()
        {
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine(i-1 + " " + cards[i-1].name + " " + cards[i-1].damage);
            }
            Console.WriteLine();
        }
    }
}
