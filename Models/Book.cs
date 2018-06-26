using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Models
{
    class Book
    {
        public int id { get; set; }
        public string isbn { get; set; }
        public string name { get; set; }
        public string publisher { get; set; }
        public int page { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public int isBorrowed { get; set; }
        public DateTime borrowedAt { get; set; }
        public DateTime returnedAt { get; set; }

    }
}
