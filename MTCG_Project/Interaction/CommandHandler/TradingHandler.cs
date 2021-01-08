using System;
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
            return Output.AuthError;
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
                        Output.WriteConsole(Output.TradeCreationSuccess);
                    }
                    catch (Exception e)
                    {
                        Output.WriteConsole(e.Message);
                    }
                    return;
                }
                Output.WriteConsole(Output.TradeCreationInvalidCard);
                return;
            }
            Output.WriteConsole(Output.AuthError);
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
                    return Output.TradeDeletionSuccess;
                }
                return Output.TradeDeletionError;
            }
            return Output.AuthError;
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
                            Output.WriteConsole(Output.TradeSuccess);
                            return;
                        }
                        Output.WriteConsole(Output.TradeConditionsNotMet);
                        return;
                    }
                    Output.WriteConsole(Output.TradeInvalidCard);
                    return;
                }
                Output.WriteConsole(Output.TradeSelfTrade);
                return;
            }
            Output.WriteConsole(Output.AuthError);
        }

        static public void SellCard(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)
            {
                User user = UserHandler.GetUserDataByToken(request);
                string cardId = request.Message.Trim('"');
                if(UserCardsHandler.CheckValidCardToUser(cardId, user))
                {
                    UserCardsHandler.SellCard(cardId, user);
                    Output.WriteConsole(Output.CardSoldSuccess);
                    return;
                }
                Output.WriteConsole(Output.CardSoldError);
                return;
            }
            Output.WriteConsole(Output.AuthError);
        }

        static string ExtractIdFromRessource(string ress)
        {
            return ress.Replace("/tradings/", "");
        }
    }
}
