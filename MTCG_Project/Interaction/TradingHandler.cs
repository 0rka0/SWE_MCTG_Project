using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.Trading;
using MTCG_Project.MTCG.Cards;
using MTCG_Project.MTCG.NamespaceUser;
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
                User user = UserHandler.GetUserDataByToken(request);
                if (UserCardsHandler.CheckValidCardToUser(item.cardToTrade, user))
                {
                    try
                    {
                        TradingDatabaseHandler.CreateTradingDeal(user, item);
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
                string tradeId = ExtractIdFromRessource(request.Ressource);
                User user = UserHandler.GetUserDataByToken(request);
                if (!TradingDatabaseHandler.CheckDealToUser(tradeId, user))
                {
                    string offeredCardId = request.Message.Trim('"');
                    DummyCard dummyCard = CardsUsersDatabaseHandler.GetDummyCard(user, offeredCardId);
                    if (dummyCard != null)
                    {
                        ICard card = DummyCardConverter.Convert(dummyCard);
                        if (TradingDatabaseHandler.Trade(tradeId, user, offeredCardId, card.type, card.damage))
                        {
                            TradingDatabaseHandler.DeleteTradingDeal(tradeId);
                            Console.WriteLine("Tausch erfolgreich!\n");
                            return;
                        }
                        Console.WriteLine("Tausch leider nicht erfolgreich, Anforderungen nicht erfüllt!\n");
                        return;
                    }
                    Console.WriteLine("Karte kann existiert nicht oder kann nicht getauscht werden!\n");
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
