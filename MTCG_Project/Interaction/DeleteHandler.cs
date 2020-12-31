using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    static public class DeleteHandler
    {
        static public string HandleByCommand(RequestContext request)
        {
            if (request.Ressource.Contains(RequestCalls.specific_trade))
            {
                return TradingHandler.DeleteDeal(request);
            }
            return "Kein passender Befehl!";
        }

    }
}
