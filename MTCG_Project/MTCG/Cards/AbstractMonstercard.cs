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

            if(otherCard.type == "Spell")
            {
                if (ElementalWeakness(otherCard))
                {
                    return damage / 2;
                }
                if (ElementalAdvantage(otherCard))
                {
                    return damage * 2;
                }
            }

            return damage;
        }

        public abstract bool SpecialBehavior(ICard otherCard);

        bool ElementalWeakness(ICard other)
        {
            if (this.element_type == Element_type.Fire && other.element_type == Element_type.Water)
            {
                return true;
            }
            if (this.element_type == Element_type.Normal && other.element_type == Element_type.Fire)
            {
                return true;
            }
            if (this.element_type == Element_type.Water && other.element_type == Element_type.Normal)
            {
                return true;
            }

            return false;
        }

        bool ElementalAdvantage(ICard other)
        {
            if (this.element_type == Element_type.Fire && other.element_type == Element_type.Normal)
            {
                return true;
            }
            if (this.element_type == Element_type.Normal && other.element_type == Element_type.Water)
            {
                return true;
            }
            if (this.element_type == Element_type.Water && other.element_type == Element_type.Fire)
            {
                return true;
            }

            return false;
        }
    }
}
