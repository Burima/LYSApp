using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LYSApp.Model
{
    public class Apartment
    {

        public int ApartmentID { get; set; }
        public string ApartmentName { get; set; }
        public string HouseNo { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> AreaID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public int OwnerID { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<Block> Blocks { get; set; }
    }
}
