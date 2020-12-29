using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards
{
    public abstract class AbstractMonstercard : ICard
    {
        public Cardname name { get; set; }
        public int damage { get; set; }
        public Element_types element_type { get; set; }
        public string type { get; set; }
        public AbstractMonstercard(Cardname name, int damage)
        {
            this.name = name;

            if (name == Cardname.Dragon)
            {
                this.damage = damage;
                element_type = Element_types.Fire;
                type = "Monster";
            }
            else if (name == Cardname.FireElf)
            {
                this.damage = damage;
                element_type = Element_types.Fire;
                type = "Monster";
            }
            else if (name == Cardname.WaterGoblin)
            {
                this.damage = damage;
                element_type = Element_types.Water;
                type = "Monster";
            }
            else if (name == Cardname.Knight)
            {
                this.damage = damage;
                element_type = Element_types.Normal;
                type = "Monster";
            }
            else if (name == Cardname.Kraken)
            {
                this.damage = damage;
                element_type = Element_types.Water;
                type = "Monster";
            }
            else if (name == Cardname.Ork)
            {
                this.damage = damage;
                element_type = Element_types.Normal;
                type = "Monster";
            }
            else if (name == Cardname.Wizard)
            {
                this.damage = damage;
                element_type = Element_types.Water;
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
            if (this.element_type == Element_types.Fire && other.element_type == Element_types.Water)
            {
                return true;
            }
            if (this.element_type == Element_types.Normal && other.element_type == Element_types.Fire)
            {
                return true;
            }
            if (this.element_type == Element_types.Water && other.element_type == Element_types.Normal)
            {
                return true;
            }

            return false;
        }

        bool ElementalAdvantage(ICard other)
        {
            if (this.element_type == Element_types.Fire && other.element_type == Element_types.Normal)
            {
                return true;
            }
            if (this.element_type == Element_types.Normal && other.element_type == Element_types.Water)
            {
                return true;
            }
            if (this.element_type == Element_types.Water && other.element_type == Element_types.Fire)
            {
                return true;
            }

            return false;
        }
    }
}
