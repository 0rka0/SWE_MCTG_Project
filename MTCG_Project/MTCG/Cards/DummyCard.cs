using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Cards
{
    public class DummyCard  //used for database access
    {
        public string id { get; set; }
        public string name { get; set; }
        public float damage { get; set; }
        public int packOrUserId { get; set; }         
    }
}
