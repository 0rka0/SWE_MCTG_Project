using System;
using Newtonsoft.Json;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    static public class UserHandler
    {
        static public void CreateUser(RequestContext request)   //register user for further use
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);    //temp user to write data into db
            try
            {
                UserDatabaseHandler.InsertUser(tmpUser);
                Output.WriteConsole(Output.UserCreated);
            }
            catch (Exception e)
            {
                Output.WriteConsole(e.Message);
            }
        }

        static public void LoginUser(RequestContext request)    //login user to perform actions
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);
            try
            {
                UserDatabaseHandler.LoginUser(tmpUser);
                Output.WriteConsole(Output.UserLoginSuccess);
            }
            catch (Exception e)
            {
                Output.WriteConsole(e.Message);
            }
        }

        static public string ShowPersonalUserData(RequestContext request)   //a user can view his own personal data
        {
            int userstate = UserHandler.AuthUser(request);      //check if user is logged in
            if (userstate == 1 || userstate == 2) 
            {
                if(AccessUserdata(request))
                {
                    User user = GetUserDataByToken(request);
                    return String.Format("ID: {0}\nUsername: {1}\nCoins: {2}\nName: {3}\nBio: {4}\nImage: {5}\n", 
                        user.uid, user.username, user.coins, user.name, user.bio, user.image);
                }
                return Output.UserdataAccessError;
            }
            return Output.AuthError;
        }

        static public string ShowPersonalStats(RequestContext request)      //a user can view his personal stats
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
            return Output.AuthError;
        }

        static public string ShowScoreboard(RequestContext request)         //a scoreboard including stats of all users can be displayed
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                return UserDatabaseHandler.GenerateScoreboard();
            }
            return Output.AuthError;
        }

        static public void UpdateExtraUserData(RequestContext request)      //used to edit data like name, bio and image
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                if (AccessUserdata(request))        //check if user is permitted to access data (if user tries to edit his own data)
                { 
                    User user = JsonConvert.DeserializeObject<User>(request.Message);
                    user.username = ExtractUserFromToken(GetToken(request));
                    UserDatabaseHandler.UpdateExtraUserData(user);
                    Output.WriteConsole(Output.UserdataEditSuccess);
                    return;
                }
                Output.WriteConsole(Output.UserdataAccessError);
                return;
            }
            Output.WriteConsole(Output.AuthError);
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
            return UserDatabaseHandler.GetUserDataByToken(token);
        }

        static public User GetUserDataById(int id)
        {
            return UserDatabaseHandler.GetUserDataById(id);
        }

        static public User GetUserDataByUsername(string username)
        {
            return UserDatabaseHandler.GetUserDataByUsername(username);
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
