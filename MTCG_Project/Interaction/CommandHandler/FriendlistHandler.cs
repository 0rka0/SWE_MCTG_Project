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
            return Output.AuthError;
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
                    Output.WriteConsole(Output.UserDoesNotExist);
                return;
            }
            Output.WriteConsole(Output.AuthError);
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
                return Output.UserDoesNotExist;
            }
            return Output.AuthError;
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
