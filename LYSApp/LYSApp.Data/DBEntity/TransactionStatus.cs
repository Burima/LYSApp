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
    
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int TransactionStatusID { get; set; }
        public string TransactionStatus1 { get; set; }
    
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
