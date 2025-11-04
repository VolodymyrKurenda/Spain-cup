using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spain_cup_03_11_2025.DAL.Entities
{
    public class Player
    {
        public int ID { get; set; }

        public string FullName { get; set; }

        public string Country { get; set; }

        public int Number { get; set; }

        public string Position { get; set; }

        public Club Club { get; set; }

        public List<Goal> Goals { get; set; }
    }
}
