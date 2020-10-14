using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class Wizard : AbstractMonstercard
    {
        public Wizard() : base(Cardname.Wizard) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            return false;
        }
    }
}
