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
        List<ICard> cards = new List<ICard>();

        public void UpdateDeck(Stack stack)
        {
            List<ICard> sorted_cards = stack.cards.OrderBy(c => c.damage).ToList();
            sorted_cards.Reverse();

            cards.Clear();

            for (int i = 0; i < 4; i++)
            {
                cards.Add(sorted_cards[i]);
            }

        }

        public void ListCards()
        {
            int counter = 0;
            foreach (ICard card in cards)
            {
                counter++;
                Console.WriteLine(counter + " " + card.name + " " + card.damage);
            }
            if (counter == 0)
            {
                Console.WriteLine("The deck is still empty.");
            }
            Console.WriteLine();
        }
    }
}
