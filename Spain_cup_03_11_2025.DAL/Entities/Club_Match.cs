using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spain_cup_03_11_2025.DAL.Entities
{
    public class Club_Match
    {
        public int ID { get; set; }

        public int Club1_id { get; set; }

        public int Club2_id { get; set; }

        [ForeignKey("Club1_id")]
        public Club Club1 { get; set; }

        [ForeignKey("Club2_id")]
        public Club Club2 { get; set; }

        public int MatchID { get; set; }

        public Match Match { get; set; }

    }

}
