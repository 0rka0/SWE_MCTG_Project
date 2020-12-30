using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    public static class GetHandler
    {
        public static string HandleByCommand(RequestContext request)
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
                return UserHandler.ShowUserData(request);
            }
            if (String.Compare(request.Ressource, RequestCalls.stats) == 0)
            {
                return UserHandler.ShowStats(request);
            }
            return "Kein passender Befehl!";
        }
    }
}
