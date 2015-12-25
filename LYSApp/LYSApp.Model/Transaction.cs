using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
   public class Transaction
    {
        public int TransactionID { get; set; }
        public string OrderID { get; set; }
        public string mode { get; set; }
        public int TransactionStatusID { get; set; }
        public decimal amount { get; set; }
        public string productinfo { get; set; }
        public string Error { get; set; }
        public string PG_TYPE { get; set; }
        public string bank_ref_num { get; set; }
        public string payuMoneyId { get; set; }
        public string additionalCharges { get; set; }
        public bool IsValidTransaction { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime LastUpdatedOn { get; set; }
        public long UserID { get; set; }
        public int BedID { get; set; }

        public virtual Bed Bed { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
        public virtual User User { get; set; }
    }
}
