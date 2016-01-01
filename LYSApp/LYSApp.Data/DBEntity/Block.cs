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
    
    public partial class Block
    {
        public Block()
        {
            this.Houses = new HashSet<House>();
        }
    
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
        public Nullable<long> CreatedBy { get; set; }
        public bool IsDefault { get; set; }
    
        public virtual ICollection<House> Houses { get; set; }
        public virtual Apartment Apartment { get; set; }
    }
}
