using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    static public class FriendsDatabaseHandler
    {
        static string connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";

        static public string ShowFriendlist(User user)
        {
            int counter = 1;
            string str = String.Format("Friends of {0}:\n", user.username);
            using var conn = new NpgsqlConnection(connString);  //connect to db
            conn.Open();

            string selectString = String.Format("SELECT uid1, uid2 FROM friends WHERE (uid1 = {0} OR uid2 = {0}) AND accepted = true", user.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    if ((int)reader[0] == user.uid)
                        str += String.Format("{0}. {1}\n", counter, UserHandler.GetUserDataById((int)reader[1]).username);
                    else
                        str += String.Format("{0}. {1}\n", counter, UserHandler.GetUserDataById((int)reader[0]).username);
                    counter++;
                }
            conn.Close();
            return str;
        }

        static public void AddFriend(User user, User friend)
        {
            if(user.uid == friend.uid)
            {
                Console.WriteLine("Man kann sich selbst keine Anfrage schicken!\n");
                return;
            }
            using var conn = new NpgsqlConnection(connString);  
            conn.Open();

            string selectString1 = String.Format("SELECT * FROM friends WHERE ((uid1 = {0} AND uid2 = {1}) OR (uid1 = {1} AND uid2 = {0})) AND accepted = true", user.uid, friend.uid);
            using (var cmd = new NpgsqlCommand(selectString1, conn))
            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    Console.WriteLine("Bereits Freunde!\n");
                    conn.Close();
                    return;
                }

            string selectString2 = String.Format("SELECT uid1 FROM friends WHERE (uid1 = {0} AND uid2 = {1}) OR (uid1 = {1} AND uid2 = {0})", user.uid, friend.uid);
            using (var cmd = new NpgsqlCommand(selectString2, conn))
            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    if((int)reader[0] == user.uid)
                    {
                        conn.Close();
                        Console.WriteLine("Man kann seine eigene Anfrage nicht annehmen!\n");
                        return;
                    }
                    conn.Close();
                    Accept(user, friend);
                    Console.WriteLine("Freundschaftsanfrage angenommen!\n");
                    return;
                }
            conn.Close();

            Insert(user, friend);
            Console.WriteLine("Anfrage verschickt!\n");
        }

        static public string DeleteFriend(User user, User friend)
        {
            if (user.uid == friend.uid)
                return "Man sollte stets sein eigener bester Freund bleiben!";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            string selectString = String.Format("SELECT * FROM friends WHERE (uid1 = {0} AND uid2 = {1}) OR (uid1 = {1} AND uid2 = {0})", user.uid, friend.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                if (!reader.Read())
                {
                    conn.Close();
                    return "Nichts zu löschen!";
                }

            using (var cmd = new NpgsqlCommand("DELETE FROM friends WHERE (uid1 = @id1 AND uid2 = @id2) OR (uid1 = @id2 AND uid2 = @id1)", conn))
            {
                cmd.Parameters.AddWithValue("@id1", user.uid);
                cmd.Parameters.AddWithValue("@id2", friend.uid);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return "Erfolgreich gelöscht!";
        }

        static void Accept(User user, User friend)
        {
            using var conn = new NpgsqlConnection(connString);  
            conn.Open();
            //string updateString = String.Format("UPDATE friends SET accepted = true WHERE (uid1 = {0} AND uid2 = {1}) OR (uid1 = {1} AND uid2 = {0})", user.uid, friend.uid);
            using (var cmd = new NpgsqlCommand("UPDATE friends SET accepted = true WHERE (uid1 = @id1 AND uid2 = @id2) OR (uid1 = @id2 AND uid2 = @id1)", conn))
            {
                cmd.Parameters.AddWithValue("@id1", user.uid);
                cmd.Parameters.AddWithValue("@id2", friend.uid);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        static void Insert(User user, User friend)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using (var cmd = new NpgsqlCommand("INSERT INTO friends VALUES (@id1, @id2)", conn))       
            {
                cmd.Parameters.AddWithValue("@id1", user.uid);      
                cmd.Parameters.AddWithValue("@id2", friend.uid);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}
