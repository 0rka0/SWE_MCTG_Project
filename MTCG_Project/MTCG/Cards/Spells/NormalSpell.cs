using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Spells
{
    public class NormalSpell : AbstractSpellcard
    {
        public NormalSpell() : base(Cardname.NormalSpell) { }

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
