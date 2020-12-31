using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    static public class Matchmaker
    {
        public static void MatchmadeBattleRequest(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                User user = UserDatabaseHandler.GetUserData(UserHandler.GetToken(request));
                User opp = FindOpponent(user);
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

        static User FindOpponent(User user)
        {
            //User[] opponents = UserDatabaseHandler.SearchOpponents(user);

            return user;
        }
    }
}
