using System;
using System.Collections.Generic;
using System.Text;
using MTCG_Project.MTCG.Cards.Monsters;
using MTCG_Project.MTCG.Cards.Spells;

namespace MTCG_Project.MTCG.Cards
{
    static public class DummyCardConverter
    {
        public static ICard Convert(DummyCard card)
        {
            //All Monsters
            if (String.Compare(card.name, Cardname.Dragon.ToString()) == 0)
                return new Dragon(card.damage);
            if (String.Compare(card.name, Cardname.FireElf.ToString()) == 0)
                return new FireElf(card.damage);
            if (String.Compare(card.name, Cardname.Knight.ToString()) == 0)
                return new Knight(card.damage);
            if (String.Compare(card.name, Cardname.Kraken.ToString()) == 0)
                return new Kraken(card.damage);
            if (String.Compare(card.name, Cardname.Ork.ToString()) == 0)
                return new Ork(card.damage);
            if (String.Compare(card.name, Cardname.WaterGoblin.ToString()) == 0)
                return new WaterGoblin(card.damage);
            if (String.Compare(card.name, Cardname.Wizard.ToString()) == 0)
                return new Wizard(card.damage);

            //All Spells
            if (String.Compare(card.name, Cardname.FireSpell.ToString()) == 0)
                return new FireSpell(card.damage);
            if (String.Compare(card.name, Cardname.RegularSpell.ToString()) == 0)
                return new RegularSpell(card.damage);
            if (String.Compare(card.name, Cardname.WaterSpell.ToString()) == 0)
                return new WaterSpell(card.damage);

            return null;
        }
    }
}
