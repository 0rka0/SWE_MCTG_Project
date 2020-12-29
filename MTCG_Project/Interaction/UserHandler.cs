using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.Interaction
{
    static public class UserHandler
    {
        static public void CreateUser(RequestContext request)
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);    //temp user to write data into db
            try
            {
                UserDatabaseHandler.InsertUser(tmpUser);
                Console.WriteLine("User: {0}, wurde erfolgreich erstellt!\n", tmpUser.username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void LoginUser(RequestContext request)
        {
            User tmpUser = JsonConvert.DeserializeObject<User>(request.Message);
            try
            {
                UserDatabaseHandler.LoginUser(tmpUser);
                Console.WriteLine("User {0} erfolgreich eingeloggt!\n", tmpUser.username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public int AuthUser(RequestContext request)
        {
            string token = GetToken(request);

            return UserDatabaseHandler.AuthUser(token);
        }

        static string GetToken(RequestContext request)
        {
            return request.HeaderData["Authorization"];
        }
    }
}
