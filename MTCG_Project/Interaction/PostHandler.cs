using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    public class PostHandler
    {
        public void HandleByCommand(RequestContext request)
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
        }
    }
}
