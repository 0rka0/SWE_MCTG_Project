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
            if (String.Compare(request.Ressource, RequestCalls.deck) == 0)
            {
                return UserCardsHandler.ShowDeck(request);
            }
            return "Kein passender Befehl!";
        }
    }
}
