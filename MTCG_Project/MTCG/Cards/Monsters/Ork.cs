using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class Ork : AbstractMonstercard
    {
        public Ork(float damage) : base(Cardname.Ork, damage) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            if (otherCard.name == Cardname.Wizard)
            {
                return true;
            }
            return false;
        }
    }
}
