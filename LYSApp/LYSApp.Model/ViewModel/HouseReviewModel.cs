using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class HouseReviewModel
    {
        public string Comments { get; set; }

        public decimal Rating { get; set; }

        public int HouseID { get; set; }

        public House house { get; set; }

        public User user { get; set; }
    }
}
