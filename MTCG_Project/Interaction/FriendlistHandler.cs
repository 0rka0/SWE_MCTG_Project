using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;

namespace MTCG_Project.Interaction
{
    static public class FriendlistHandler
    {
        static public string ShowFriendlist(RequestContext request)
        {
            return "Trying to show friends";
        }

        static public void AddFriend(RequestContext request)
        {
            Console.WriteLine("Trying to add friend");
        }

        static public string DeleteFriend(RequestContext request)
        {
            return "Trying to delete friend";
        }
    }
}
