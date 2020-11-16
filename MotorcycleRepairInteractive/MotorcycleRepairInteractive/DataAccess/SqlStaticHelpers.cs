using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MRI.Core
{
    public static class SqlStaticHelpers
    {
        /// <summary>
        /// The connection string that points to the database in SQL server
        /// </summary>
        private static string connectionString =
            @"Data Source=LAPTOP-GE9KQ5E6;
              Initial Catalog=MotorcycleParts;

              Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        /// <summary>
        /// Helper method to read all the cell data from a table in SQL and 
        /// converts it to a DataTable.
        /// </summary>
        /// <param name="tableName">The name of the table passed in</param>
        /// <returns>DataTable that is the same as the SQL table</returns>
        public static DataTable ReadTableData(string tableName)
        {
            
            // Check to see if a tableName was passed in
            if (tableName.Length == 0) throw new Exception();

            // Create a new blank DataTable
            //DataTable table = new DataTable();

            // Create the query string from the passed in table name
            //var queryString = string.Format("SELECT * FROM dbo.{0}", tableName);

            // Create the query string from the passed in table name
            var queryString = string.Format("select * from dbo.{0} " , tableName);


            DataTable PageItemsDT = new DataTable("PageItems");
            // Create the connection to the SQL instance (uses the private property at 
            // the top of this class
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Creates new SqlDataAdapter for converting SQL table to
                // C# DataTable property
                using (var da = new SqlDataAdapter(queryString, connection))
                {
                    // Fills the DataTable from the SQL table
                    //da.FillSchema(PageItemsDV, SchemaType.Source, "PageItems");
                    //da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    //da.Fill(PageItemsDS, "PageItems");
                    da.Fill(PageItemsDT);
                }
            }
            // returns the DataTable
            return PageItemsDT;
        }
    }
}
