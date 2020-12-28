using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    public class PostRequests
    {
        public void HandlePost(RequestContext request)
        {
            if(String.Compare(request.Ressource, RequestCalls.users) == 0)
            {
                CreateUser(request);
            }
            if (String.Compare(request.Ressource, RequestCalls.sessions) == 0)
            {
                LoginUser(request);
            }
        }

        void CreateUser(RequestContext request)
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);
            UserDatabaseHandler db = new UserDatabaseHandler();

            db.InsertUser(tmpUser);
        }

        void LoginUser(RequestContext request)
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);
            UserDatabaseHandler db = new UserDatabaseHandler();
            
            db.CheckUser(tmpUser);
        }
    }
}
