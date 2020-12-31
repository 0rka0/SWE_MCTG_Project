using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards
{
    public interface ICard
    {
        Cardname name { get; set; }
        float damage { get; set; }
        Element_types element_type { get; set; }
        string type { get; set; }

        float CombatBehavior(ICard otherCard);
    }
}
