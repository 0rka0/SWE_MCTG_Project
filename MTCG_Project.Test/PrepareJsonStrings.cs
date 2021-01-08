using NUnit.Framework;
using Newtonsoft.Json;
using MTCG_Project.Interaction;
using MTCG_Project.MTCG.Cards;

namespace MTCG_Project.Test
{
    public class PrepareJsonStrings
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Package_String_Handling_IDs()
        {
            int counter = 0;
            DummyCard[] cards = new DummyCard[5];
            string str = "[{\"Id\":\"845f0dc7-37d0-426e-994e-43fc3ac83c08\", \"Name\":\"WaterGoblin\", \"Damage\": 10.0}, " +
                "{\"Id\":\"99f8f8dc-e25e-4a95-aa2c-782823f36e2a\", \"Name\":\"Dragon\", \"Damage\": 50.0}, " +
                "{\"Id\":\"e85e3976-7c86-4d06-9a80-641c2019a79f\", \"Name\":\"WaterSpell\", \"Damage\": 20.0}, " +
                "{\"Id\":\"1cb6ab86-bdb2-47e5-b6e4-68c5ab389334\", \"Name\":\"Ork\", \"Damage\": 45.0}, " +
                "{\"Id\":\"dfdd758f-649c-40f9-ba3a-8657f4b3439f\", \"Name\":\"FireSpell\",    \"Damage\": 25.0}]";
            string[] jsonStrings = PackageHandler.PrepareJsonStrings(str);

            foreach (string s in jsonStrings)
            {
                cards[counter] = JsonConvert.DeserializeObject<DummyCard>(jsonStrings[counter]);
                counter++;
            }

            Assert.AreEqual(cards[0].id, "845f0dc7-37d0-426e-994e-43fc3ac83c08");
            Assert.AreEqual(cards[1].id, "99f8f8dc-e25e-4a95-aa2c-782823f36e2a");
            Assert.AreEqual(cards[2].id, "e85e3976-7c86-4d06-9a80-641c2019a79f");
            Assert.AreEqual(cards[3].id, "1cb6ab86-bdb2-47e5-b6e4-68c5ab389334");
            Assert.AreEqual(cards[4].id, "dfdd758f-649c-40f9-ba3a-8657f4b3439f");
        }

        [Test]
        public void Package_String_Handling_Name()
        {
            int counter = 0;
            DummyCard[] cards = new DummyCard[5];
            string str = "[{\"Id\":\"845f0dc7-37d0-426e-994e-43fc3ac83c08\", \"Name\":\"WaterGoblin\", \"Damage\": 10.0}, " +
                "{\"Id\":\"99f8f8dc-e25e-4a95-aa2c-782823f36e2a\", \"Name\":\"Dragon\", \"Damage\": 50.0}, " +
                "{\"Id\":\"e85e3976-7c86-4d06-9a80-641c2019a79f\", \"Name\":\"WaterSpell\", \"Damage\": 20.0}, " +
                "{\"Id\":\"1cb6ab86-bdb2-47e5-b6e4-68c5ab389334\", \"Name\":\"Ork\", \"Damage\": 45.0}, " +
                "{\"Id\":\"dfdd758f-649c-40f9-ba3a-8657f4b3439f\", \"Name\":\"FireSpell\",    \"Damage\": 25.0}]";
            string[] jsonStrings = PackageHandler.PrepareJsonStrings(str);

            foreach (string s in jsonStrings)
            {
                cards[counter] = JsonConvert.DeserializeObject<DummyCard>(jsonStrings[counter]);
                counter++;
            }

            Assert.AreEqual(cards[0].name, "WaterGoblin");
            Assert.AreEqual(cards[1].name, "Dragon");
            Assert.AreEqual(cards[2].name, "WaterSpell");
            Assert.AreEqual(cards[3].name, "Ork");
            Assert.AreEqual(cards[4].name, "FireSpell");
        }

        [Test]
        public void Package_String_Handling_Damage()
        {
            int counter = 0;
            DummyCard[] cards = new DummyCard[5];
            string str = "[{\"Id\":\"845f0dc7-37d0-426e-994e-43fc3ac83c08\", \"Name\":\"WaterGoblin\", \"Damage\": 10.0}, " +
                "{\"Id\":\"99f8f8dc-e25e-4a95-aa2c-782823f36e2a\", \"Name\":\"Dragon\", \"Damage\": 50.0}, " +
                "{\"Id\":\"e85e3976-7c86-4d06-9a80-641c2019a79f\", \"Name\":\"WaterSpell\", \"Damage\": 20.0}, " +
                "{\"Id\":\"1cb6ab86-bdb2-47e5-b6e4-68c5ab389334\", \"Name\":\"Ork\", \"Damage\": 45.0}, " +
                "{\"Id\":\"dfdd758f-649c-40f9-ba3a-8657f4b3439f\", \"Name\":\"FireSpell\",    \"Damage\": 25.0}]";
            string[] jsonStrings = PackageHandler.PrepareJsonStrings(str);

            foreach (string s in jsonStrings)
            {
                cards[counter] = JsonConvert.DeserializeObject<DummyCard>(jsonStrings[counter]);
                counter++;
            }

            Assert.AreEqual(cards[0].damage, 10);
            Assert.AreEqual(cards[1].damage, 50);
            Assert.AreEqual(cards[2].damage, 20);
            Assert.AreEqual(cards[3].damage, 45);
            Assert.AreEqual(cards[4].damage, 25);
        }
    }
}
