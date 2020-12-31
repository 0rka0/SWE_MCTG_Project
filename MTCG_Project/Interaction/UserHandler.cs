using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    static public class UserHandler
    {
        static public void CreateUser(RequestContext request)
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);    //temp user to write data into db
            try
            {
                UserDatabaseHandler.InsertUser(tmpUser);
                Console.WriteLine("User: {0}, wurde erfolgreich erstellt!\n", tmpUser.username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void LoginUser(RequestContext request)
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);
            try
            {
                UserDatabaseHandler.LoginUser(tmpUser);
                Console.WriteLine("User {0} erfolgreich eingeloggt!\n", tmpUser.username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public string ShowPersonalUserData(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                if(AccessUserdata(request))
                {
                    User user = GetUserDataByToken(request);
                    return String.Format("ID: {0}\nUsername: {1}\nCoins: {2}\nName: {3}\nBio: {4}\nImage: {5}\n", 
                        user.uid, user.username, user.coins, user.name, user.bio, user.image);
                }
                return "Man kann nicht die persönlichen Daten von anderen Usern nicht sehen!";
            }
            return "Nicht eingeloggt!";
        }

        static public string ShowPersonalStats(RequestContext request)
        {
            float winrate;
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {                
                User user = GetUserDataByToken(request);
                if (user.gamesPlayed == 0)
                    winrate = 0;
                else
                    winrate = (float)user.wins / (float)user.gamesPlayed * 100;

                return String.Format("Username: {0}\nElo: {1}\nGames played: {2}\nWins: {3}\nWinrate: {4}%\n",
                    user.username, user.elo, user.gamesPlayed, user.wins, winrate.ToString("n2"));                
            }
            return "Nicht eingeloggt!";
        }

        static public string ShowScoreboard(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                return UserDatabaseHandler.GenerateScoreboard();
            }
            return "Nicht eingeloggt!";
        }

        static public void UpdateExtraUserData(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                if (AccessUserdata(request))
                { 
                    User user = JsonConvert.DeserializeObject<User>(request.Message);
                    user.username = ExtractUserFromToken(GetToken(request));
                    UserDatabaseHandler.UpdateExtraUserData(user);
                    Console.WriteLine("Profil des Users {0} wurden erfolgreich bearbeitet!", user.username);
                    return;
                }
                Console.WriteLine("Man kann nicht die Daten von anderen Usern verändern!\n");
                return;
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

        static public int AuthUser(RequestContext request)
        {
            string token = GetToken(request);

            if (token != null)
                return UserDatabaseHandler.AuthUser(token);

            return 0;
        }

        static public User GetUserDataByToken(RequestContext request)
        {
            string token = GetToken(request);

            User user = UserDatabaseHandler.GetUserDataByToken(token);

            return user;
        }

        static public User GetUserDataById(int id)
        {
            User user = UserDatabaseHandler.GetUserDataById(id);

            return user;
        }

        static public void UpdateCoins(User user)
        {
            UserDatabaseHandler.UpdateCoins(user);
        }

        static public void UpdateAfterBattle(User user1, User user2)
        {
            UserDatabaseHandler.UpdateAfterBattle(user1);
            UserDatabaseHandler.UpdateAfterBattle(user2);
        }

        static public string GetToken(RequestContext request)
        {
            if(request.HeaderData.ContainsKey("Authorization"))
                return request.HeaderData["Authorization"];

            return null;
        }

        static bool AccessUserdata(RequestContext request)
        {
            if(ExtractUserFromRessource(request.Ressource) == ExtractUserFromToken(GetToken(request)))
                return true;
            return false;
        }

        static string ExtractUserFromToken(string token)
        {
            token = token.Replace("Basic ", "");
            return token.Replace("-mtcgToken", "");
        }

        static string ExtractUserFromRessource(string ress)
        {
            return ress.Replace("/users/", "");
        }
    }
}
