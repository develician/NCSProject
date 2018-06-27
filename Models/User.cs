using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Models
{
    class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int borrowedNumber { get; set; }
        public int delayedCnt { get; set; }
    }
}
