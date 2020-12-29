using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class Wizard : AbstractMonstercard
    {
        public Wizard(float damage) : base(Cardname.Wizard, damage) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            return false;
        }
    }
}
