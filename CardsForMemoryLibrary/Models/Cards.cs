using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CardsForMemoryLibrary.Models {
    public class Cards {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
