using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using MTCG_Project.MTCG.Cards;
using MTCG_Project.MTCG.Cards.Monsters;
using MTCG_Project.MTCG.Cards.Spells;

namespace MTCG_Project.MTCG.NamespaceStore
{
    public class Pack
    {
        public ICard[] cards;

        public Pack()
        {
            cards = new ICard[5];
        }

        public void SetCards()
        {

        }
        
    }
}
