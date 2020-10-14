using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monster
{
    public class Dragon : AbstractMonstercard
    {
        public Dragon() : base(Cardname.Dragon){}

        public override bool SpecialBehavior(ICard otherCard)
        {
            if(otherCard.name == Cardname.FireElf)
            {
                return true;
            }
            return false;
        }
    }
}
