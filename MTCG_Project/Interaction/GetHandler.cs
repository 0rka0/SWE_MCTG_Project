using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    public static class GetHandler
    {
        public static void HandleByCommand(RequestContext request)
        {
            if (String.Compare(request.Ressource, RequestCalls.cards) == 0)
            {
                UserHandler.CreateUser(request);
            }
        }
    }
}
