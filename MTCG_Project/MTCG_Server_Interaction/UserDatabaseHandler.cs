using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    public class UserDatabaseHandler
    {
        string connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
        public void InsertUser(User user)
        {
            int maxId = 0;
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("Select max(uid) FROM users", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        maxId = (int)reader[0];
            }
            catch(Exception e)
            {
            }

            string token = String.Format("Basic {0}-mctgToken", user.username);
            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO users VALUES (@id, @un, @pw, @c, @gp, @elo, @token)", conn))
                {
                    cmd.Parameters.AddWithValue("@id", maxId + 1);
                    cmd.Parameters.AddWithValue("@un", user.username);
                    cmd.Parameters.AddWithValue("@pw", user.password);
                    cmd.Parameters.AddWithValue("@c", user.coins);
                    cmd.Parameters.AddWithValue("@gp", user.gamesPlayed);
                    cmd.Parameters.AddWithValue("@elo", user.elo);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                string exMsg = String.Format("User mit selbem Username existiert bereits!");
                conn.Close();
                throw new Exception(exMsg);
            }

            using (var cmd = new NpgsqlCommand("Select uid, username FROM users", conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    Console.WriteLine("{0} {1}", reader[0], reader[1]);

            conn.Close();
        }

        public void LoginUser(User user)
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
                string exMsg = String.Format("Username {0} does not exist", user.username);
                conn.Close();
                throw new Exception(exMsg);
            }

            if (!((String.Compare(username, user.username) == 0) && (String.Compare(password, user.password) == 0)))
            {
                string exMsg = String.Format("Password does not fit Username");
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
            Console.WriteLine(sessionActive);
            if (String.Compare(sessionActive, "1") == 0)
            {
                string exMsg = String.Format("User bereits eingeloggt!");
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
    }
}
