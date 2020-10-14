using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class Goblin : AbstractMonstercard
    {
        public Goblin() : base(Cardname.Goblin) { }

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
