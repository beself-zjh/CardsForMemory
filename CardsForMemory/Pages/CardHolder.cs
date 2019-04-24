using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CardsForMemory
{
    public class CardHolder
    {
        static private int _Id = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public List<Card> Cards = new List<Card>();
        public CardHolder(string name="",string author = "", string description = "")
        {
            Id = _Id++;
            Name = name;
            Author = author;
            Description = description;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            Random random = new Random();
            Cards.Add(new Card
            {
                a = md5(random.Next().ToString()),
                b = md5(random.Next().ToString()),
                c = md5(random.Next().ToString())
            });
            Cards.Add(new Card
            {
                a = md5(random.Next().ToString()),
                b = md5(random.Next().ToString()),
                c = md5(random.Next().ToString())
            });
            Cards.Add(new Card
            {
                a = md5(random.Next().ToString()),
                b = md5(random.Next().ToString()),
                c = md5(random.Next().ToString())
            });
        }

        private string md5(string str)
        {
            return Convert.ToBase64String(MD5.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(str)));
        }
    }
}
