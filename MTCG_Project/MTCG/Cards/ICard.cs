using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG
{
    public enum Cardname
    {
        Goblin,
        Dragon,
        Wizard,
        Ork,
        Knight,
        Kraken,
        FireElf,
    }

    public enum Element_type
    {
        Fire,
        Water,
        Normal,
    }

    public interface ICard
    {
        Cardname name { get; set; }
        int damage { get; set; }
        Element_type element_type { get; set; }
        string type { get; set; }

        int CombatBehavior(ICard otherCard);
    }
}
