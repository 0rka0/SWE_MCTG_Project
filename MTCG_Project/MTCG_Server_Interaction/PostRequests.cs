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
        public void HandlePost(RequestContext request, string[] ressourceElements)
        {
            if(String.Compare(request.Ressource, RequestCalls.users) == 0)
            {
                CreateUsers(request);
            }
        }

        void CreateUsers(RequestContext request)
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);
            DatabaseHandler db = new DatabaseHandler();
            try
            {
                db.InsertUser(tmpUser);
            }
            catch(Exception e)
            {

            }
        }
    }
}
