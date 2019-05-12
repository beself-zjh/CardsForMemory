using SQLite;
using System;

namespace CardsForMemoryLibrary.Models {
    public class Log {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Time { get; set; }
        public int CardId { get; set; }
        public int Proficiency { get; set; }
    }
}
