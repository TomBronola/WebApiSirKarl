using Sap.Data.Hana;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Translator;

namespace DirecLayer.Helpers
{
    public class MsSqlAccess
    {
        public string GetConnection(string sAppName)
        {
            var output = ConfigurationManager.ConnectionStrings[sAppName] != null ? ConfigurationManager.ConnectionStrings[sAppName].ToString() : "";
            return output;
        }
        public string AppSettings(string value)
        {
            var output = ConfigurationManager.AppSettings[value] != null ? ConfigurationManager.AppSettings[value].ToString() : "";
            return output;
        }

        #region ConnectionString
        public static string ConnectionString(string server,
                                        string dbUserId,
                                        string dbPassword,
                                        string database,
                                        bool refresh = false,
                                        string connectionString = "")
        {
            var output = new StringBuilder();

            try
            {
                output.Append($"Data Source={server};");
                output.Append("Persist Security Info=True;");
                output.Append($"User ID={dbUserId};");
                output.Append($"Password={dbPassword};");
                output.Append($"Initial Catalog={database};");
                output.Append("Connection Timeout=0;");

                if (refresh)
                { AppConfig.UpdateConnectionString(connectionString, output.ToString()); }
            }
            catch (Exception ex)
            { throw new Exception($"Error : {ex.Message}"); }

            return output.ToString();
        }

        public static string ConnectionString(string server,
                                        string dbUserId,
                                        string dbPassword,
                                        bool refresh = false,
                                        string connectionString = "")
        {
            var output = new StringBuilder();

            try
            {
                output.Append($"Data Source={server};");
                output.Append("Persist Security Info=True;");
                output.Append($"User ID={dbUserId};");
                output.Append($"Password={dbPassword};");
                output.Append($"Initial Catalog={AppConfig.AppSettings("SqlDatabase")};");
                output.Append("Connection Timeout=0;");

                if (refresh)
                { AppConfig.UpdateConnectionString(connectionString, output.ToString()); }
            }
            catch (Exception ex)
            { throw new Exception($"Error : {ex.Message}"); }

            return output.ToString();
        }

        public static string ConnectionString(string database,
                                        bool refresh = false,
                                        string connectionString = "")
        {
            var output = new StringBuilder();

            try
            {
                output.Append($"Data Source={AppConfig.AppSettings("SqlServer")};");
                output.Append("Persist Security Info=True;");
                output.Append($"User ID={AppConfig.AppSettings("SqlUserId")};");
                output.Append($"Password={AppConfig.AppSettings("SqlPassword")};");
                output.Append($"Initial Catalog={database};");
                output.Append("Connection Timeout=0;");

                if (refresh)
                { AppConfig.UpdateConnectionString(connectionString, output.ToString()); }
            }
            catch (Exception ex)
            { throw new Exception($"Error : {ex.Message}"); }

            return output.ToString();
        }

        public static string ConnectionString(bool refresh = false,
                                        string connectionString = "")
        {
            var output = new StringBuilder();

            try
            {
                output.Append($"Data Source={AppConfig.AppSettings("SqlServer")};");
                output.Append("Persist Security Info=True;");
                output.Append($"User ID={AppConfig.AppSettings("SqlUserId")};");
                output.Append($"Password={AppConfig.AppSettings("SqlPassword")};");
                output.Append($"Initial Catalog={AppConfig.AppSettings("SqlDatabase")};");
                output.Append("Connection Timeout=0;");

                if (refresh)
                { AppConfig.UpdateConnectionString(connectionString, output.ToString()); }
            }
            catch (Exception ex)
            { throw new Exception($"Error : {ex.Message}"); }

            return output.ToString();
        }

        public static string ConnectionString(string connectionName)
        {
            var output = new StringBuilder();

            try
            {
                output.Append(AppConfig.GetConnection(connectionName));
            }
            catch (Exception ex)
            { throw new Exception($"Error : {ex.Message}"); }

            return output.ToString();
        }
        #endregion

