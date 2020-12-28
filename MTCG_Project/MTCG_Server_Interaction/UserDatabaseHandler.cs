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
        public async void InsertUser(User user)
        {
            int maxId = 0;
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            try
            {
                await using (var cmd = new NpgsqlCommand("Select max(uid) FROM users", conn))
                await using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                        maxId = (int)reader[0];
            }
            catch(Exception e)
            {
            }

            try
            {
                await using (var cmd = new NpgsqlCommand("INSERT INTO users VALUES (@id, @un, @pw, @c, @gp, @elo)", conn))
                {
                    cmd.Parameters.AddWithValue("@id", maxId + 1);
                    cmd.Parameters.AddWithValue("@un", user.username);
                    cmd.Parameters.AddWithValue("@pw", user.password);
                    cmd.Parameters.AddWithValue("@c", user.coins);
                    cmd.Parameters.AddWithValue("@gp", user.gamesPlayed);
                    cmd.Parameters.AddWithValue("@elo", user.elo);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch(Exception e)
            {
                await conn.CloseAsync();
                Console.WriteLine("User mit selbem Username existiert bereits!");
                return;
            }

            Console.WriteLine("User: {0}, wurde erfolgreich erstellt!", user.username);

            await using (var cmd = new NpgsqlCommand("Select uid, username FROM users", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                    Console.WriteLine("{0} {1}", reader[0], reader[1]);

            await conn.CloseAsync();
        }

        public async void CheckUser(User user)
        {
            string username = null;
            string password = null;
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            string selectString = String.Format("Select username, pw FROM users WHERE username = '{0}'", user.username);
            await using (var cmd = new NpgsqlCommand(selectString, conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    username = reader[0].ToString();
                    password = reader[1].ToString();
                }

            await conn.CloseAsync();
            if (username == null)
            {
                Console.WriteLine("Username {0} does not exist", user.username);
                return;
            }

            if (!((String.Compare(username, user.username) == 0) && (String.Compare(password, user.password) == 0)))
            {
                Console.WriteLine("Password does not fit Username");
                return;
            }

            Console.WriteLine("Login successful!");
        }
    }
}
