using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LYSApp.Model
{
    public class Block
    {
        public int BlockID { get; set; }
        public string BlockName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> ApartmentID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual ICollection<House> Houses { get; set; }
    }
}
