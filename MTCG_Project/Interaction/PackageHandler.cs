using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceStore;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.Interaction
{
    static public class PackageHandler
    {
        static public void CreatePackage(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 2)     //adminrechte benötigt
            {
                string json = JsonConvert.DeserializeObject<string>(request.Message);
                Console.WriteLine(json);
                return;
            }
            
            if (userstate == 1)
                Console.WriteLine("Adminrechte benötigt!\n");

            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }
    }
}
