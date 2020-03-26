using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace coreUtils
{
    class SQLServerHelper
    {
        /// <summary>
        /// 连接字符串字段
        /// </summary>
        private static string _connString;

        /// <summary>
        /// 数据库连接字符串
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// </summary>
        public static string connString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        /// <summary>
        /// 执行命令返回DataSet数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string query)
        {
            try
            {
                using (DataSet tempdt = new DataSet())
                {
                    using (SqlConnection conn = new SqlConnection(_connString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            if (query.ToLower().Trim().StartsWith("select"))
                            {
                                comm.CommandType = CommandType.Text;
                            }
                            else
                            {
                                comm.CommandType = CommandType.StoredProcedure;
                            }

                            comm.CommandText = query;
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回DataSet数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="cmdType">执行类型</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string query, CommandType cmdType)
        {
            try
            {
                using (DataSet tempdt = new DataSet())
                {
                    using (SqlConnection conn = new SqlConnection(_connString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            comm.CommandType = cmdType;

                            comm.CommandText = query;
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回DataSet数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (DataSet tempdt = new DataSet())
                {
                    using (SqlConnection conn = new SqlConnection(_connString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            if (query.ToLower().Trim().StartsWith("select"))
                            {
                                comm.CommandType = CommandType.Text;
                            }
                            else
                            {
                                comm.CommandType = CommandType.StoredProcedure;
                            }

                            comm.CommandText = query;
                            for (int i = 0; i <= parameters.Length - 1; i++)
                            {
                                comm.Parameters.Add(parameters[i]);
                            }
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回DataSet数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string query, CommandType cmdType, params SqlParameter[] parameters)
        {
            try
            {
                using (DataSet tempdt = new DataSet())
                {
                    using (SqlConnection conn = new SqlConnection(_connString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            comm.CommandType = cmdType;
                            comm.CommandText = query;
                            for (int i = 0; i <= parameters.Length - 1; i++)
                            {
                                comm.Parameters.Add(parameters[i]);
                            }
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回DataSet数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="connectString">数据库连接字符串</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string query, string connectString)
        {
            try
            {
                using (DataSet tempdt = new DataSet())
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            if (query.ToLower().Trim().StartsWith("select"))
                            {
                                comm.CommandType = CommandType.Text;
                            }
                            else
                            {
                                comm.CommandType = CommandType.StoredProcedure;
                            }

                            comm.CommandText = query;
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回DataSet数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="connectString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string query, string connectString, CommandType cmdType)
        {
            try
            {
                using (DataSet tempdt = new DataSet())
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            comm.CommandType = cmdType;
                            comm.CommandText = query;
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回DataSet数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="connectString">数据库连接字符串</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string query, string connectString, params SqlParameter[] parameters)
        {
            try
            {
                using (DataSet tempdt = new DataSet())
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            if (query.ToLower().Trim().StartsWith("select"))
                            {
                                comm.CommandType = CommandType.Text;
                            }
                            else
                            {
                                comm.CommandType = CommandType.StoredProcedure;
                            }

                            comm.CommandText = query;
                            for (int i = 0; i <= parameters.Length - 1; i++)
                            {
                                comm.Parameters.Add(parameters[i]);
                            }
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行命令返回DataTable数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <returns>返回DataTable数据集</returns>
        public static DataTable ExecuteDataTable(string query)
        {
            try
            {
                using (DataTable tempdt = new DataTable())
                {
                    using (SqlConnection conn = new SqlConnection(_connString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            if (query.ToLower().Trim().StartsWith("select"))
                            {
                                comm.CommandType = CommandType.Text;
                            }
                            else
                            {
                                comm.CommandType = CommandType.StoredProcedure;
                            }
                            comm.CommandText = query;

                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行命令返回DataTable数据集
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>返回DataTable数据集</returns>
        public static DataTable ExecuteDataTable(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (DataTable tempdt = new DataTable())
                {
                    using (SqlConnection conn = new SqlConnection(_connString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            if (query.ToLower().Trim().StartsWith("select"))
                            {
                                comm.CommandType = CommandType.Text;
                            }
                            else
                            {
                                comm.CommandType = CommandType.StoredProcedure;
                            }

                            comm.CommandText = query;
                            for (int i = 0; i <= parameters.Length - 1; i++)
                            {
                                comm.Parameters.Add(parameters[i]);
                            }
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回DataTable数据集(设置超时时间)
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="TimeOut">执行文本：SQL语句/存储过程</param>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>返回DataTable数据集</returns>
        public static DataTable ExecuteDataTable(int TimeOut, string query, params SqlParameter[] parameters)
        {
            try
            {
                using (DataTable tempdt = new DataTable())
                {
                    using (SqlConnection conn = new SqlConnection(_connString))
                    {
                        using (SqlCommand comm = conn.CreateCommand())
                        {
                            comm.CommandTimeout = TimeOut;
                            if (query.ToLower().Trim().StartsWith("select"))
                            {
                                comm.CommandType = CommandType.Text;
                            }
                            else
                            {
                                comm.CommandType = CommandType.StoredProcedure;
                            }

                            comm.CommandText = query;
                            for (int i = 0; i <= parameters.Length - 1; i++)
                            {
                                comm.Parameters.Add(parameters[i]);
                            }
                            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
                            {
                                sda.Fill(tempdt);
                            }
                        }
                    }
                    return tempdt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回受影响的行数
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <returns>返回执行受影响的行数</returns>
        public static int ExecuteNonQuery(string query)
        {
            try
            {
                int rtn = 0;
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    conn.Open();
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        if (query.ToLower().Trim().StartsWith("insert")
                            || query.ToLower().Trim().StartsWith("update") || query.ToLower().Trim().StartsWith("delete"))
                        {
                            comm.CommandType = CommandType.Text;
                        }
                        else
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                        }

                        comm.CommandText = query;
                        rtn = comm.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return rtn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回受影响的行数
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="connectString">数据库连接字符串</param>
        /// <returns>返回执行受影响的行数</returns>
        public static int ExecuteNonQuery(string query, string connectString)
        {
            try
            {
                int rtn = 0;
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        if (query.ToLower().Trim().StartsWith("insert")
                            || query.ToLower().Trim().StartsWith("update") || query.ToLower().Trim().StartsWith("delete"))
                        {
                            comm.CommandType = CommandType.Text;
                        }
                        else
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                        }

                        comm.CommandText = query;
                        rtn = comm.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return rtn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回受影响的行数
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>返回执行受影响的行数</returns>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            try
            {
                int rtn = 0;
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    conn.Open();
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        if (query.ToLower().Trim().StartsWith("insert")
                            || query.ToLower().Trim().StartsWith("update") || query.ToLower().Trim().StartsWith("delete"))
                        {
                            comm.CommandType = CommandType.Text;
                        }
                        else
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                        }

                        comm.CommandText = query;
                        for (int i = 0; i <= parameters.Length - 1; i++)
                        {
                            comm.Parameters.Add(parameters[i]);
                        }
                        rtn = comm.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return rtn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回受影响的行数
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>返回执行受影响的行数</returns>
        public static int ExecuteNonQuery(string query, CommandType cmdType, params SqlParameter[] parameters)
        {
            try
            {
                int rtn = 0;
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    conn.Open();
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandType = cmdType;

                        comm.CommandText = query;

                        for (int i = 0; i <= parameters.Length - 1; i++)
                        {
                            comm.Parameters.Add(parameters[i]);
                        }
                        rtn = comm.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return rtn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行命令返回执行的单一值
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="query">执行文本：SQL语句/存储过程</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回执行的单一值</returns>
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (query.ToLower().Trim().StartsWith("select"))
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
                conn.Open();
                object retval = cmd.ExecuteScalar();
                conn.Close();

                return retval;
            }
        }

        /// <summary>
        /// 执行事务返回执行成功数
        /// </summary>
        /// <remarks>
        /// 创建人：Null
        /// </remarks>
        /// <param name="list">执行事务</param>
        /// <returns>返回执行成功数</returns>
        public static int ExecuteSqlTran(List<string> list)
        {
            int num = 0;
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    try
                    {
                        //循环
                        for (int i = 0; i < list.Count; i++)
                        {
                            cmd.CommandText = list[i];
                            cmd.ExecuteNonQuery();
                            num++;
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        num = 0;
                        trans.Rollback();
                        throw ex;
                    }
                }
                conn.Close();
                return num;
            }
        }

    }
}

