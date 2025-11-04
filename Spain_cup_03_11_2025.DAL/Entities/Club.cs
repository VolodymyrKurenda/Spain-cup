using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spain_cup_03_11_2025.DAL.Entities
{
    public class Club
    {
        public int ID { get; set; }

        public string Club_Name { get; set; }

        public string City { get; set; }

        public int Wins { get; set; }

        public int Lose { get; set; }

        public int Tie { get; set; }

        public int Goals_scored { get; set; }

        public int Goals_Lost { get; set; }

        public List<Player> Players { get; set; }

        public List<Club_Match> Club1_Matches { get; set; } = new();

        public List<Club_Match> Club2_Matches { get; set; } = new();

    }
}
