using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class FireElf : AbstractMonstercard
    {
        public FireElf(int damage) : base(Cardname.FireElf, damage) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            return false;
        }
    }
}
