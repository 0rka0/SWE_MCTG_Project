using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    public class DatabaseHandler
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
                Console.WriteLine("User mit selbem Username existiert bereits!");
            }

            await using (var cmd = new NpgsqlCommand("Select uid, username FROM users", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                    Console.WriteLine("{0} {1}", reader[0], reader[1]);
        }
    }
}
