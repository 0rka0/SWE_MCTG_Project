using MTCG_Project.MTCG;
using MTCG_Project.MTCG.NamespaceUser;
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
        public int wins { get; set; }
        public int elo { get;set; }
        public string token { get; set; }
        public string name { get; set; }
        public string bio { get; set; }
        public string image { get; set; }
        public bool deck_set { get; set; }

        public BattleDeck deck;

        public User()
        {
            uid = 0;
            username = null;
            elo = 100;
            coins = 20;
            gamesPlayed = 0;
            wins = 0;
            token = "Basic " + username + "-mtcgToken";
            name = null;
            bio = null;
            image = null;
            deck_set = false;
            deck = null;
        }
    }
}
