using NUnit.Framework;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.Test
{
    public class CardConversionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DummyCard_Dragon_Conversion()
        {
            DummyCard card = new DummyCard();
            card.id = "abc";
            card.damage = 20;
            card.name = "Dragon";

            ICard card2 = DummyCardConverter.Convert(card);

            Assert.AreEqual(card2.element_type, Element_types.Fire);
        }

        [Test]
        public void DummyCard_Waterspell_Conversion()
        {
            DummyCard card = new DummyCard();
            card.id = "abc";
            card.damage = 20;
            card.name = "WaterSpell";

            ICard card2 = DummyCardConverter.Convert(card);

            Assert.AreEqual(card2.element_type, Element_types.Water);
        }

        [Test]
        public void DummyCard_Knight_Conversion()
        {
            DummyCard card = new DummyCard();
            card.id = "abc";
            card.damage = 20;
            card.name = "Knight";

            ICard card2 = DummyCardConverter.Convert(card);

            Assert.AreEqual(card2.element_type, Element_types.Normal);
        }

        [Test]
        public void DummyCard_Banana_Fail_Conversion()
        {
            DummyCard card = new DummyCard();
            card.id = "abc";
            card.damage = 20;
            card.name = "Banana";

            ICard card2 = DummyCardConverter.Convert(card);

            Assert.AreEqual(card2, null);
        }
    }
}
