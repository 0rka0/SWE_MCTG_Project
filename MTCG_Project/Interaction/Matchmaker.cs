using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Server;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.MTCG.Battle;

namespace MTCG_Project.Interaction
{
    static public class Matchmaker
    {
        public static void MatchmadeBattleRequest(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     //eingeloggt
            {
                User user = UserHandler.GetUserDataByToken(request);
                if (user.deck_set)
                {
                    try
                    {
                        User opp = FindOpponent(user);
                        BattleManager battle = new BattleManager(user, opp);
                        battle.PrepareDecks();
                        battle.StartBattle();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    return;
                }
                Console.WriteLine("Es muss zuerst ein Deck definiert werden!\n");
                return;
            }
            Console.WriteLine("Authentifizierung fehlgeschlagen/Nicht eingeloggt!\n");
        }

        static User FindOpponent(User user)
        {
            List<int> goodOpponents = UserDatabaseHandler.SearchOpponentsWithElo(user);

            User opp = SelectOpponent(goodOpponents);
            if (opp != null)
            {
                Console.WriteLine("Ein Gegner mit ähnlicher Elo wurde gefunden!");
                return opp;
            }

            List<int> allOpponents = UserDatabaseHandler.SearchOpponentsWithoutElo(user);

            opp = SelectOpponent(allOpponents);
            if (opp != null)
            {
                Console.WriteLine("Ein Gegner wurde gefunden!");
                return opp;
            }

            throw new Exception("Es existiert keine möglicher Gegner!");
        }

        static User SelectOpponent(List<int> opponents)
        {
            int oppCounter = opponents.Count;
            if (oppCounter == 1)
            {
                User opp = UserHandler.GetUserDataById(opponents[0]);
                return opp;
            }
            if (oppCounter > 1)
            {
                Random rnd = new Random();
                int oppSelector = rnd.Next(0, oppCounter);
                User opp = UserHandler.GetUserDataById(opponents[oppSelector]);
                return opp;
            }
            return null;
        }
    }
}
