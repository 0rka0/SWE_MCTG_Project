using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    static public class GetHandler
    {
        static public string HandleByCommand(RequestContext request)
        {
            if (String.Compare(request.Ressource, RequestCalls.cards) == 0)
            {
                return UserCardsHandler.ShowStack(request);
            }
            if (String.Compare(request.Ressource, RequestCalls.deck_plain) == 0)
            {
                return UserCardsHandler.ShowDeck(request, true);
            }
            if (String.Compare(request.Ressource, RequestCalls.deck) == 0)
            {
                return UserCardsHandler.ShowDeck(request, false);
            }
            if (request.Ressource.Contains(RequestCalls.specific_user))
            {
                return UserHandler.ShowPersonalUserData(request);
            }
            if (String.Compare(request.Ressource, RequestCalls.stats) == 0)
            {
                return UserHandler.ShowPersonalStats(request);
            }
            if (String.Compare(request.Ressource, RequestCalls.score) == 0)
            {
                return UserHandler.ShowScoreboard(request);
            }
            if (String.Compare(request.Ressource, RequestCalls.tradings) == 0)
            {
                return TradingHandler.ShowTradeDeals(request);
            }
            return "Kein passender Befehl!";
        }
    }
}
