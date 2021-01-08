using System;
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
                    Output.WriteConsole(Output.PackageAddedSuccess);
                }
                catch (Exception e)
                {
                    Output.WriteConsole(e.Message);
                }

                return;
            }

            if (userstate == 1)
            {
                Output.WriteConsole(Output.PermissionsDenied);
                return;
            }
            Output.WriteConsole(Output.AuthError);
        }

        static public void AcquirePackage(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                User user = UserHandler.GetUserDataByToken(request);
                if (user.coins < 5)
                {
                    Output.WriteConsole(Output.InsufficientCoins);
                    return;
                }

                try
                {
                    CardsPacksDatabaseHandler.AcquirePackage(user);
                    user.coins -= 5;
                    UserHandler.UpdateCoins(user);
                    Output.WriteConsole(Output.PackageTransactionSuccess);
                }
                catch (Exception e)
                {
                    Output.WriteConsole(e.Message);
                }

                return;
            }
            Output.WriteConsole(Output.AuthError);
        }

        static public string[] PrepareJsonStrings(string inputString)
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
