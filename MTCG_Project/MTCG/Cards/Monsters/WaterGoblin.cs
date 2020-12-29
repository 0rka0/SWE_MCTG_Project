using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class WaterGoblin : AbstractMonstercard
    {
        public WaterGoblin(float damage) : base(Cardname.WaterGoblin, damage) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            if (otherCard.name == Cardname.Dragon)
            {
                return true;
            }
            return false;
        }
    }
}
