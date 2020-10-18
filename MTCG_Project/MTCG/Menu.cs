using MTCG_Project.MTCG.Battle;
using MTCG_Project.MTCG.NamespaceStore;
using MTCG_Project.MTCG.NamespaceUser;
using System;
using System.Collections.Generic;
using System.Text;


namespace MTCG_Project.MTCG
{
    public class Menu
    {
        bool loggedIn = false;
        string loginName;
        User user;
        Store store;

        public Menu()
        {
            store = new Store();
        }

        public void Start()
        {
            string input;
            do
            {
                Login();
            } while (loggedIn == false);

            WelcomeMsg();

            do
            {
                Console.WriteLine("\nMain Menu - Enter:");
                input = Console.ReadLine();
                if (String.Compare(input, "H") == 0)
                {
                    HelpPage();
                }
                else if (String.Compare(input, "S") == 0)
                {
                    store.Interact(user);
                    Console.WriteLine("Returned to the main menu.");
                }
                else if (String.Compare(input, "C") == 0)
                {
                    user.stack.ListCards();
                }
                else if (String.Compare(input, "D") == 0)
                {
                    user.deck.ListCards();
                }
                else if (String.Compare(input, "B") == 0)
                {
                    BattleManager battle = new BattleManager(user);
                }
                else if (String.Compare(input, "X") != 0)
                {
                    Console.WriteLine("Invalid input.");
                    HelpPage();
                }
            }
            while (String.Compare(input, "X") != 0);
        }

        void Login()
        {
            Console.WriteLine("Enter your username:");
            loginName = Console.ReadLine();
            user = new User("David");
            loggedIn = true;
        }

        void WelcomeMsg()
        {
            Console.WriteLine("Welcome {0}", user.username);
            Console.WriteLine("What do you want to do? (Type 'H' to display options)");
        }

        void HelpPage()
        {
            Console.WriteLine("Type 'S' to enter the store.");
            Console.WriteLine("Type 'C' to view your collection.");
            Console.WriteLine("Type 'D' to view your current deck.");
            Console.WriteLine("Type 'B' to request a battle.");
            Console.WriteLine("Type 'X' to exit the game.");
        }
    }
}
