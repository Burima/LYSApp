using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class Area
    {

        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<PGDetail> PGDetails { get; set; }
    }
}
