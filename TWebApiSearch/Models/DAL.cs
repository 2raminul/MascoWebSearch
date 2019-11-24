using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace TWebApiSearch.Models
{
    public class DAL
    {

        public static CallSite<Action<CallSite, SqlParameterCollection, string, object>> param;
        public static object ExecuteScalar(string sql, string DataBaseName)
        {
            dynamic obj2;

            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection)
                    {
                        CommandType = CommandType.Text
                    };
                    connection.Open();
                    obj2 = command.ExecuteScalar();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return obj2;
        }

        internal static DataTable GetDataTable(object getTaxYear, string searchET)
        {
            throw new NotImplementedException();
        }

        public static object ExecuteScalar(string sql, Dictionary<string, object> dictionary, string DataBaseName)
        {
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            dynamic obj2;
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection)
                    {
                        CommandType = CommandType.Text
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, command.Parameters, pair.Key, pair.Value);
                    }
                    connection.Open();
                    obj2 = command.ExecuteScalar();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return obj2;
        }

        public static int ExecuteSQL(string sql, string DataBaseName)
        {
            int num;
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    string str = "BEGIN TRY BEGIN TRANSACTION ";
                    string str2 = " COMMIT TRANSACTION END TRY BEGIN CATCH  ROLLBACK TRANSACTION END CATCH";
                    sql = str + sql + str2;
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    num = command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                num = -1;
            }
            return num;
        }

        public static int ExecuteSQL(string sql, Dictionary<string, object> dictionary, string DataBaseName)
        {
            int num;
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    string str = "BEGIN TRY BEGIN TRANSACTION ";
                    string str2 = " COMMIT TRANSACTION END TRY BEGIN CATCH  ROLLBACK TRANSACTION END CATCH";
                    sql = str + sql + str2;
                    SqlCommand command = new SqlCommand(sql, connection)
                    {
                        CommandType = CommandType.Text
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, command.Parameters, pair.Key, pair.Value);
                    }
                    connection.Open();
                    num = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return num;
        }

        public static Guid ExecuteSQLGUID(string sql, Dictionary<string, object> dictionary, string DataBaseName)
        {
            Guid guid;
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    string str = "BEGIN TRY BEGIN TRANSACTION ";
                    string str2 = " COMMIT TRANSACTION END TRY BEGIN CATCH  ROLLBACK TRANSACTION END CATCH";
                    sql = str + sql + str2;
                    SqlCommand command = new SqlCommand(sql, connection)
                    {
                        CommandType = CommandType.Text
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, command.Parameters, pair.Key, pair.Value);
                    }
                    guid = (Guid)command.ExecuteScalar();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return guid;
        }

        public static byte[] getbyteArray(string sql, Dictionary<string, object> dictionary, string DataBaseName)
        {
            byte[] buffer;

            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, command.Parameters, pair.Key, pair.Value);
                    }
                    connection.Open();
                    buffer = (byte[])command.ExecuteScalar();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return buffer;
        }

        public static DataSet GetDataSet(string sql, string DataBaseName)
        {
            DataSet set2;
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand selectCommand = new SqlCommand(sql, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    set2 = dataSet;
                }
            }
            catch (SqlException exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return set2;
        }

        public static DataSet GetDataSet(string sql, Dictionary<string, object> dictionary, string DataBaseName)
        {
            DataSet set2;
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand selectCommand = new SqlCommand(sql, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, selectCommand.Parameters, pair.Key, pair.Value);
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    set2 = dataSet;
                }
            }
            catch (SqlException exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return set2;
        }

        public static DataSet GetDataSetFromStoredProc(string sql, Dictionary<string, object> dictionary, string DataBaseName)
        {
            DataSet set2;
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand selectCommand = new SqlCommand(sql, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, selectCommand.Parameters, pair.Key, pair.Value);
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    set2 = dataSet;
                }
            }
            catch (SqlException exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return set2;
        }
        
        public static DataTable GetDataTable(string sql, string DataBaseName)
        {
            DataSet dataSet = GetDataSet(sql, DataBaseName);
            if (dataSet.Tables.Count > 0)
            {
                return dataSet.Tables[0];
            }
            return null;
        }

        public static DataTable GetDataTable(string sql, Dictionary<string, object> dictionary, string DataBaseName)
        {        
            DataSet set = GetDataSet(sql, dictionary, DataBaseName);
            if (set.Tables.Count > 0)
            {
                return set.Tables[0];
            }
            return null;
        }

        public static DataTable GetDataTableFromStoredProc(string sql, Dictionary<string, object> dictionary, string DataBaseName)
        {           
            DataSet set = GetDataSetFromStoredProc(sql, dictionary, DataBaseName);
            if (set.Tables.Count > 0)
            {
                return set.Tables[0];
            }
            return null;
        }

        public static int storedProc(string StoredProcedure, Dictionary<string, object> dictionary, string DataBaseName)
        {
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            int num;
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand command = new SqlCommand(StoredProcedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, command.Parameters, pair.Key, pair.Value);
                    }
                    connection.Open();
                    num = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return num;
        }

        public static DataTable storedProc2(string StoredProcedure, Dictionary<string, object> dictionary, string DataBaseName)
        {
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            DataTable table;
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand command = new SqlCommand(StoredProcedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, command.Parameters, pair.Key, pair.Value);
                    }
                    table = GetDataTableFromStoredProc(StoredProcedure, dictionary, DataBaseName);
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return table;
        }

        public static int SaveDataByStoreProcedure(string StoredProcedure, Dictionary<string, object> dictionary, string DataBaseName)
        {
            string _ConnectionString = DBConnection.GetConectionString(DataBaseName);
            int num;
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand command = new SqlCommand(StoredProcedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    foreach (KeyValuePair<string, object> pair in dictionary)
                    {
                        if (param == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            param = CallSite<Action<CallSite, SqlParameterCollection, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddWithValue", null, typeof(DAL), argumentInfo));
                        }
                        param.Target.Invoke(param, command.Parameters, pair.Key, pair.Value);
                    }
                    connection.Open();
                    num = (int) command.ExecuteScalar();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Data error. " + exception.Message.ToString());
            }
            return num;
        }

    }
}