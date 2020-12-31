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
            card1 = new Dragon(100);
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
            ICard card1 = new Dragon(100);
            ICard card2 = new FireElf(60);
            ICard card3 = new WaterGoblin(40);
            ICard card4 = new Knight(80);
            ICard card5 = new Kraken(80);
            ICard card6 = new Ork(70);
            ICard card7 = new Wizard(90);

            Cardname desiredName1 = Cardname.Dragon;
            Cardname desiredName2 = Cardname.FireElf;
            Cardname desiredName3 = Cardname.WaterGoblin;
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
            ICard card1 = new FireSpell(100);
            ICard card2 = new RegularSpell(100);
            ICard card3 = new WaterSpell(100);

            Cardname desiredName1 = Cardname.FireSpell;
            Cardname desiredName2 = Cardname.RegularSpell;
            Cardname desiredName3 = Cardname.WaterSpell;

            Assert.AreEqual(desiredName1, card1.name);
            Assert.AreEqual(desiredName2, card2.name);
            Assert.AreEqual(desiredName3, card3.name);
        }

        [Test]
        public void Dragon_Goblin()
        {
            ICard Dragon = new Dragon(100);
            ICard Goblin = new WaterGoblin(40);

            float desiredDragonDamage = 100;
            float desiredGoblinDamage = 0;

            float actualDragonDamage = Dragon.CombatBehavior(Goblin);
            float actualGoblinDamage = Goblin.CombatBehavior(Dragon);

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