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

            if(token != null)
                return UserDatabaseHandler.AuthUser(token);

            return 0;
        }

        static public User GetUserData(RequestContext request)
        {
            string token = GetToken(request);

            User user = new User("");
            user = UserDatabaseHandler.GetUserData(token, user);

            return user;
        }

        static public void UpdateUserData(User user)
        {
            UserDatabaseHandler.UpdateUserData(user);
        }

        static string GetToken(RequestContext request)
        {
            if(request.HeaderData.ContainsKey("Authorization"))
                return request.HeaderData["Authorization"];

            return null;
        }
    }
}
