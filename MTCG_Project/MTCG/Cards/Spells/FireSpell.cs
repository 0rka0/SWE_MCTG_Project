using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Spells
{
    public class FireSpell : AbstractSpellcard
    {
        public FireSpell(int damage) : base(Cardname.FireSpell, damage) { }

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
