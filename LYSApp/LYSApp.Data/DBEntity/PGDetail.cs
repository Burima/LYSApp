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
    
    public partial class PGDetail
    {
        public PGDetail()
        {
            this.Houses = new HashSet<House>();
        }
    
        public int PGDetailID { get; set; }
        public string PGName { get; set; }
        public int AreaID { get; set; }
        public long OwnerID { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual ICollection<House> Houses { get; set; }
        public virtual User User { get; set; }
    }
}
