using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    public static class UserCardsHandler
    {
        public static string ShowStack(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                User user = UserHandler.GetUserData(request);

                return CardsUsersDatabaseHandler.GetStackByUser(user); ;
            }

            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
            return "Nicht eingeloggt!";
        }

        public static string ShowDeck(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                
            }

            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
            return "Nicht eingeloggt!";
        }
    }
}
