using NUnit.Framework;
using System.IO;
using System;
using MTCG_Project.MTCG;
using MTCG_Project.MTCG.Cards.Monster;
using MTCG_Project.MTCG.Cards.Monsters;

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
            Element_type desiredElement = Element_type.Fire;
            string desiredType = "Monster";

            //Assert
            Assert.AreEqual(desiredDamage, card1.damage);
            Assert.AreEqual(desiredName, card1.name);
            Assert.AreEqual(desiredElement, card1.element_type);
            Assert.AreEqual(desiredType, card1.type);
        }

        public void createAll()
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
    }
}