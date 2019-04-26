using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CardsForMemoryLibrary.Models {
    public class Package {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Styles Style { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    ///     风格类型
    /// </summary>
    public enum Styles {
        defaulted = 0
    }
}
