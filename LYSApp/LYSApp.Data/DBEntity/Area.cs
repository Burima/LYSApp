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
    
    public partial class Area
    {
        public Area()
        {
            this.PGDetails = new HashSet<PGDetail>();
        }
    
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<PGDetail> PGDetails { get; set; }
    }
}
