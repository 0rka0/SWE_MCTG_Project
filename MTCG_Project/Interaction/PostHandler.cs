using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    static public class PostHandler
    {
        static public void HandleByCommand(RequestContext request)
        {
            if(String.Compare(request.Ressource, RequestCalls.users) == 0)
            {
                UserHandler.CreateUser(request);
            }
            if (String.Compare(request.Ressource, RequestCalls.sessions) == 0)
            {
                UserHandler.LoginUser(request);
            }
            if(String.Compare(request.Ressource, RequestCalls.packages) == 0)
            {
                PackageHandler.CreatePackage(request);
            }
            if(String.Compare(request.Ressource, RequestCalls.trans_packs) == 0)
            {
                PackageHandler.AcquirePackage(request);
            }
            if(String.Compare(request.Ressource, RequestCalls.battles) == 0)
            {
                Matchmaker.MatchmadeBattleRequest(request);
            }
        }
    }
}
