using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class Dragon : AbstractMonstercard
    {
        public Dragon(float damage) : base(Cardname.Dragon, damage) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            if (otherCard.name == Cardname.FireElf)
            {
                return true;
            }
            return false;
        }
    }
}
