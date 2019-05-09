using System;
using System.Data;
using System.Data.SqlClient;

namespace DbAccess
{
    public class BaseRepository
    {
        string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        internal DataTable ExecuteQuery(string q)
        {
            DataTable ds = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(q, con))
            using (SqlDataAdapter dap = new SqlDataAdapter(command))
            {
                dap.Fill(ds);
            }

            return ds;
        }

        internal int ExecuteNonQuery(string q)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand(q, con))
            {
                con.Open();

                rowsAffected = com.ExecuteNonQuery();
            }


            return rowsAffected;
        }

        internal int ExecuteNonQueryScalar(string q)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand(q, con))
            {
                con.Open();

                return (int)com.ExecuteScalar();
            }

        }
    }
}
