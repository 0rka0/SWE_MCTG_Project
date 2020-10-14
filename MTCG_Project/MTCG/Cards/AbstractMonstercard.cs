using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards
{
    public abstract class AbstractMonstercard : ICard
    {
        public Cardname name { get; set; }
        public int damage { get; set; }
        public Element_type element_type { get; set; }
        public string type { get; set; }
        public AbstractMonstercard(Cardname name)
        {
            this.name = name;

            if (name == Cardname.Dragon)
            {
                damage = 100;
                element_type = Element_type.Fire;
                type = "Monster";
            }
            else if (name == Cardname.FireElf)
            {
                damage = 60;
                element_type = Element_type.Fire;
                type = "Monster";
            }
            else if (name == Cardname.Goblin)
            {
                damage = 40;
                element_type = Element_type.Normal;
                type = "Monster";
            }
            else if (name == Cardname.Knight)
            {
                damage = 80;
                element_type = Element_type.Normal;
                type = "Monster";
            }
            else if (name == Cardname.Kraken)
            {
                damage = 60;
                element_type = Element_type.Water;
                type = "Monster";
            }
            else if (name == Cardname.Ork)
            {
                damage = 60;
                element_type = Element_type.Normal;
                type = "Monster";
            }
            else if (name == Cardname.Wizard)
            {
                damage = 80;
                element_type = Element_type.Water;
                type = "Monster";
            }
        }

        public int CombatBehavior(ICard otherCard)
        {
            if(SpecialBehavior(otherCard))
            {
                return 0;
            }

            return damage;
        }

        public abstract bool SpecialBehavior(ICard otherCard);
    }
}
