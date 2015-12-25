using LYSApp.Model;
using System;

namespace LYSApp.Domain.TransactionManagement
{
    public interface ITransactionManagement
    {
        int AddTransaction(BookingDetailsViewModel bookingDetailsViewModel, string orderID, long userID,DateTime bookingFromDate);
        int UpdateTransaction(LYSApp.Model.Transaction transaction);
    }
}
