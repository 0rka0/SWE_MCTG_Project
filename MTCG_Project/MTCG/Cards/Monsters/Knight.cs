using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class Knight : AbstractMonstercard
    {
        public Knight(float damage) : base(Cardname.Knight, damage) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            if (otherCard.name == Cardname.WaterSpell)
            {
                return true;
            }
            return false;
        }
    }
}
