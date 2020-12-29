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
    }
}
