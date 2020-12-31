using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.Interaction
{
    static public class PackageHandler
    {
        static public void CreatePackage(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 2)     //adminrechte benötigt
            {
                int counter = 0;
                DummyCard[] cards = new DummyCard[5];
                string[] jsonStrings = PrepareJsonStrings(request.Message);
                foreach (string s in jsonStrings)
                {
                    cards[counter] = JsonConvert.DeserializeObject<DummyCard>(jsonStrings[counter]);
                    counter++;
                }

                try
                {
                    CardsPacksDatabaseHandler.InsertPackage(cards);
                    Console.WriteLine("Package wurde erfolgreich hinzugefügt!\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return;
            }

            if (userstate == 1)
            {
                Console.WriteLine("Adminrechte benötigt!\n");
                return;
            }

            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

        static public void AcquirePackage(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                User user = UserHandler.GetUserDataByToken(request);
                if (user.coins < 5)
                {
                    Console.WriteLine("Nicht genug Muenzen im Besitz!\n");
                    return;
                }

                try
                {
                    CardsPacksDatabaseHandler.AcquirePackage(user);
                    user.coins -= 5;
                    UserHandler.UpdateCoins(user);
                    Console.WriteLine("Package wurde erfolgreich gekauft!\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return;
            }

            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

        static string[] PrepareJsonStrings(string inputString)
        {
            int counter = 0;
            string[] finishedStrings = new string[5];
            string jsonString = inputString.Trim('[', ']');
            string[] jsonStrings = jsonString.Split("},");
            foreach (string s in jsonStrings)
            {
                if (counter < 4)
                    finishedStrings[counter] = s + "}";
                if (counter == 4)
                    finishedStrings[counter] = s;
                counter++;
            }
            return finishedStrings;
        }
    }
}
