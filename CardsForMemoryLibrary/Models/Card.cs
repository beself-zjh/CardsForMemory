using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CardsForMemoryLibrary.Models {
    public class Card {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int PackageId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Options { get; set; }
        public DateTime UpdateTime { get; set; }//上次复习时间
        public int Proficiency { get; set; } //熟练度(下限)
    }
}
