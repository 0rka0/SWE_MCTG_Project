using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards
{
    public abstract class AbstractSpellcard : ICard
    {
        public Cardname name { get; set; }
        public int damage { get; set; }
        public Element_types element_type { get; set; }
        public string type { get; set; }
        public AbstractSpellcard(Cardname name, int damage)
        {
            this.name = name;

            if (name == Cardname.FireSpell)
            {
                this.damage = damage;
                element_type = Element_types.Fire;
                type = "Spell";
            }
            else if (name == Cardname.WaterSpell)
            {
                this.damage = damage;
                element_type = Element_types.Water;
                type = "Spell";
            }
            else if (name == Cardname.RegularSpell)
            {
                this.damage = damage;
                element_type = Element_types.Normal;
                type = "Spell";
            }
        }

        public int CombatBehavior(ICard otherCard)
        {
            if (SpecialBehavior(otherCard))
            {
                return 0;
            }

            if (ElementalWeakness(otherCard))
            {
                return damage / 2;
            }
            if (ElementalAdvantage(otherCard))
            {
                return damage * 2;
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
