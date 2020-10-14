using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards
{
    public abstract class AbstractSpellcard : ICard
    {
        public Cardname name { get; set; }
        public int damage { get; set; }
        public Element_type element_type { get; set; }
        public string type { get; set; }
        public AbstractSpellcard(Cardname name)
        {
            this.name = name;

            if (name == Cardname.FireSpell)
            {
                damage = 100;
                element_type = Element_type.Fire;
                type = "Spell";
            }
            else if (name == Cardname.WaterSpell)
            {
                damage = 100;
                element_type = Element_type.Water;
                type = "Spell";
            }
            else if (name == Cardname.NormalSpell)
            {
                damage = 100;
                element_type = Element_type.Normal;
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
