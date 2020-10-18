using Microsoft.VisualBasic;
using MTCG_Project.MTCG.NamespaceUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.NamespaceStore
{
    class Store
    {
        User user;

        public void Interact(User curUser)
        {
            string input;
            user = curUser;

            WelcomeMsg();

            do
            {
                Console.WriteLine("\nStore - Enter:");
                input = Console.ReadLine();
                if (String.Compare(input, "H") == 0)
                {
                    HelpPage();
                }
                else if (String.Compare(input, "B") == 0)
                {
                    bool suc = false;
                    if (user.coins >= 5)
                    {
                        user.GetPack(NewPack());
                        suc = true;
                    }
                    PackMsg(suc);
                }
                else if (String.Compare(input, "X") != 0)
                {
                    Console.WriteLine("Invalid input.");
                    HelpPage();
                }
            }
            while (String.Compare(input, "X") != 0);
            Console.WriteLine("You left the store.");
        }
        
        public Pack NewPack()
        {
            Pack pack = new Pack();
            return pack;
        }

        void WelcomeMsg()
        {
            Console.WriteLine("Welcome to the store!");
            Console.WriteLine("What do you want to do? (Type 'H' to display options)");
        }

        void PackMsg(bool suc)
        {
            if(suc)
            {
                Console.WriteLine("Pack purchase has been successful.");
            }
            else
            {
                Console.WriteLine("Insufficient coins to buy a new pack.");
            }
        }

        void HelpPage()
        {
            Console.WriteLine("Type 'B' to buy a new pack.");
            Console.WriteLine("Type 'T' to start trading.");
            Console.WriteLine("Type 'X' to exit the store.");
        }
    }
}
