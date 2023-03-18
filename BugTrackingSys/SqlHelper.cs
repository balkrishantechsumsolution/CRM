using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlHelper
/// </summary>
namespace SqlHelper
{
    public class data
    {
        private string _conString = string.Empty;

        private readonly IConfiguration Configuration;


        public data(IConfiguration Configuration)
        {
            _conString = Configuration.GetConnectionString("TrackBugsContext");
        }

        public data()
        {
            _conString = Configuration.GetConnectionString("TrackBugsContext");
        }

    
        public int ExecuteNonQuery(string query)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (query.StartsWith("INSERT") | query.StartsWith("insert") | query.StartsWith("UPDATE") | query.StartsWith("update") | query.StartsWith("DELETE") | query.StartsWith("delete"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            int retval;
            try
            {
                cnn.Open();
                cmd.CommandTimeout = 300;
                retval = cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return retval;
        }
        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (query.StartsWith("INSERT") | query.StartsWith("insert") | query.StartsWith("UPDATE") | query.StartsWith("update") | query.StartsWith("DELETE") | query.StartsWith("delete"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
            cnn.Open();
            cmd.CommandTimeout = 300;
            int retval = cmd.ExecuteNonQuery();
            cnn.Close();

            if (cmd.Parameters.Count > 0)
            {
                cmd.Parameters.Clear();
            }

            return retval;
        }
        public object ExecuteScalar(string query)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (query.StartsWith("SELECT") | query.StartsWith("select"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            cmd.CommandTimeout = 300;
            cnn.Open();
            object retval = cmd.ExecuteNonQuery();
            cnn.Close();
            return retval;
        }
        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (query.StartsWith("SELECT") | query.StartsWith("select"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
            cmd.CommandTimeout = 300;
            cnn.Open();
            object retval = cmd.ExecuteScalar();
            cnn.Close();

            if (cmd.Parameters.Count > 0)
            {
                cmd.Parameters.Clear();
            }

            return retval;
        }
        public SqlDataReader ExecuteReader(string query)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandTimeout = 300;
            if (query.StartsWith("SELECT") | query.StartsWith("select"))
            {
                cmd.CommandType = CommandType.Text;
                cnn.Open();
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandTimeout = 300;
            if (query.StartsWith("SELECT") | query.StartsWith("select"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
            cnn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public DataSet ExecuteDataSet(string query)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (query.StartsWith("SELECT") | query.StartsWith("select") | query.StartsWith("exec"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            cmd.CommandTimeout = 300;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet ExecuteDataSet(string query, params SqlParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (query.StartsWith("SELECT") | query.StartsWith("select"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
            cmd.CommandTimeout = 300;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (cmd.Parameters.Count > 0)
            {
                cmd.Parameters.Clear();
            }

            return ds;
        }
        public DataTable ExecuteDataTable(string query)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (query.StartsWith("SELECT") | query.StartsWith("select"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            cmd.CommandTimeout = 300;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (cmd.Parameters.Count > 0)
            {
                cmd.Parameters.Clear();
            }

            return dt;
        }
        public DataTable ExecuteDataTable(string query, params SqlParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (query.StartsWith("SELECT") | query.StartsWith("select"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
            cmd.CommandTimeout = 1200;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (cmd.Parameters.Count > 0)
            {
                cmd.Parameters.Clear();
            }

            return dt;
        }
        // Return Stored Procedure return 
        public int ReturnValue(string query, params SqlParameter[] parameters)
        {
            int retval = -101;
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandTimeout = 300;
            if (query.StartsWith("SELECT") | query.StartsWith("select"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@return_value";
            param.DbType = DbType.Int32;
            param.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                retval = (int)cmd.Parameters["@return_value"].Value;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }


            return retval;
        }

        public DataTable ExecuteDataTableNon(string query, params SqlParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(query, cnn);
            try
            {
                if (query.StartsWith("INSERT") | query.StartsWith("insert") | query.StartsWith("UPDATE") | query.StartsWith("update") | query.StartsWith("DELETE") | query.StartsWith("delete"))
                {
                    cmd.CommandType = CommandType.Text;
                }
                else
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
                cnn.Open();
                cmd.CommandTimeout = 300;
                var retval = cmd.ExecuteReader();

                var dataTable = new DataTable();
                dataTable.Load(retval);

                cnn.Close();

                if (cmd.Parameters.Count > 0)
                {
                    cmd.Parameters.Clear();
                }

                return dataTable;
            }
            catch (Exception e)
            {
                DataTable dte = new DataTable();
                dte.Columns.Add("ErrorMessage");
                object[] o = { e.Message };
                dte.Rows.Add(o);
                return dte;
            }

        }
    }
}