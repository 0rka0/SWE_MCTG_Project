using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.Interaction;
using MTCG_Project.MTCG.NamespaceUser;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.MTCG.Battle
{
    public class BattleManager
    {
        User user1;
        User user2;
        List<string> log = new List<string>();
        int round;
        public BattleManager(User curUser, User oppUser)
        {
            user1 = curUser;
            user2 = oppUser;
            round = 1;
        }

        public void PrepareDecks()
        {
            user1.deck = UserCardsHandler.GenerateBattleDeck(user1);
            user2.deck = UserCardsHandler.GenerateBattleDeck(user2);
        }

        public void StartMatchmadeBattle()
        {
            int winner = GameFlow();

            MatchmadeBattleEnd(winner);

            foreach (string s in log)
                Console.WriteLine(s);
        }

        public void StartFriendBattle()
        {
            int winner = GameFlow();

            FriendBattleEnd(winner);

            foreach (string s in log)
                Console.WriteLine(s);
        }

        int GameFlow()
        {
            int winner = 0;
            while (round <= 100)
            {
                Turn();
                winner = CheckWinner();
                if (winner != 0)
                    break;
                round++;
            }
            return winner;
        }

        void Turn()
        {
            ICard card1 = user1.deck.GetRandomCard();
            ICard card2 = user2.deck.GetRandomCard();

            float card1EffectiveDamage = card1.CombatBehavior(card2);
            float card2EffectiveDamage = card2.CombatBehavior(card1);

            log.Add(String.Format("Round: {0}", round));

            if (card1EffectiveDamage > card2EffectiveDamage)
            {
                log.Add(String.Format("Player1: {0}({1}-->{2}) has won against Player2: {3}({4}-->{5})", card1.name, card1.damage, card1EffectiveDamage, card2.name, card2.damage, card2EffectiveDamage));
                user2.deck.RemoveCard();
                user1.deck.AddCard(card2);
                log.Add(String.Format("{0} has been transferred to Player 1\n", card2.name));
                return;
            }

            log.Add(String.Format("Player2: {0}({1}-->{2}) has won against Player1: {3}({4}-->{5})", card2.name, card2.damage, card2EffectiveDamage, card1.name, card1.damage, card1EffectiveDamage));
            user1.deck.RemoveCard();
            user2.deck.AddCard(card1);
            log.Add(String.Format("{0} has been transferred to Player 2\n", card1.name));
        }

        int CheckWinner()
        {
            if (user1.deck.GetLength() < 1)
                return 2;
            if (user2.deck.GetLength() < 1)
                return 1;
            return 0;
        }

        void MatchmadeBattleEnd(int winner)
        {
            if (winner != 0)
            {
                log.Add(String.Format("Player {0} won the battle!\n", winner));
                if (winner == 1)
                {
                    user1.wins++;
                    int prevElo1 = user1.elo;
                    int prevElo2 = user2.elo;
                    ChangeElo(user1, user2);
                    log.Add(String.Format("Player 1 Rating: {0} --> {1}\nPlayer 2 Rating: {2} --> {3}", prevElo1, user1.elo, prevElo2, user2.elo));
                }
                else
                {
                    user2.wins++;
                    int prevElo1 = user1.elo;
                    int prevElo2 = user2.elo;
                    ChangeElo(user2, user1);
                    log.Add(String.Format("Player 1 Rating: {0} --> {1}\nPlayer 2 Rating: {2} --> {3}", prevElo1, user1.elo, prevElo2, user2.elo));
                }
            }
            else
                log.Add("The battle has concluded in a draw\nRatings unchanged\n");

            user1.gamesPlayed++;
            user2.gamesPlayed++;

            UserHandler.UpdateAfterBattle(user1, user2);
        }

        void FriendBattleEnd(int winner)
        {
            if (winner != 0)
            {
                log.Add(String.Format("Player {0} won the battle!\n", winner));
            }
            else
                log.Add("The battle has concluded in a draw\nRatings unchanged\n");
        }

        void ChangeElo(User winner, User loser)
        {
            if ((winner.elo - loser.elo) > 50)
            {
                winner.elo += EloValues.LowestInc;
                loser.elo -= EloValues.LowestDec;
            }
            if ((winner.elo - loser.elo) > 25 && (winner.elo - loser.elo) <= 50)
            {
                winner.elo += EloValues.LowInc;
                loser.elo -= EloValues.LowDec;
            }
            if ((winner.elo - loser.elo) <= 25 && (loser.elo - winner.elo) <= 25)
            {
                winner.elo += EloValues.NormalInc;
                loser.elo -= EloValues.NormalDec;
            }
            if ((loser.elo - winner.elo) > 25 && (loser.elo - winner.elo) <= 50)
            {
                winner.elo += EloValues.HighInc;
                loser.elo -= EloValues.HighDec;
            }
            if ((loser.elo - winner.elo) > 50)
            {
                winner.elo += EloValues.HighestInc;
                loser.elo -= EloValues.LowestDec;
            }
        }
    }
}
