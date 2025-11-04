using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spain_cup_03_11_2025.DAL.Entities
{
    public class Goal
    {
        public int ID { get; set; }

        public int PlayerID { get; set; }
        public Player Player { get; set; }

        public int MatchID { get; set; }
        public Match Match { get; set; }

        public int Minute { get; set; }  
    }
}
