using NUnit.Framework;
using System.IO;
using System;
using MTCG_Project.MTCG.Cards;
using MTCG_Project.MTCG.Cards.Monsters;
using MTCG_Project.MTCG.Cards.Spells;
using MTCG_Project.MTCG.NamespaceUser;


namespace MCTG_Project.Test
{
    public class CardTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void createDragon()
        {
            //Arrange
            ICard card1;

            //Act
            card1 = new Dragon();
            Cardname desiredName = Cardname.Dragon;
            int desiredDamage = 100;
            Element_types desiredElement = Element_types.Fire;
            string desiredType = "Monster";

            //Assert
            Assert.AreEqual(desiredDamage, card1.damage);
            Assert.AreEqual(desiredName, card1.name);
            Assert.AreEqual(desiredElement, card1.element_type);
            Assert.AreEqual(desiredType, card1.type);
        }

        [Test]
        public void createAllMonstercards()
        {
            ICard card1 = new Dragon();
            ICard card2 = new FireElf();
            ICard card3 = new Goblin();
            ICard card4 = new Knight();
            ICard card5 = new Kraken();
            ICard card6 = new Ork();
            ICard card7 = new Wizard();

            Cardname desiredName1 = Cardname.Dragon;
            Cardname desiredName2 = Cardname.FireElf;
            Cardname desiredName3 = Cardname.Goblin;
            Cardname desiredName4 = Cardname.Knight;
            Cardname desiredName5 = Cardname.Kraken;
            Cardname desiredName6 = Cardname.Ork;
            Cardname desiredName7 = Cardname.Wizard;

            Assert.AreEqual(desiredName1, card1.name);
            Assert.AreEqual(desiredName2, card2.name);
            Assert.AreEqual(desiredName3, card3.name);
            Assert.AreEqual(desiredName4, card4.name);
            Assert.AreEqual(desiredName5, card5.name);
            Assert.AreEqual(desiredName6, card6.name);
            Assert.AreEqual(desiredName7, card7.name);
        }

        [Test]
        public void createAllSpellcards()
        {
            ICard card1 = new FireSpell();
            ICard card2 = new NormalSpell();
            ICard card3 = new WaterSpell();

            Cardname desiredName1 = Cardname.FireSpell;
            Cardname desiredName2 = Cardname.NormalSpell;
            Cardname desiredName3 = Cardname.WaterSpell;

            Assert.AreEqual(desiredName1, card1.name);
            Assert.AreEqual(desiredName2, card2.name);
            Assert.AreEqual(desiredName3, card3.name);
        }

        [Test]
        public void Dragon_Goblin()
        {
            ICard Dragon = new Dragon();
            ICard Goblin = new Goblin();

            int desiredDragonDamage = 100;
            int desiredGoblinDamage = 0;

            int actualDragonDamage = Dragon.CombatBehavior(Goblin);
            int actualGoblinDamage = Goblin.CombatBehavior(Dragon);

            Assert.AreEqual(desiredDragonDamage, actualDragonDamage);
            Assert.AreEqual(desiredGoblinDamage, actualGoblinDamage);
        }
    }

    public class StackTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddCards()
        {

        }
    }
}