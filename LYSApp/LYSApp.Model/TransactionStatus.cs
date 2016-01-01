using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class TransactionStatus
    {
        public int TransactionStatusID { get; set; }
        public string TransactionStatus1 { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
