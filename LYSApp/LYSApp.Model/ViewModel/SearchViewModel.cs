using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class SearchViewModel
    {
        [Required]
        public int AreaID { get; set; }

        public int Gender { get; set; }
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:d}")]
        [Display(Name = "Booking From")]
        [DataType(DataType.Date)]
        public DateTime BookingFromDate { get; set; }
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:d}")]
        [Display(Name = "Booking Upto")]
        [DataType(DataType.Date)]
        public DateTime BookingToDate { get; set; }

        public int SharingType { get; set; }

    }
    public class SearchAreaViewModel
    {
        public int AreaID { get; set; }

        public string AreaName { get; set; }

        public string CityName { get; set; }
    }

    public class SearchResultViewModel
    {
        public int PGDetailID { get; set; }
        public string PGName { get; set; }
        public House MinimumRentHouse { get; set; }
    }
    public class PropertyDetailsViewModel
    {
        public int PGDetailsID { get; set; }

        public string PGName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public Nullable<int> Gender { get; set; }

        public Nullable<decimal> Latitude { get; set; }

        public Nullable<decimal> Longitude { get; set; }

        public IList<House> HouseList { get; set; }

    }
    public class BookingDetailsViewModel
    {
        public DateTime BookingStartDate { get; set; }

        public DateTime BookingEndDate { get; set; }

        public int HouseID { get; set; }

        public int RoomID { get; set; }

        public int Price { get; set; }

        public string RoomName { get; set; }

        public User User { get; set; }

        public long OwnerID { get; set; }

    }
}
