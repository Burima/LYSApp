//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LYSApp.Data.DBEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room
    {
        public Room()
        {
            this.Beds = new HashSet<Bed>();
        }
    
        public int RoomID { get; set; }
        public int HouseID { get; set; }
        public int RoomNumber { get; set; }
        public int MonthlyRent { get; set; }
        public int Deposit { get; set; }
        public int NoOfBeds { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime LastUpdatedOn { get; set; }
    
        public virtual ICollection<Bed> Beds { get; set; }
        public virtual House House { get; set; }
    }
}
