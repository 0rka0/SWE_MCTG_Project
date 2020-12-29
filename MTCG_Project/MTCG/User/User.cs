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
        public int uid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int coins { get; set; }
        public int gamesPlayed { get; set; }
        public int elo { get;set; }
        public string token { get; set; }

        public Stack stack;
        public Deck deck;

        public User(string name)
        {
            uid = 0;
            username = name;
            elo = 100;
            coins = 20;
            token = "Basic " + username + "-mtcgToken";
            stack = new Stack();
            deck = new Deck();
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
