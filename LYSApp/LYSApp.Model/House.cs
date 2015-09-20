using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class House
    {

        public int HouseID { get; set; }
        public string HouseName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> OwnerID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
        public Nullable<int> LinkID { get; set; }
        public Nullable<int> LinkTypeID { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string DisplayName { get; set; }
        public bool IsPg { get; set; }
        public Nullable<int> PGDetailID { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> NoOfBathrooms { get; set; }
        public Nullable<int> NoOfBalconnies { get; set; }

        public virtual ICollection<HouseAmenity> HouseAmenities { get; set; }
        public virtual ICollection<HouseImage> HouseImages { get; set; }
        public virtual ICollection<HouseReview> HouseReviews { get; set; }
        public virtual PGDetail PGDetail { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
