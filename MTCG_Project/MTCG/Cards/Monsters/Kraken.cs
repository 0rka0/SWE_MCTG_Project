using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards.Monsters
{
    public class Kraken : AbstractMonstercard
    {
        public Kraken() : base(Cardname.Kraken) { }

        public override bool SpecialBehavior(ICard otherCard)
        {
            return false;
        }
    }
}
