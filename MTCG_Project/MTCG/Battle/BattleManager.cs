using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MTCG_Project.MTCG;
using MTCG_Project.MTCG.NamespaceUser;

namespace MTCG_Project.MTCG.Battle
{
    public class BattleManager
    {
        User user1;
        User user2;
        string log;
        bool userTurn;
        public BattleManager(User curUser)
        {
            user1 = curUser;
            user2 = FindOpponent();
        }

        public void Start()
        {
            userTurn = DetStartingUser();

            
            if(userTurn)
            {
                userTurn = false;
            }
            else
            {
                userTurn = true;
            }
        }

        User FindOpponent()
        {
            User tmp_user = new User("Test");
            tmp_user.GenerateDummy();
            return tmp_user;
        }

        bool DetStartingUser()
        {
            Random rd = new Random();
            if(rd.Next(0,2) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
