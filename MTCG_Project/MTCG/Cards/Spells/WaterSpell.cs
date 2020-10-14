using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Spells
{
    public class WaterSpell : AbstractSpellcard
    {
        public WaterSpell() : base(Cardname.WaterSpell) { }

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
