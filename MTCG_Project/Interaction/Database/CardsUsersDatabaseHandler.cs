using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.MTCG.Cards;
using MTCG_Project.MTCG.NamespaceUser;
using Npgsql;

namespace MTCG_Project.Interaction
{
    public static class CardsUsersDatabaseHandler
    {
        static string connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";  //connection info db
        public static void InsertPackage(DummyCard[] cards, User user)
        {
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();

            for (int i = 0; i < 5; i++)
            {               
                using (var cmd = new NpgsqlCommand("INSERT INTO cards_users VALUES (@id, @cn, @damage, @uid)", conn))        //inserting into db
                {      //adding parameters
                    cmd.Parameters.AddWithValue("@id", cards[i].id);
                    cmd.Parameters.AddWithValue("@cn", cards[i].name);
                    cmd.Parameters.AddWithValue("@damage", cards[i].damage);
                    cmd.Parameters.AddWithValue("@uid", user.uid);
                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        public static string GetStackByUser(User user)
        {
            string str = String.Format("Stack of {0}:\n", user.username);
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();

            string selectString = String.Format("SELECT id, cardname, damage FROM cards_users WHERE userid = {0}", user.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    //str += "Id: " + reader[0].ToString() + " | ";
                    str += "Name: " + reader[1].ToString() + "\n";
                    str += " Damage: " + reader[2].ToString() + "\n";
                }

            //Console.WriteLine(str);
            conn.Close();
            return str;
        }

        public static string GetDeckByUser(User user)
        {
            string str = String.Format("Deck of {0}:\n", user.username);
            bool deckEmpty = true;
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();

            string selectString = String.Format("SELECT id, cardname, damage FROM cards_users WHERE userid = {0} AND in_deck = true", user.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    deckEmpty = false;
                    //str += "Id: " + reader[0].ToString() + " | ";
                    str += "Name: " + reader[1].ToString() + "\n";
                    str += " Damage: " + reader[2].ToString() + "\n";
                }
            //Console.WriteLine(str);
            conn.Close();
            if (deckEmpty)
                return "Deck still empty!";
            return str;
        }

        public static void UpdateDeck(User user, string[] strings)
        {
            int counter = 0;
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();
            
            for (int i = 0; i < 4; i++)
            {
                string selectString = String.Format("SELECT * FROM cards_users WHERE id = '{0}' AND in_deck = false AND userid = {1}", strings[i], user.uid);
                using (var cmd = new NpgsqlCommand(selectString, conn))
                using (var reader = cmd.ExecuteReader())
                    if(reader.Read())
                    {
                        counter++;
                    }
            }

            if (counter < 4)
            {
                Console.WriteLine("Error");
                return;
            }

            conn.Close();
            AddToDeck(user, strings);
        }

        //WORK IN PROGRESS HERE
        static void AddToDeck(User user, string[] strings)
        {   //set all cards to false and selected cards true again
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();
            using (var cmd = new NpgsqlCommand("UPDATE cards_users SET in_deck = false WHERE userid = @uid", conn))        //inserting into db
            {      //adding parameters
                cmd.Parameters.AddWithValue("@uid", user.uid);
                cmd.ExecuteNonQuery();
            }

            foreach (string s in strings)
            {
                using (var cmd = new NpgsqlCommand("UPDATE cards_users SET in_deck = true WHERE userid = @uid AND id = '@id'", conn))        //inserting into db
                {      //adding parameters
                    cmd.Parameters.AddWithValue("@uid", user.uid);
                    cmd.Parameters.AddWithValue("@id", s);
                    cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }
    }
}
