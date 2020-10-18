using MTCG_Project.MTCG;
using MTCG_Project.MTCG.User;
using MTCG_Project.MTCG.Store;
using System;
using System.Collections.Generic;
using System.Text;


namespace MTCG_Project
{
    public class User
    {
        public string username;
        public string password;
        public int coins;
        public int gamesPlayed;
        public int elo;
        
        public Stack stack;
        public Deck deck;

        public User()
        {
            stack = new Stack();
            deck = new Deck();
        }

        public void GetPack(Pack pack)
        {
            for (int i = 0; i < 5; i++)
            {
                stack.AddCard(pack.cards[i]);
            }
            deck.UpdateDeck(stack);
        }
    }
}
