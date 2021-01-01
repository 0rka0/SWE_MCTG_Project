using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.Interaction
{
    static public class UserCardsHandler
    {
        static public string ShowStack(RequestContext request)
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

        static public string ShowDeck(RequestContext request, bool format)
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

        static public void ConfigureDeck(RequestContext request)
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

        public static BattleDeck GenerateBattleDeck(User user)
        {
            DummyCard[] dummyDeck = CardsUsersDatabaseHandler.GetDummyDeck(user);
            BattleDeck deck = new BattleDeck();

            foreach (DummyCard card in dummyDeck)
            {
                deck.AddCard(DummyCardConverter.Convert(card));
            }

            return deck;
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

        static public bool CheckValidCardToUser(string cardId, User user)
        {
            return CardsUsersDatabaseHandler.CheckValidCard(cardId, user);
        }

        static public void SellCard(string cardId, User user)
        {
            CardsUsersDatabaseHandler.SellCard(cardId, user);
        }
    }
}
