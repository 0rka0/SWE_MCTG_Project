using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace MTCG_Project.MTCG.User
{
    public class Stack
    {
        public List<ICard> cards { get; } = new List<ICard>();
        public int cards_amount;

        public Stack()
        {
            cards_amount = 0; 
        }

        public void AddCard(ICard card)
        {
            cards.Add(card);
            cards_amount++;
        }

        public void RemoveCard(int cardPos)
        {
            cards.RemoveAt(cardPos);
            cards_amount--;
        }

        public void ListCards()
        {
            int counter = 0;
            foreach (ICard card in cards)
            {
                counter++;
                Console.WriteLine(counter + " " + card.name + " " + card.damage);
            }
            Console.WriteLine();
        }
    }
}
