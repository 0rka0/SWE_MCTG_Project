using System;
using System.Collections.Generic;
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
                        battle.StartMatchmadeBattle();
                    }
                    catch (Exception e)
                    {
                        Output.WriteConsole(e.Message);
                    }
                    return;
                }
                Output.WriteConsole(Output.RatedMatchDeckNotDefined);
                return;
            }
            Output.WriteConsole(Output.AuthError);
        }

        public static void FriendBattleRequest(RequestContext request)
        {
            int userstate = UserHandler.AuthUser(request);
            if (userstate == 1 || userstate == 2)     
            {
                User user = UserHandler.GetUserDataByToken(request);
                User friend = UserHandler.GetUserDataByUsername(ExtractUsernameFromRessource(request.Ressource));
                if(friend != null && FriendlistHandler.CheckFriends(user,friend))
                {
                    if (user.deck_set && friend.deck_set)
                    {
                        BattleManager battle = new BattleManager(user, friend);
                        battle.PrepareDecks();
                        battle.StartFriendBattle();
                        return;
                    }
                    Output.WriteConsole(Output.FriendMatchDeckNotDefined);
                    return;
                }
                Output.WriteConsole(Output.FriendMatchNotInFl);
                return;
            }
            Output.WriteConsole(Output.AuthError);
        }

        static User FindOpponent(User user)
        {
            List<int> goodOpponents = UserDatabaseHandler.SearchOpponentsWithElo(user);

            User opp = SelectOpponent(goodOpponents);
            if (opp != null)
            {
                Output.WriteConsole(Output.RatedMatchGoodOpp);
                return opp;
            }

            List<int> allOpponents = UserDatabaseHandler.SearchOpponentsWithoutElo(user);

            opp = SelectOpponent(allOpponents);
            if (opp != null)
            {
                Output.WriteConsole(Output.RatedMatchBadOpp);
                return opp;
            }

            throw new Exception(Output.RatedMatchNoOpp);
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
        static string ExtractUsernameFromRessource(string ress)
        {
            return ress.Replace("/battles/", "");
        }
    }
}
