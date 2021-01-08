using NUnit.Framework;
using MTCG_Project.MTCG.Cards;
using MTCG_Project.MTCG.Cards.Monsters;
using MTCG_Project.MTCG.Cards.Spells;

namespace MTCG_Project.Test
{
    public class BattleCardsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Dragon_Goblin()
        {
            ICard card1 = new Dragon(100);
            ICard card2 = new WaterGoblin(40);

            float desiredCard1Damage = 100;
            float desiredCard2Damage = 0;

            float actualCard1Damage = card1.CombatBehavior(card2);
            float actualCard2Damage = card2.CombatBehavior(card1);

            Assert.AreEqual(desiredCard1Damage, actualCard1Damage);
            Assert.AreEqual(desiredCard2Damage, actualCard2Damage);
        }

        [Test]
        public void Dragon_Knight()
        {
            ICard card1 = new Dragon(100);
            ICard card2 = new Knight(40);

            float desiredCard1Damage = 100;
            float desiredCard2Damage = 40;

            float actualCard1Damage = card1.CombatBehavior(card2);
            float actualCard2Damage = card2.CombatBehavior(card1);

            Assert.AreEqual(desiredCard1Damage, actualCard1Damage);
            Assert.AreEqual(desiredCard2Damage, actualCard2Damage);
        }

        [Test]
        public void Dragon_Waterspell()
        {
            ICard card1 = new Dragon(100);
            ICard card2 = new WaterSpell(40);

            float desiredCard1Damage = 50;
            float desiredCard2Damage = 80;

            float actualCard1Damage = card1.CombatBehavior(card2);
            float actualCard2Damage = card2.CombatBehavior(card1);

            Assert.AreEqual(desiredCard1Damage, actualCard1Damage);
            Assert.AreEqual(desiredCard2Damage, actualCard2Damage);
        }

        [Test]
        public void Knight_Waterspell()
        {
            ICard card1 = new Knight(40);
            ICard card2 = new WaterSpell(40);

            float desiredCard1Damage = 0;
            float desiredCard2Damage = 20;

            float actualCard1Damage = card1.CombatBehavior(card2);
            float actualCard2Damage = card2.CombatBehavior(card1);

            Assert.AreEqual(desiredCard1Damage, actualCard1Damage);
            Assert.AreEqual(desiredCard2Damage, actualCard2Damage);
        }

        [Test]
        public void Kraken_Waterspell()
        {
            ICard card1 = new Kraken(40);
            ICard card2 = new WaterSpell(40);

            float desiredCard1Damage = 40;
            float desiredCard2Damage = 0;

            float actualCard1Damage = card1.CombatBehavior(card2);
            float actualCard2Damage = card2.CombatBehavior(card1);

            Assert.AreEqual(desiredCard1Damage, actualCard1Damage);
            Assert.AreEqual(desiredCard2Damage, actualCard2Damage);
        }

        [Test]
        public void Wizard_Ork()
        {
            ICard card1 = new Wizard(80);
            ICard card2 = new Ork(50);

            float desiredCard1Damage = 80;
            float desiredCard2Damage = 0;

            float actualCard1Damage = card1.CombatBehavior(card2);
            float actualCard2Damage = card2.CombatBehavior(card1);

            Assert.AreEqual(desiredCard1Damage, actualCard1Damage);
            Assert.AreEqual(desiredCard2Damage, actualCard2Damage);
        }

        [Test]
        public void FireElf_Dragon()
        {
            ICard card1 = new FireElf(60);
            ICard card2 = new Dragon(100);

            float desiredCard1Damage = 60;
            float desiredCard2Damage = 0;

            float actualCard1Damage = card1.CombatBehavior(card2);
            float actualCard2Damage = card2.CombatBehavior(card1);

            Assert.AreEqual(desiredCard1Damage, actualCard1Damage);
            Assert.AreEqual(desiredCard2Damage, actualCard2Damage);
        }
    }
}
