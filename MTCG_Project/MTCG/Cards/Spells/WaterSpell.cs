using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Spells
{
    public class WaterSpell : AbstractSpellcard
    {
        public WaterSpell(float damage) : base(Cardname.WaterSpell, damage) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            if (otherCard.name == Cardname.Kraken)
            {
                return true;
            }
            return false;
        }
    }
}
