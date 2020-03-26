using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace coreUtils
{
    /// <summary>
    /// 公共方法库
    /// 常见的操作，转换
    /// </summary>
    public  class Nall
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objField"></param>
        /// <param name="objDBNull"></param>
        /// <returns></returns>
        public static object GetNull(object objField, object objDBNull)
        {
            object objectValue = RuntimeHelpers.GetObjectValue(objField);
            if (objField == null)
            {
                return RuntimeHelpers.GetObjectValue(objDBNull);
            }
            if (objField is int)
            {
                if (Convert.ToInt32(RuntimeHelpers.GetObjectValue(objField)) == NullInteger)
                {
                    objectValue = RuntimeHelpers.GetObjectValue(objDBNull);
                }
                return objectValue;
            }
            if (objField is float)
            {
                if (Convert.ToSingle(RuntimeHelpers.GetObjectValue(objField)) == NullInteger)
                {
                    objectValue = RuntimeHelpers.GetObjectValue(objDBNull);
                }
                return objectValue;
            }
            if (objField is double)
            {
                if (Convert.ToDouble(RuntimeHelpers.GetObjectValue(objField)) == NullInteger)
                {
                    objectValue = RuntimeHelpers.GetObjectValue(objDBNull);
                }
                return objectValue;
            }
            if (objField is decimal)
            {
                if (decimal.Compare(Convert.ToDecimal(RuntimeHelpers.GetObjectValue(objField)), new decimal(NullInteger)) == 0)
                {
                    objectValue = RuntimeHelpers.GetObjectValue(objDBNull);
                }
                return objectValue;
            }
            if (objField is DateTime)
            {
                if (DateTime.Compare(Convert.ToDateTime(RuntimeHelpers.GetObjectValue(objField)), NullDate) == 0)
                {
                    objectValue = RuntimeHelpers.GetObjectValue(objDBNull);
                }
                return objectValue;
            }
            if (objField is string)
            {
                if (objField == null)
                {
                    return RuntimeHelpers.GetObjectValue(objDBNull);
                }
                if (objField.ToString() == NullString)
                {
                    objectValue = RuntimeHelpers.GetObjectValue(objDBNull);
                }
                return objectValue;
            }
            if (objField is bool)
            {
                if (Convert.ToBoolean(RuntimeHelpers.GetObjectValue(objField)) == NullBoolean)
                {
                    objectValue = RuntimeHelpers.GetObjectValue(objDBNull);
                }
                return objectValue;
            }
            if (!(objField is Guid))
            {
                throw new NullReferenceException();
            }
            Guid guid2 = (Guid)objField;
            if (guid2.Equals(NullGuid))
            {
                objectValue = RuntimeHelpers.GetObjectValue(objDBNull);
            }
            return objectValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objField"></param>
        /// <returns></returns>
        public static bool IsNull(object objField)
        {
            return ((objField == null) || objField.Equals(RuntimeHelpers.GetObjectValue(SetNull(RuntimeHelpers.GetObjectValue(objField)))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objField"></param>
        /// <returns></returns>
        public static object SetNull(object objField)
        {
            if (objField != null)
            {
                if (objField is int)
                {
                    return NullInteger;
                }
                if (objField is float)
                {
                    return NullInteger;
                }
                if (objField is double)
                {
                    return -1.0;
                }
                if (objField is decimal)
                {
                    return NullInteger;
                }
                if (objField is DateTime)
                {
                    return NullDate;
                }
                if (objField is string)
                {
                    return NullString;
                }
                if (objField is bool)
                {
                    return NullBoolean;
                }
                if (!(objField is Guid))
                {
                    throw new NullReferenceException();
                }
                return NullGuid;
            }
            return NullString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPropertyInfo"></param>
        /// <returns></returns>
        public static object SetNull(PropertyInfo objPropertyInfo)
        {
            switch (objPropertyInfo.PropertyType.ToString())
            {
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    return NullInteger;

                case "System.Byte":
                    return Convert.ToByte(0);

                case "System.DateTime":
                    return NullDate;

                case "System.String":
                case "System.Char":
                    return NullString;

                case "System.Boolean":
                    return NullBoolean;
                case "System.Object":
                    return NullObject;
                case "System.Guid":
                    return NullGuid;
                default:
                    return null;
            }

        }

        #region 数据转换
        /// <summary>
        /// int 转bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(int obj)
        {
            return (obj > 0);
        }
        /// <summary>
        /// obj转bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return false;
            }
            return Convert.ToBoolean(RuntimeHelpers.GetObjectValue(obj));
        }

        /// <summary>
        /// 转long
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToLong(object obj)
        {
            long num;
            try
            {
                if (obj == null)
                {
                    return -1;
                }
                num = Convert.ToInt64(obj.ToString());
            }
            catch (Exception exception1)
            {

                Exception exception = exception1;
                num = -1;

            }
            return num;
        }
        /// <summary>
        /// string 转 bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(string obj)
        {
            if ((obj == "0") | (obj.ToLower() == "false"))
            {
                return false;
            }
            return ((obj == "1") | (obj.ToLower() == "true"));
        }
        /// <summary>
        /// 转 double类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(string obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj))
                {
                    return 0;
                }
                else
                {
                    return double.Parse(obj);
                }

            }
            catch
            {
                return 0;
            }

        }
        /// <summary>
        /// 转DateTime类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string obj)
        {
            DateTime nullDate;
            try
            {
                if (obj.Trim() == "")
                {
                    return new DateTime(0x76c, 1, 1);
                    //return DateTime.MinValue;
                }
                if (obj == null)
                {
                    return new DateTime(0x76c, 1, 1);
                    //return DateTime.MinValue;
                }
                if (obj.Trim().Length <= 0)
                {
                    return new DateTime(0x76c, 1, 1);
                    //return DateTime.MinValue;
                }
                DateTime time2 = new DateTime(0x76c, 1, 1);
                if (DateTime.Compare(Convert.ToDateTime(obj), time2) < 0)
                {
                    return new DateTime(0x76c, 1, 1);
                    //return DateTime.MinValue;
                }
                nullDate = Convert.ToDateTime(obj).Year == 1900 ? new DateTime(0x76c, 1, 1) : Convert.ToDateTime(obj);
                //nullDate = Convert.ToDateTime(obj).Year == 1900 ? DateTime.MinValue : Convert.ToDateTime(obj);
            }
            catch (Exception exception1)
            {

                Exception exception = exception1;
                nullDate = NullDate;

            }
            return nullDate;
        }
        /// <summary>
        /// 转Guid类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid ToGuid(object obj)
        {
            Guid empty;
            if (obj == null)
            {
                return Guid.Empty;
            }
            try
            {
                Guid guid2 = new Guid(obj.ToString());
                empty = guid2;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                empty = Guid.Empty;

            }
            return empty;
        }
        /// <summary>
        /// 转Int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(bool obj)
        {
            if (obj)
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// 转Int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            int num;
            try
            {
                if (obj == null)
                {
                    return -1;
                }
                num = Nall.ToInt(obj.ToString());
            }
            catch (Exception exception1)
            {

                Exception exception = exception1;
                num = -1;

            }
            return num;
        }
        /// <summary>
        /// 转Int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(string obj)
        {
            int num;
            if (obj == null)
            {
                return -1;
            }
            if (obj.Trim().Length <= 0)
            {
                return -1;
            }
            try
            {
                num = Convert.ToInt32(obj);
            }
            catch (Exception exception1)
            {

                Exception exception = exception1;
                num = -1;

            }
            return num;
        }
        /// <summary>
        /// 转string类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStrings(object obj)
        {
            if (obj == null)
            {
                return "";
            }

            return Convert.ToString(RuntimeHelpers.GetObjectValue(obj));
        }
        /// <summary>
        /// 转string类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (obj == DBNull.Value)
                return "";
            return Convert.ToString(RuntimeHelpers.GetObjectValue(obj));
        }
        /// <summary>
        /// 返回bool类型默认数据
        /// </summary>
        public static bool NullBoolean
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 返回空DateTime默认数据
        /// </summary>
        public static DateTime NullDate
        {
            get
            {
                return new DateTime(0x76c, 1, 1);
            }
        }
        /// <summary>
        /// 返回Guid 默认空值
        /// </summary>
        public static Guid NullGuid
        {
            get
            {
                return Guid.Empty;
            }
        }
        /// <summary>
        /// 返回-1
        /// </summary>
        public static int NullInteger
        {
            get
            {
                return -1;
            }
        }
        /// <summary>
        /// 返回空对象
        /// </summary>
        public static object NullObject
        {
            get
            {
                return "";
            }
        }
        /// <summary>
        /// 返回空字符串
        /// </summary>
        public static string NullString
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 新增 ip地址转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IPAddress toIP(string str)
        {
            IPAddress safe = IPAddress.Parse("0.0.0.0");
            IPAddress.TryParse(str, out safe);
            return safe;
        }
        #endregion

        #region 常见的dt，list操作
        /// <summary>
        /// 数据集DataTable转换成List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dtTemp"></param>
        /// <returns></returns>
        public static List<T> ToListByDataTable<T>(DataTable dtTemp)
        {
            if (dtTemp == null || dtTemp.Rows.Count == 0)
            {
                return null;
            }
            List<T> lstResult = new List<T>();

            for (int j = 0, l = dtTemp.Rows.Count; j < l; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0, k = dtTemp.Columns.Count; i < k; i++)
                    {
                        // 属性与字段名称一致的进行赋值    
                        if (pi.Name.ToLower().Equals(dtTemp.Columns[i].ColumnName.ToLower()))
                        {
                            if (dtTemp.Rows[j][i] != DBNull.Value)
                            {
                                switch (pi.PropertyType.ToString())
                                {
                                    case "System.Int32":
                                        pi.SetValue(_t, Nall.ToInt(dtTemp.Rows[j][i].ToString()), null);
                                        break;
                                    case "System.Int64":
                                        pi.SetValue(_t, Nall.ToLong(dtTemp.Rows[j][i].ToString()), null);
                                        break;
                                    case "System.DateTime":
                                        pi.SetValue(_t, Nall.ToDateTime(dtTemp.Rows[j][i].ToString()), null);
                                        break;
                                    case "System.String":
                                        pi.SetValue(_t, dtTemp.Rows[j][i].ToString(), null);
                                        break;
                                    case "System.Boolean":
                                        pi.SetValue(_t, Nall.ToBoolean(dtTemp.Rows[j][i].ToString()), null);
                                        break;
                                    case "System.Guid":
                                        pi.SetValue(_t, Nall.ToGuid(dtTemp.Rows[j][i].ToString()), null);
                                        break;
                                    case "System.Single":
                                        pi.SetValue(_t, Convert.ToSingle(dtTemp.Rows[j][i].ToString()), null);
                                        break;
                                    case "System.Double":
                                        pi.SetValue(_t, Convert.ToDouble(dtTemp.Rows[j][i].ToString()), null);
                                        break;
                                    case "System.Object":
                                        pi.SetValue(_t, dtTemp.Rows[j][i], null);
                                        break;
                                }
                            }
                            else
                            {
                                switch (pi.PropertyType.ToString())
                                {
                                    case "System.Int32":
                                        pi.SetValue(_t, -1, null);
                                        break;
                                    case "System.Int64":
                                        pi.SetValue(_t, -1, null);
                                        break;
                                    case "System.DateTime":
                                        pi.SetValue(_t, new DateTime(0x76c, 1, 1), null);
                                        break;
                                    case "System.Boolean":
                                        pi.SetValue(_t, false, null);
                                        break;
                                    case "System.Guid":
                                        pi.SetValue(_t, Guid.Empty, null);
                                        break;
                                    case "System.Single":
                                        pi.SetValue(_t, 0.0f, null);
                                        break;
                                    case "System.Double":
                                        pi.SetValue(_t, 0.0, null);
                                        break;
                                    case "System.String":
                                        pi.SetValue(_t, string.Empty, null);
                                        break;
                                    default:
                                        pi.SetValue(_t, null, null);
                                        break;
                                }
                            }
                            break;
                        }
                    }
                }

                lstResult.Add(_t);
            }

            return lstResult;
        }
        /// <summary>
        /// List集合转换成数据集DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTableByList<T>(List<T> list)
        {

            //创建属性的集合    
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口    

            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                pList.Add(p);
                dt.Columns.Add(p.Name, p.PropertyType);
            });
            //仅用于特殊处理 照管评价算分逻辑 Common类库引用
            //Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); 
            //    dt.Columns.Add(p.Name, !p.Name.Equals("Score") ? p.PropertyType : typeof(System.Decimal)); });
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    //创建一个DataRow实例    
                    DataRow row = dt.NewRow();
                    //给row 赋值    
                    pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                    //加入到DataTable    
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        /// <summary>
        /// DataRow转HashTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Hashtable ToHashtableByDataRow(DataTable dt, int key)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ht.Add(dt.Columns[i].ColumnName, dt.Rows[key][i]);
            }
            return ht;
        }
        /// <summary>
        ///  转换哈希表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Hashtable> ToHashtableByDataTable<T>(DataTable dt)
        {
            List<Hashtable> listht = new List<Hashtable>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Hashtable ht = new Hashtable();
                ht.Add(i, dt.Rows[i]);
                listht.Add(ht);
            }
            return listht;
        }

        /// <summary>
        /// dt 转hashtable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Hashtable ToHashtableByDataTableColumnName(DataTable dt)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ht.Add(dt.Columns[i].ColumnName, i);
            }
            return ht;
        }
        /// <summary> 
        /// 将两个列不同(结构不同)的DataTable合并成一个新的DataTable，第一个放行数多的datatable 
        /// </summary> 
        /// 创建人：潘书北 at 2017-02-16
        /// <param name="DataTable1">表1(行数多的放前面)</param> 
        /// <param name="DataTable2">表2</param> 
        /// <param name="DTName">合并后新的表名</param> 
        /// <returns>合并后的新表</returns> 
        public static DataTable UniteDataTable(DataTable DataTable1, DataTable DataTable2, string DTName)
        {
            DataTable newDataTable = new DataTable();
            if (DataTable1.Rows.Count > DataTable2.Rows.Count)
            {
                newDataTable = FillData(DataTable1, DataTable2);
            }
            else
            {
                newDataTable = FillData(DataTable2, DataTable1);
            }

            newDataTable.TableName = DTName; //设置DT的名字 
            return newDataTable;
        }
        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        private static DataTable FillData(DataTable dt1, DataTable dt2)
        {
            //克隆DataTable1的结构
            DataTable newDataTable = dt1.Clone();
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                //再向新表中加入DataTable2的列结构
                newDataTable.Columns.Add(dt2.Columns[i].ColumnName);
            }
            object[] obj = new object[newDataTable.Columns.Count];
            //添加DataTable1的数据
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    newDataTable.Rows[i][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
                }
            }
            return newDataTable;
        }
        #endregion

        #region 检测汉字
        /// <summary>
        /// 用 UNICODE 编码范围判断字符是不是汉字
        /// </summary>
        /// <param name="text">待判断字符或字符串</param>
        /// <returns>真：是汉字；假：不是</returns>
        public static bool CheckStringChineseUn(string text)
        {
            bool res = false;
            foreach (char t in text)
            {
                if (t >= 0x4e00 && t <= 0x9fbb)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
        #endregion

    }
}
