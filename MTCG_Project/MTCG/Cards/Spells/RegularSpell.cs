using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Spells
{
    public class RegularSpell : AbstractSpellcard
    {
        public RegularSpell(float damage) : base(Cardname.RegularSpell, damage) { }

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
