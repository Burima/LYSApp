using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class HouseImage
    {
        public int HouseImageID { get; set; }
        public string ImagePath { get; set; }
        public int HouseID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }

        public virtual House House { get; set; }
    }
}
