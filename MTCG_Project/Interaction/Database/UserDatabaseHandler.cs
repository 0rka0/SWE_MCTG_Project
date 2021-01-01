using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    static public class UserDatabaseHandler
    {
        static string connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";  //connection info db
        static public void InsertUser(User user)
        {
            int maxId = 0;
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("Select max(uid) FROM users", conn))     //selecting and reading something from db
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        maxId = (int)reader[0];
            }
            catch(Exception e)
            {
            }

            string token = String.Format("Basic {0}-mtcgToken", user.username);
            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO users VALUES (@id, @un, @pw, @c, @gp, @elo, @w, @token)", conn))        //inserting into db
                {
                    cmd.Parameters.AddWithValue("@id", maxId + 1);      //adding parameters
                    cmd.Parameters.AddWithValue("@un", user.username);
                    cmd.Parameters.AddWithValue("@pw", user.password);
                    cmd.Parameters.AddWithValue("@c", user.coins);
                    cmd.Parameters.AddWithValue("@gp", user.gamesPlayed);
                    cmd.Parameters.AddWithValue("@elo", user.elo);
                    cmd.Parameters.AddWithValue("@w", user.wins);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)      //potential errors with db solved by exception handling
            {
                string exMsg = String.Format("User mit selbem Username existiert bereits!\n");
                conn.Close();
                throw new Exception(exMsg);
            }

            /*using (var cmd = new NpgsqlCommand("Select uid, username FROM users", conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    Console.WriteLine("{0} {1}", reader[0], reader[1]);*/

            conn.Close();
        }

        static public void LoginUser(User user)
        {
            string username = null;
            string password = null;
            string sessionActive = null;
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string selectString = String.Format("Select username, pw FROM users WHERE username = '{0}'", user.username);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    username = reader[0].ToString();
                    password = reader[1].ToString();
                }

            if (username == null)
            {
                string exMsg = String.Format("Username {0} does not exist!\n", user.username);
                conn.Close();
                throw new Exception(exMsg);
            }

            if (!((String.Compare(username, user.username) == 0) && (String.Compare(password, user.password) == 0)))
            {
                string exMsg = String.Format("Password does not fit Username\n");
                conn.Close();
                throw new Exception(exMsg);
            }

            selectString = String.Format("SELECT count(*) FROM users WHERE current_timestamp < session AND username = '{0}'", user.username);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    sessionActive = reader[0].ToString();
                }
            
            if (String.Compare(sessionActive, "1") == 0)
            {
                string exMsg = String.Format("User bereits eingeloggt!\n");
                conn.Close();
                throw new Exception(exMsg);
            }

            using (var cmd = new NpgsqlCommand("UPDATE users SET session = current_timestamp + (60 || 'minutes')::interval WHERE username = @un", conn))
            {
                cmd.Parameters.AddWithValue("@un", user.username);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        static public int AuthUser(string token)
        {
            string username = null;
            string loggedIn = null;
            //Console.WriteLine(token);
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string selectString = String.Format("SELECT username, count(username) FROM users WHERE current_timestamp < session AND token = '{0}' GROUP BY username", token);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    username = reader[0].ToString();
                    loggedIn = reader[1].ToString();
                }
            conn.Close();

            //different user states: admin, logged in user, not logged int
            if ((String.Compare(loggedIn, "0") == 0) || (String.Compare(loggedIn, null) == 0))
            {
                return 0;
            }

            if (String.Compare(username, "admin") == 0)
            {
                return 2;
            }
            
            return 1;
        }

        static public User GetUserDataByToken(string token)
        {
            string selectString = String.Format("SELECT uid, username, coins, games_played, elo, wins, name, bio, image, deck_set FROM users WHERE token = '{0}'", token);
            return GetData(selectString);
        }

        static public User GetUserDataById(int id)
        {
            string selectString = String.Format("SELECT uid, username, coins, games_played, elo, wins, name, bio, image, deck_set FROM users WHERE uid = {0}", id);
            return GetData(selectString);
        }

        static public User GetUserDataByUsername(string username)
        {
            string selectString = String.Format("SELECT uid, username, coins, games_played, elo, wins, name, bio, image, deck_set FROM users WHERE username = '{0}'", username);
            return GetData(selectString);
        }

        static User GetData(string selectString)
        {
            User user = new User();
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    user.uid = (int)reader[0];
                    user.username = reader[1].ToString();
                    user.coins = (int)reader[2];
                    user.gamesPlayed = (int)reader[3];
                    user.elo = (int)reader[4];
                    user.wins = (int)reader[5];
                    user.name = reader[6].ToString();
                    user.bio = reader[7].ToString();
                    user.image = reader[8].ToString();
                    user.deck_set = (bool)reader[9];
                }
                else
                    return null;
            conn.Close();

            return user;
        }

        static public void UpdateCoins(User user)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var cmd = new NpgsqlCommand("UPDATE users SET coins = @coins WHERE uid = @uid", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@coins", user.coins);
                cmd.Parameters.AddWithValue("@uid", user.uid);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        static public void UpdateExtraUserData(User user)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            if(String.Compare(user.name, null) != 0)
                using (var cmd = new NpgsqlCommand("UPDATE users SET name = @n WHERE username = @un", conn))
                {      //adding parameters
                    cmd.Parameters.AddWithValue("@n", user.name);
                    cmd.Parameters.AddWithValue("@un", user.username);
                    cmd.ExecuteNonQuery();
                }
            if (String.Compare(user.bio, null) != 0)
                using (var cmd = new NpgsqlCommand("UPDATE users SET bio = @b WHERE username = @un", conn))
                {      //adding parameters
                    cmd.Parameters.AddWithValue("@b", user.bio);
                    cmd.Parameters.AddWithValue("@un", user.username);
                    cmd.ExecuteNonQuery();
                }
            if (String.Compare(user.image, null) != 0)
                using (var cmd = new NpgsqlCommand("UPDATE users SET image = @i WHERE username = @un", conn))
                {      //adding parameters
                    cmd.Parameters.AddWithValue("@i", user.image);
                    cmd.Parameters.AddWithValue("@un", user.username);
                    cmd.ExecuteNonQuery();
                }

            conn.Close();
        }

        static public void UpdateAfterBattle(User user)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var cmd = new NpgsqlCommand("UPDATE users SET games_played = @gp, elo = @elo, wins = @wins WHERE username = @un", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@gp", user.gamesPlayed);
                cmd.Parameters.AddWithValue("@elo", user.elo);
                cmd.Parameters.AddWithValue("@wins", user.wins);
                cmd.Parameters.AddWithValue("@un", user.username);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        static public string GenerateScoreboard()
        {
            string str = "";
            int counter = 1;
            float winrate;
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var cmd = new NpgsqlCommand("SELECT username, elo, games_played, wins FROM users ORDER BY elo desc, wins desc", conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    winrate = 0;
                    if ((int)reader[2] != 0)
                        winrate = (float)((int)reader[3]) / (float)((int)reader[2]) * 100;
                    str += String.Format("{0}. Username: {1} | Elo: {2} | Games: {3} | Wins: {4} | Winrate: {5}%\n", 
                        counter, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), winrate.ToString("n2"));
                    counter++;
                }
            
            conn.Close();
            return str;
        }

        static public List<int> SearchOpponentsWithElo(User user)
        {
            List<int> ids = new List<int>();
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string selectString = String.Format("SELECT uid FROM users WHERE elo >= ({0} - 50) AND elo <= ({1} + 50) AND deck_set = true AND uid != {2}", user.elo, user.elo, user.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    ids.Add((int)reader[0]);
                }
            conn.Close();
            return ids;
        }

        static public List<int> SearchOpponentsWithoutElo(User user)
        {
            List<int> ids = new List<int>();
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string selectString = String.Format("SELECT uid FROM users WHERE deck_set = true AND uid != {0}", user.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    ids.Add((int)reader[0]);
                }
            conn.Close();
            return ids;
        }
    }
}
