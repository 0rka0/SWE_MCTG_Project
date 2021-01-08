using System;
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
                Output.WriteConsole(Output.FriendlistSelfInteraction);
                return;
            }
            if (CheckFriends(user, friend))
            {
                Output.WriteConsole(Output.AlreadyFriends);
                return;
            }

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string selectString = String.Format("SELECT uid1 FROM friends WHERE (uid1 = {0} AND uid2 = {1}) OR (uid1 = {1} AND uid2 = {0})", user.uid, friend.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    if((int)reader[0] == user.uid)
                    {
                        conn.Close();
                        Output.WriteConsole(Output.FriendlistSelfInteraction);
                        return;
                    }
                    conn.Close();
                    Accept(user, friend);
                    Output.WriteConsole(Output.FriendlistRequestAccepted);
                    return;
                }
            conn.Close();

            Insert(user, friend);
            Output.WriteConsole(Output.FriendlistRequestSent);
        }

        static public string DeleteFriend(User user, User friend)
        {
            if (user.uid == friend.uid)
                return Output.FriendlistSelfInteraction;
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            string selectString = String.Format("SELECT * FROM friends WHERE (uid1 = {0} AND uid2 = {1}) OR (uid1 = {1} AND uid2 = {0})", user.uid, friend.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                if (!reader.Read())
                {
                    conn.Close();
                    return Output.FriendlistRemoveError;
                }

            using (var cmd = new NpgsqlCommand("DELETE FROM friends WHERE (uid1 = @id1 AND uid2 = @id2) OR (uid1 = @id2 AND uid2 = @id1)", conn))
            {
                cmd.Parameters.AddWithValue("@id1", user.uid);
                cmd.Parameters.AddWithValue("@id2", friend.uid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return Output.FriendlistRemoveSuccess;
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
                cmd.Prepare();
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
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        static public bool CheckFriends(User user, User friend)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            string selectString = String.Format("SELECT * FROM friends WHERE ((uid1 = {0} AND uid2 = {1}) OR (uid1 = {1} AND uid2 = {0})) AND accepted = true", user.uid, friend.uid);
            using (var cmd = new NpgsqlCommand(selectString, conn))
            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    conn.Close();
                    return true;
                }
            conn.Close();
            return false;
        }
    }
}
