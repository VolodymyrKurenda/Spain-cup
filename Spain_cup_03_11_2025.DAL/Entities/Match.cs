using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spain_cup_03_11_2025.DAL.Entities
{
    public class Match
    {
        public int ID { get; set; }

        public int club1_scored { get; set; }

        public int club2_scored { get; set; }

        public DateOnly Date { get; set; } 

        public List<Club_Match> Club_Match { get; set; }

        public List<Player> playersscored { get; set; }

        public List<Goal> Goals { get; set; }
    }
}
