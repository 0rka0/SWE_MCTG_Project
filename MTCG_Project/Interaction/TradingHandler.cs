using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.Trading;
using MTCG_Project.MTCG.Cards;
using Newtonsoft.Json;

namespace MTCG_Project.Interaction
{
    static public class TradingHandler
    {
        static public string ShowTradeDeals(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                return TradingDatabaseHandler.GetTradingDeals();
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
            return "Nicht eingeloggt!";
        }

        static public void CreateDeal(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                TradeItem item = JsonConvert.DeserializeObject<TradeItem>(request.Message);
                if (UserCardsHandler.CheckValidCardToUser(item.cardToTrade, UserHandler.GetUserDataByToken(request)))
                {
                    try
                    {
                        TradingDatabaseHandler.CreateTradingDeal(UserHandler.GetUserDataByToken(request), item);
                        Console.WriteLine("Trade Deal erfolgreich erstellt!\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    return;
                }
                Console.WriteLine("Die angegebene Karte kann nicht getauscht werden!\n");
                return;
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

        static public string DeleteDeal(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                string id = ExtractIdFromRessource(request.Ressource);
                if (TradingDatabaseHandler.CheckDealToUser(id, UserHandler.GetUserDataByToken(request)))
                {
                    //card id needed to update cards_users
                    TradingDatabaseHandler.DeleteTradingDeal(id);
                    return "Deal erfolgreich gelöscht!";
                }
                return "Es kann kein Deal mit der angegebenen Karte gelöscht werden";
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
            return "Nicht eingeloggt!";
        }

        static public void Trade(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                string id = ExtractIdFromRessource(request.Ressource);
                if (!TradingDatabaseHandler.CheckDealToUser(id, UserHandler.GetUserDataByToken(request)))
                {
                    string str = request.Message.Trim('"');
                    Console.WriteLine(str);
                    //ICard needs to be created to get type, Trade() function needs to be implemented
                    TradingDatabaseHandler.Trade();
                    return;
                }
                Console.WriteLine("Man kann nicht mit sich selbst handeln!\n");
                return;
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

            static string ExtractIdFromRessource(string ress)
        {
            return ress.Replace("/tradings/", "");
        }
    }
}