        #region RESTful Return
        //public static DataTable Get(string connectionString,
        //                      string query)
        //{
        //    var output = new DataTable();

        //    try
        //    {
        //        using (var dataAdapter = new SqlDataAdapter(query, connectionString))
        //        {
        //            using (var dataTable = new DataTable())
        //            {
        //                dataAdapter.Fill(dataTable);
        //                output = dataTable;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw new Exception($"Error : RESTful Return Get {ex.Message}"); }

        //    return output;
        //}

        public static DataTable Get(string query)
        {
            var output = new DataTable();

            try
            {
                using(var dataAdapter = new SqlDataAdapter(query, ConnectionString()))
                {
                    using(var dataTable = new DataTable())
                    {
                        dataAdapter.Fill(dataTable);
                        output = dataTable;
                    }
                }
            }
            catch (Exception ex)
            { throw new Exception($"Error : RESTful Return Get {ex.Message}"); }

            return output;
        }

        public DataTable GetData(string sQuery, string Connectionstring)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = new SqlConnection(Connectionstring))
                    {
                        using (SqlCommand cmd = new SqlCommand(sQuery, con))
                        {
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            con.Open();
                            da.Fill(dt);
                            con.Close();
                        }
                    }

                    return dt;
                }
            }
            catch (Exception ex)
            { return null; }

            //return output;
        }

        public DataTable GetHanaData(string sQuery, string Connectionstring)
        {
            try
            {
                using (DataTable dt1 = new DataTable())
                {
                    using (HanaConnection con = new HanaConnection(Connectionstring))
                    {
                        using (HanaCommand cmd = new HanaCommand(sQuery, con))
                        {
                            HanaDataAdapter da = new HanaDataAdapter(cmd);
                            con.Open();
                            da.Fill(dt1);
                            con.Close();
                        }
                    }

                    return dt1;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null; 
            }

            //return output;
        }

        public DataTable GetHanaData1(string sQuery, string Connectionstring)
        {
            try
            {
                using (DataTable dt1 = new DataTable())
                {
                    using (HanaConnection con = new HanaConnection(Connectionstring))
                    {
                        var translator = new TranslatorTool();
                        var query = translator.TranslateQuery(sQuery, out int numOfStatements, out int numOfErrors);


                        using (HanaCommand cmd = new HanaCommand(query, con))
                        {
                            HanaDataAdapter da = new HanaDataAdapter(cmd);
                            con.Open();
                            da.Fill(dt1);
                            con.Close();
                        }
                    }

                    return dt1;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

            //return output;
        }


        public int Execute(string connectionString,
                        string query)
        {
            var output = -999;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        output = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            { throw new Exception($"Error : Execute {ex.Message}"); }

            return output;
        }

        public static int Execute(string query)
        {
            var output = -999;
            try
            {
                using (var connection = new SqlConnection(ConnectionString()))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        output = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            { throw new Exception($"Error : Execute {ex.Message}"); }

            return output;
        }

        public static int CreateMsSqlAttachmentDatabase(string fileName)
        {
            var output = -999;
            try
            {
                using (var connection = new SqlConnection(ConnectionString("master")))
                {
                    using (var command = new SqlCommand())
                    {
                        string database = SystemSettings.GetFileName(fileName);
                        command.Connection = connection;
                        connection.Open();
                        string query = $"IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = '{database}' OR name = '{database}'))) " +
                                        $"CREATE DATABASE {database} ON PRIMARY (NAME={database}, FILENAME='{fileName}')";
                        command.CommandText = query;
                        if (command.ExecuteNonQuery() < 0)
                        {
                            command.CommandText = $"EXEC sp_detach_db '{database}', 'true'";
                        }
                        output = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            { throw new Exception($"Error : Execute {ex.Message}"); }
            return output;
        }
        #endregion
    }
}
