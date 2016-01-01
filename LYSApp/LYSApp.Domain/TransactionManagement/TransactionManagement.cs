using LYSApp.Data.DBRepository;
using LYSApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model.Constants;

namespace LYSApp.Domain.TransactionManagement
{
    public class TransactionManagement : ITransactionManagement
    {
        private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.Transaction> transactionRepository = null;
        private IBaseRepository<Data.DBEntity.Bed> bedRepository = null;

        /// <summary>
        /// Initialize repositories and UnitOfWork
        /// </summary>
        public TransactionManagement()
        {
            unitOfWork = new UnitOfWork();
            transactionRepository = new BaseRepository<Data.DBEntity.Transaction>(unitOfWork);
            bedRepository = new BaseRepository<Data.DBEntity.Bed>(unitOfWork);
        }

        /// <summary>
        /// This method gets called when user clicks for payment.
        /// </summary>
        /// <param name="bookingDetailsViewModel">booking details</param>
        /// <param name="orderID">transactionID generated before redirecting to payment gateway</param>
        /// <param name="userID">userID of logged on user</param>
        /// <param name="bookingFromDate">date from which user wants to book the bed</param>
        /// <returns>number of rows changed</returns>
        public int AddTransaction(BookingDetailsViewModel bookingDetailsViewModel, string orderID, long userID,DateTime bookingFromDate)
        {
            //get all the beds of the room which is empty
            var dbBed = (from bed in bedRepository.Get()
                         where bed.RoomID == bookingDetailsViewModel.RoomID && bed.Status == true && bed.BedStatus == (int)Constants.Bed_Status.Vacant && ((bed.UserID == 0) || (((DateTime)bed.BookingToDate - bookingFromDate).Days >= 30))//Status active for Bed and bed is empty (zero)
                         select bed
                        ).FirstOrDefault();
            if (dbBed != null && dbBed.BedID > 0)
            {
                //Lock the bed for the current transaction
                dbBed.BedStatus = (int)Constants.Bed_Status.InProgress;
                bedRepository.Update(dbBed);

                //add a record in Transaction table
                Data.DBEntity.Transaction dbTransaction = new Data.DBEntity.Transaction()
                {
                    OrderID = orderID,
                    TransactionStatusID = (int)Constants.Transaction_Status.inprogress,
                    amount = bookingDetailsViewModel.Price,
                    productinfo = bookingDetailsViewModel.Address,
                    IsValidTransaction=false,
                    CreatedOn = DateTime.Now,
                    LastUpdatedOn = DateTime.Now,
                    UserID = userID,
                    BedID = dbBed.BedID
                };
                transactionRepository.Insert(dbTransaction);//Inserting new lead
                
                
            }

            return unitOfWork.SaveChanges();//Saving the changes to DB
        }

        /// <summary>
        /// This method is to update the transaction status ans well as bedStatus after returning back from PaymentGateway
        /// </summary>
        /// <param name="transaction">transaction details from payment gateway</param>
        /// <returns>number of rowas updated</returns>
        public int UpdateTransaction(LYSApp.Model.Transaction transaction)
        {
            //update transaction
            var dbTransaction = transactionRepository.Get(t => t.OrderID == transaction.OrderID).FirstOrDefault();
            dbTransaction.mode = transaction.mode;
            dbTransaction.TransactionStatusID = transaction.TransactionStatusID;
            dbTransaction.amount = transaction.amount;            
            dbTransaction.Error = transaction.Error;
            dbTransaction.PG_TYPE = transaction.PG_TYPE;
            dbTransaction.bank_ref_num = transaction.bank_ref_num;
            dbTransaction.payuMoneyId = transaction.payuMoneyId;
            dbTransaction.additionalCharges = transaction.additionalCharges;
            dbTransaction.IsValidTransaction = transaction.IsValidTransaction;
            dbTransaction.LastUpdatedOn = DateTime.Now;
            transactionRepository.Update(dbTransaction);

            //update bed status
            var dbBed = bedRepository.Get(b => b.BedID == dbTransaction.BedID).FirstOrDefault();
            dbBed.BedStatus = transaction.TransactionStatusID == (int)Constants.Transaction_Status.success && transaction.IsValidTransaction ? (int)Constants.Bed_Status.Booked : (int)Constants.Bed_Status.Vacant;
            dbBed.LastUpdatedOn = DateTime.Now;
            bedRepository.Update(dbBed);
            return unitOfWork.SaveChanges();
            
        }
    }
}
