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

        static public string ShowUserData(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                if(AccessUserdata(request))
                {
                    User user = GetUserData(request);
                    return String.Format("ID: {0}\nUsername: {1}\nCoins: {2}\nGames played: {3}\nElo: {4}\nName: {5}\nBio: {6}\nImage: {7}\n", 
                        user.uid, user.username, user.coins, user.gamesPlayed, user.elo, user.name, user.bio, user.image);
                }
                return "Man kann nicht die Daten von anderen Usern verändern!";
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

        static public User GetUserData(RequestContext request)
        {
            string token = GetToken(request);

            User user = new User();
            user = UserDatabaseHandler.GetUserData(token, user);

            return user;
        }

        static public void UpdateBaseUserData(User user)
        {
            UserDatabaseHandler.UpdateBaseUserData(user);
        }

        static string GetToken(RequestContext request)
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
