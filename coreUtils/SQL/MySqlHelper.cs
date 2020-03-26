using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace coreUtils
{
    /// <summary>
    /// MySql帮助类
    /// </summary>
     public class MySqlHelper
    {


        /// <summary>
        /// 连接字符串字段
        /// </summary>
        private static string _mySqlConnString;

        /// <summary>
        /// 数据库连接字符串
        /// <remarks>
        /// </remarks>
        /// </summary>
        public static string MySqlconnString
        {
            get { return _mySqlConnString; }
            set { _mySqlConnString = value; }
        }

        #region 方法
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection conn = new MySqlConnection(_mySqlConnString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(_mySqlConnString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection connection = new MySqlConnection(_mySqlConnString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 返回ds
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string cmdText)
        {
            DataSet retSet = new DataSet();
            using (MySqlDataAdapter msda = new MySqlDataAdapter(cmdText, _mySqlConnString))
            {
                msda.Fill(retSet);
            }
            return retSet;
        }
        /// <summary>
        /// 返回dt
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string cmdText)
        {
            DataSet retSet = new DataSet();
            using (MySqlDataAdapter msda = new MySqlDataAdapter(cmdText, _mySqlConnString))
            {
                msda.Fill(retSet);
            }
            if (retSet != null && retSet.Tables.Count > 0)
            {
                return retSet.Tables[0];
            }
            else
            {
                return null;
            }
        }

        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
        }

        #endregion

        #region 参数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Size"></param>
        /// <param name="Direction"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static MySqlParameter CreateParam(string ParamName, MySqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            MySqlParameter param;


            if (Size > 0)
            {
                param = new MySqlParameter(ParamName, DbType, Size);
            }
            else
            {

                param = new MySqlParameter(ParamName, DbType);
            }


            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
            {
                param.Value = Value;
            }


            return param;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Size"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static MySqlParameter CreateInParam(string ParamName, MySqlDbType DbType, int Size, object Value)
        {
            return CreateParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static MySqlParameter CreateOutParam(string ParamName, MySqlDbType DbType, int Size)
        {
            return CreateParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static MySqlParameter CreateReturnParam(string ParamName, MySqlDbType DbType, int Size)
        {
            return CreateParam(ParamName, DbType, Size, ParameterDirection.ReturnValue, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrentIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="WhereSql"></param>
        /// <param name="TableName"></param>
        /// <param name="Columns"></param>
        /// <param name="Sort"></param>
        /// <returns></returns>
        public static MySqlParameter[] GetPageParm(int CurrentIndex, int PageSize, string WhereSql, string TableName, string Columns, Hashtable Sort)
        {
            MySqlParameter[] parm = {
                                        MySqlHelper.CreateInParam("@CurrentIndex",  MySqlDbType.Int32,      4,      CurrentIndex    ),
                                        MySqlHelper.CreateInParam("@PageSize",      MySqlDbType.Int32,      4,      PageSize        ),
                                        MySqlHelper.CreateInParam("@WhereSql",      MySqlDbType.VarChar,  2500,    WhereSql        ),
                                        MySqlHelper.CreateInParam("@TableName",     MySqlDbType.VarChar,  20,     TableName       ),
                                        MySqlHelper.CreateInParam("@Column",        MySqlDbType.VarChar,  2500,    Columns         ),
                                        MySqlHelper.CreateInParam("@Sort",          MySqlDbType.VarChar,  50,     GetSort(Sort)   ),
                                        MySqlHelper.CreateOutParam("@RecordCount",  MySqlDbType.Int32,      4                       )
                                   };
            return parm;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Columns"></param>
        /// <param name="WhereSql"></param>
        /// <returns></returns>
        public static MySqlParameter[] GetCountParm(string TableName, string Columns, string WhereSql)
        {
            MySqlParameter[] parm = {
                                   MySqlHelper.CreateInParam("@TableName",     MySqlDbType.VarChar,  20,     TableName       ),
                                   MySqlHelper.CreateInParam("@CountColumn",  MySqlDbType.VarChar,  20,     Columns         ),
                                   MySqlHelper.CreateInParam("@WhereSql",      MySqlDbType.VarChar,  250,    WhereSql        ),
                                   MySqlHelper.CreateOutParam("@RecordCount",  MySqlDbType.Int32,      4                       )
                                   };
            return parm;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        private static string GetSort(Hashtable sort)
        {
            string str = "";
            int i = 0;
            if (sort != null && sort.Count > 0)
            {
                foreach (DictionaryEntry de in sort)
                {
                    i++;
                    str += de.Key + " " + de.Value;
                    if (i != sort.Count)
                    {
                        str += ",";
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdTexts"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static bool ExecuteTransaction(CommandType cmdType, string[] cmdTexts, params MySqlParameter[][] commandParameters)
        {
            MySqlConnection myConnection = new MySqlConnection(_mySqlConnString);
            myConnection.Open();
            MySqlTransaction myTrans = myConnection.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = myConnection;
            cmd.Transaction = myTrans;

            try
            {
                for (int i = 0; i < cmdTexts.Length; i++)
                {
                    PrepareCommand(cmd, myConnection, null, cmdType, cmdTexts[i], commandParameters[i]);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                myTrans.Commit();
            }
            catch
            {
                myTrans.Rollback();
                return false;
            }
            finally
            {
                myConnection.Close();
            }
            return true;
        }

        #endregion

    }
}
