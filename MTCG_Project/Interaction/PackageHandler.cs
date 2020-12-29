using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceStore;
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
                Console.WriteLine(cards[0].id + " " + cards[0].name + " " + cards[0].damage);
                Console.WriteLine(cards[1].id + " " + cards[1].name + " " + cards[1].damage);
                Console.WriteLine(cards[2].id + " " + cards[2].name + " " + cards[2].damage);
                Console.WriteLine(cards[3].id + " " + cards[3].name + " " + cards[3].damage);
                Console.WriteLine(cards[4].id + " " + cards[4].name + " " + cards[4].damage);
                return;
            }
            
            if (userstate == 1)
                Console.WriteLine("Adminrechte benötigt!\n");

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
