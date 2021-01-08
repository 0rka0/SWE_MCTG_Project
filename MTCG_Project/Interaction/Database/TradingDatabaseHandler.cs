using System;
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
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                conn.Close();
                throw new Exception(Output.TradeCreationAlreadyExists);
            }
            conn.Close();
            CardsUsersDatabaseHandler.updateShopStatusTrue(item.cardToTrade);
        }

        static public void DeleteTradingDeal(string tradeId)
        {
            CardsUsersDatabaseHandler.updateShopStatusFalse(tradeId);
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string deleteString = String.Format("DELETE FROM tradings WHERE id = '{0}'", tradeId);
            using (var cmd = new NpgsqlCommand(deleteString, conn))     
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        static public bool Trade(string tradeId, User user, string offeredCardId, string cType, float cDamage)
        {
            string cttId = "";
            string type = "";
            float minDam = 0;
            int uId = 0;
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string selectString = String.Format("SELECT * FROM tradings WHERE id = '{0}'", tradeId);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    cttId = reader[1].ToString();
                    type = reader[2].ToString();
                    minDam = (float)((double)reader[3]);
                    uId = (int)reader[4];
                }
                else
                {
                    conn.Close();
                    return false;
                }

            if (!cType.Equals(type, StringComparison.CurrentCultureIgnoreCase)  || minDam > cDamage)
            {
                conn.Close();
                return false;
            }
            conn.Close();

            CardsUsersDatabaseHandler.swapCards(user.uid, offeredCardId, uId, cttId);
            return true;
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
