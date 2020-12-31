using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.MTCG.NamespaceUser
{
    public class BattleDeck
    {
        List<ICard> cards = new List<ICard>();

        public void AddCard(ICard card)
        {
            cards.Add(card);
        }

        public ICard GetRandomCard()
        {
            Random rnd = new Random();
            int selector = rnd.Next(0, cards.Count);
            ICard tmp = cards[selector];
            cards[selector] = cards[0];
            cards[0] = tmp;
            return cards[0];
        }

        public void RemoveCard()
        {
            cards.RemoveAt(0);
        }

        public int GetLength()
        {
            return cards.Count;
        }
    }
}
