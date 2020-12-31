using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.MTCG.Cards;
using Newtonsoft.Json;

namespace MTCG_Project.Interaction
{
    public static class UserCardsHandler
    {
        public static string ShowStack(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                User user = UserHandler.GetUserDataByToken(request);

                return CardsUsersDatabaseHandler.GetStackByUser(user);
            }

            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
            return "Nicht eingeloggt!";
        }

        public static string ShowDeck(RequestContext request, bool format)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                User user = UserHandler.GetUserDataByToken(request);

                return CardsUsersDatabaseHandler.GetDeckByUser(user, format);
            }

            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
            return "Nicht eingeloggt!";
        }

        public static void ConfigureDeck(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                string[] strings = PrepareStrings(request.Message);
                User user = UserHandler.GetUserDataByToken(request);

                CardsUsersDatabaseHandler.UpdateDeck(user, strings);
                return;
            }

            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

        static string[] PrepareStrings(string inputString)
        {
            int counter = 0;
            string[] finishedStrings = new string[5];
            string jsonString = inputString.Trim('[', ']');
            string[] jsonStrings = jsonString.Split(", ");
            foreach (string s in jsonStrings)
            {
                finishedStrings[counter] = s.Trim('"');
                counter++;
            }
            return finishedStrings;
        }
    }
}
