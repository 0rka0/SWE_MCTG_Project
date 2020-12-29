using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.MTCG.Cards;
using Npgsql;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    public static class CardsPacksDatabaseHandler
    {
        static string connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";  //connection info db
        public static void InsertPackage(DummyCard[] cards)
        {
            int maxId = 0;
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();
            try
            {
                using (var cmd = new NpgsqlCommand("Select max(packid) FROM cards_packs", conn))     //selecting and reading something from db
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        maxId = (int)reader[0];
            }
            catch (Exception e)
            {
            }

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    using (var cmd = new NpgsqlCommand("INSERT INTO cards_packs VALUES (@id, @cn, @damage, @pid)", conn))        //inserting into db
                    {      //adding parameters
                        cmd.Parameters.AddWithValue("@id", cards[i].id);
                        cmd.Parameters.AddWithValue("@cn", cards[i].name);
                        cmd.Parameters.AddWithValue("@damage", cards[i].damage);
                        cmd.Parameters.AddWithValue("@pid", maxId+1);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e) 
                {
                    string exMsg = String.Format("Das gewünschte Package existiert bereits in der Datenbank!\n");
                    conn.Close();
                    throw new Exception(exMsg);
                }
            }

            conn.Close();
        }

        public static void AcquirePackage(User user)
        {
            DummyCard[] cards = new DummyCard[5];
            for (int i = 0; i < 5; i++)
                cards[i] = new DummyCard();
            int counter = 0;
            int minId = 0;
            using var conn = new NpgsqlConnection(connString);  
            conn.Open();
            try
            {
                using (var cmd = new NpgsqlCommand("Select min(packid) FROM cards_packs WHERE bought = false", conn))    
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        minId = (int)reader[0];
            }
            catch (Exception e)
            {
                string exMsg = String.Format("Es existieren keine Packages, die gekauft werden könnten!\n");
                conn.Close();
                throw new Exception(exMsg);
            }

            string selectString = String.Format("SELECT id, cardname, damage FROM cards_packs WHERE packid = {0}", minId);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    cards[counter].id = reader[0].ToString();
                    cards[counter].name = reader[1].ToString();
                    cards[counter].damage = (float)((double)reader[2]);
                    counter++;
                }

            using (var cmd = new NpgsqlCommand("UPDATE cards_packs SET bought = true WHERE packid = @id", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@id", minId);
                cmd.ExecuteNonQuery();
            }

            conn.Close();

            CardsUsersDatabaseHandler.InsertPackage(cards, user);
        }
    }
}
