using MTCG_Project.MTCG;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.MTCG.NamespaceStore;
using System;
using System.Collections.Generic;
using System.Text;


namespace MTCG_Project.MTCG.NamespaceUser
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public int coins { get; private set; }
        public int gamesPlayed { get; private set; }
        public int elo { get; private set; }
        public string token { get; private set; }

        public Stack stack;
        public Deck deck;

        public User(string name)
        {
            username = name;
            elo = 100;
            coins = 20;
            token = null;
            stack = new Stack();
            deck = new Deck();
        }

        public void ActivateToken()
        {
            token = String.Format("Basic {0}-mctgToken", username);
        }

        public void DeactivateToken()
        {
            token = null;
        }

        public void GetPack(Pack pack)
        {
            coins -= 5;
            for (int i = 0; i < 5; i++)
            {
                stack.AddCard(pack.cards[i]);
            }
            deck.UpdateDeck(stack);
        }

        public void GenerateDummy()
        {
            Pack pack = new Pack();
            GetPack(pack);
        }
    }
}
