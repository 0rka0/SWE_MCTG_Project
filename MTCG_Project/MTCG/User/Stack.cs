using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.MTCG.NamespaceUser
{
    public class Stack
    {
        public List<ICard> cards { get; } = new List<ICard>();
        int cards_amount;

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
            if (counter == 0)
            {
                Console.WriteLine("The Stack is still empty.");
            }
            Console.WriteLine();
        }
    }
}
