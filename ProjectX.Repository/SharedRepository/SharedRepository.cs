
/////using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;

namespace Infrastructure.Models
{
    public class SharedRepository
    {
        public static string UserconnectionString = "Data Source=.;Initial Catalog=Doctors; user id=sa;password=sadev123; Persist Security Info=True;MultipleActiveResultSets=True";
        public static string connectionString = "Data Source=.;Initial Catalog=Doctors; user id=sa;password=sadev123; Persist Security Info=True;MultipleActiveResultSets=True";
        //public static string UserconnectionString = "Data Source=148.72.232.112;Initial Catalog=Doctors; user id=Doctors;password=sadev123; Persist Security Info=True;MultipleActiveResultSets=True";
        //public static string connectionString = "Data Source=148.72.232.112;Initial Catalog=Doctors; user id=Doctors;password=sadev123; Persist Security Info=True;MultipleActiveResultSets=True";
        //public static string UserconnectionString = "Data Source=148.72.232.112;Initial Catalog=Doctors; user id=Doctors;password=sadev123; Persist Security Info=True;MultipleActiveResultSets=True";
        //public static string connectionString = "Data Source=148.72.232.112;Initial Catalog=Doctors; user id=Doctors;password=sadev123; Persist Security Info=True;MultipleActiveResultSets=True";

        public static SqlConnection connection = new SqlConnection(connectionString);

    }
}
