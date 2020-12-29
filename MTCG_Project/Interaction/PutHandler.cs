using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    static public class PutHandler
    {
        public static void HandleByCommand(RequestContext request)
        {
            if (String.Compare(request.Ressource, RequestCalls.deck) == 0)
            {
                UserCardsHandler.ConfigureDeck(request);
            }
        }
    }
}
