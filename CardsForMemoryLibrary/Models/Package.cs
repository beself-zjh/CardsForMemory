using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CardsForMemoryLibrary.Models {
    public class Package {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string NeckName { get; set; }
        public string Author { get; set; }
        public int Style { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime AmendTime { get; set; }
    }
}
