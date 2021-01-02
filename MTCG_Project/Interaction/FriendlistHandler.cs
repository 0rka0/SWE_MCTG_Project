using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    static public class FriendlistHandler
    {
        static public string ShowFriendlist(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     
            {
                return FriendsDatabaseHandler.ShowFriendlist(UserHandler.GetUserDataByToken(request));
            }
            return "Nicht eingeloggt!";
        }

        static public void AddFriend(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)    
            {
                string friendName = ExtractUsernameFromRessource(request.Ressource);
                User user = UserHandler.GetUserDataByUsername(friendName);
                if (user != null)
                    FriendsDatabaseHandler.AddFriend(UserHandler.GetUserDataByToken(request), UserHandler.GetUserDataByUsername(friendName)); //To be implemented
                else
                    Console.WriteLine("Ausgewählter Username existiert nicht!\n");
                return;
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

        static public string DeleteFriend(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                string friendName = ExtractUsernameFromRessource(request.Ressource);
                User user = UserHandler.GetUserDataByUsername(friendName);
                if (user != null)
                    return FriendsDatabaseHandler.DeleteFriend(UserHandler.GetUserDataByToken(request), UserHandler.GetUserDataByUsername(friendName));
                return "Ausgewählter Username existiert nicht!";
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
            return "Nicht Eingeloggt!";
        }

        static public bool CheckFriends(User user, User friend)
        {
            return FriendsDatabaseHandler.CheckFriends(user, friend);
        }

        static string ExtractUsernameFromRessource(string ress)
        {
            return ress.Replace("/friends/", "");
        }
    }
}
