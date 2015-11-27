﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class PGDetail
    {
        public int PGDetailID { get; set; }
        public string PGName { get; set; }
        public int AreaID { get; set; }
        public long UserID { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public bool IsPg { get; set; }
        public int PGReviewID { get; set; }
        public string Description { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<House> Houses { get; set; }
        public virtual PGReview PGReview { get; set; }
    }
}
