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
                using (var cmd = new NpgsqlCommand("INSERT INTO users VALUES (@id, @un, @pw, @c, @gp, @elo, @token)", conn))        //inserting into db
                {
                    cmd.Parameters.AddWithValue("@id", maxId + 1);      //adding parameters
                    cmd.Parameters.AddWithValue("@un", user.username);
                    cmd.Parameters.AddWithValue("@pw", user.password);
                    cmd.Parameters.AddWithValue("@c", user.coins);
                    cmd.Parameters.AddWithValue("@gp", user.gamesPlayed);
                    cmd.Parameters.AddWithValue("@elo", user.elo);
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
            Console.WriteLine(token);
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
            if (String.Compare(loggedIn, "0") == 0)
            {
                return 0;
            }

            if (String.Compare(username, "admin") == 0)
            {
                return 2;
            }
            
            return 1;
        }

        static public User GetUserData(string token, User user)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string selectString = String.Format("SELECT uid, username, coins, games_played, elo FROM users WHERE token = '{0}'", token);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    user.uid = (int)reader[0];
                    user.username = reader[1].ToString();
                    user.coins = (int)reader[2];
                    user.gamesPlayed = (int)reader[3];
                    user.elo = (int)reader[4];
                }
            conn.Close();

            return user;
        }

        static public void UpdateUserData(User user)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var cmd = new NpgsqlCommand("UPDATE users SET coins = @coins, games_played = @gp, elo = @elo WHERE uid = @uid", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@coins", user.coins);
                cmd.Parameters.AddWithValue("@gp", user.gamesPlayed);
                cmd.Parameters.AddWithValue("@elo", user.elo);
                cmd.Parameters.AddWithValue("@uid", user.uid);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
}
