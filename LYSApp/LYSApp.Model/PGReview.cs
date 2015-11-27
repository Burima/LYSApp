using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class PGReview
    {
        public int PGReviewID { get; set; }
        public long UserID { get; set; }
        public string Comments { get; set; }
        public Nullable<decimal> Rating { get; set; }
        public Nullable<System.DateTime> CommentTime { get; set; }
        public int PGDetailID { get; set; }

        public virtual PGDetail PGDetail { get; set; }
        public virtual User User { get; set; }
    }
}
