using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.MTCG.Trading;

namespace MTCG_Project.Interaction
{
    static public class TradingDatabaseHandler
    {
        static string connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
        static public string GetTradingDeals()
        {
            string str = "All available trading deals:\n\n";
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();

            string selectString = String.Format("SELECT * FROM tradings");
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    str += "Trade-Id: " + reader[0].ToString() + " | ";
                    str += "Card to trade: " + reader[1].ToString() + "\n";
                    str += " WANTED: Type: " + reader[2].ToString() + " | ";
                    str += "Minimum damage: " + reader[3].ToString() + "\n\n";
                }

            conn.Close();
            return str;
        }

        static public void CreateTradingDeal(User user, TradeItem item)
        {
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();
            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO tradings VALUES (@id, @ctt, @type, @md, @uid)", conn))        //inserting into db
                {
                    cmd.Parameters.AddWithValue("@id", item.id);      //adding parameters
                    cmd.Parameters.AddWithValue("@ctt", item.cardToTrade);
                    cmd.Parameters.AddWithValue("@type", item.type);
                    cmd.Parameters.AddWithValue("@md", item.minimumDamage);
                    cmd.Parameters.AddWithValue("@uid", user.uid);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                string exMsg = String.Format("Es existiert bereits ein Trading Deal mit der angegebenen Karte oder ID!\n");
                conn.Close();
                throw new Exception(exMsg);
            }
            
            using (var cmd = new NpgsqlCommand("UPDATE cards_users SET in_shop = true WHERE id = '@id'", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@id", item.cardToTrade);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        static public void DeleteTradingDeal(string id)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            string deleteString = String.Format("DELETE FROM tradings WHERE id = '{0}'", id);
            using (var cmd = new NpgsqlCommand(deleteString, conn))     
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        static public void Trade()
        {

        }

        static public bool CheckDealToUser(string tradeId, User user)
        {
            bool result = false;
            using var conn = new NpgsqlConnection(connString); 
            conn.Open();

            string selectString = String.Format("SELECT * FROM tradings WHERE uid = {0} AND id = '{1}'", user.uid, tradeId);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                    result = true;

            conn.Close();
            return result;
        }
    }
}
