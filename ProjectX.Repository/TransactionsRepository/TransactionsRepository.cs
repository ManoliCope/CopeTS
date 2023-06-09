using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace Infrastructure.Models
{
    #region Classes

    public class Transactions
    {
        public int CarID { get; set; }
        public int UserID { get; set; }
        public int NumberofDays { get; set; }
        public int TransactionID { get; set; }

        public string Date { get; set; }
        public string Brand { get; set; }
        public string Price { get; set; }


    }

    #endregion


    #region interface
    public interface ITransactions
    {
        string SaveTransaction(List<Transactions> SelectedCars);
        List<Transactions> GetAllTransactions();

    }
    #endregion

    #region Functions

    public class TransactionsRepository : ITransactions
    {
        public List<Transactions> GetAllTransactions()
        {
            SqlConnection connection = new SqlConnection(SharedRepository.connectionString);

            var query = " SELECT TransactionID, Brand, [NumofDays] numberofDays, Price, CONVERT(VARCHAR(10), [Date], 105) as Date FROM [CarRental].[dbo].[Transactions] t1 left join [CarRental].[dbo].[Cars] t2 on t1.CarID = t2.CarID";
            List<Transactions> result = connection.Query<Transactions>(query, commandType: CommandType.Text).AsList();

            return result;
        }

        public string SaveTransaction(List<Transactions> SelectedCars)
        {
            SqlConnection connection = new SqlConnection(SharedRepository.connectionString);
            var query = "";

            foreach (Transactions car in SelectedCars)
            {
                query += " Insert into [CarRental].[dbo].[Transactions](CarID, NumofDays, UserID, Date) select '" + car.CarID + "','" + car.NumberofDays + "','" + car.UserID + "', getdate()   " +
                         " update [CarRental].[dbo].[Cars] set Availability = 'Booked' where Carid = '" + car.CarID + "'  " ;
            }

            var result = connection.Query(query, commandType: CommandType.Text).FirstOrDefault();
            return "";
        }

    }
    #endregion

}
