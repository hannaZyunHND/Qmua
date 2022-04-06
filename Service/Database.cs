using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Database
    {
        public static DataTable GetData(string query)
        {
            //String connectionString = Utility.XMLConfig_getValue("DefaultConnection");
            String connectionString = Const.DefaultConnection;
            DataTable dtResult = new DataTable();            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);                
                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();                    
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dtResult.Columns.Add(reader.GetName(i));
                    }
                    while (reader.Read())
                    {
                        DataRow drBody = dtResult.NewRow();
                        foreach (DataColumn column in dtResult.Columns)
                        {
                            drBody[column.ColumnName] = reader[column.ColumnName];                            
                        }                        
                        dtResult.Rows.Add(drBody);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return dtResult;
        }
    }
}
