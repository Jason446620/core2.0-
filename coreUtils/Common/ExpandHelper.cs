using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace coreUtils
{
    /// <summary>
    /// Nall 扩展
    /// </summary>
    public static class ExpandHelper
    {

        #region 时间扩展
        /// <summary>
        /// 将当前System.DateTime对象的值转换成其等效的字符串格式(yyyy-MM-dd)表示。
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static string ToDateString(this DateTime time)
        {
            string dateStr = time.Year.ToString() + "-"
                + (time.Month < 10 ? ("0" + time.Month.ToString()) : time.Month.ToString()) + "-"
                + (time.Day < 10 ? ("0" + time.Day.ToString()) : time.Day.ToString());

            return dateStr;
        }

        /// <summary>
        /// 将当前System.DateTime对象的值转换成其等效的字符串格式(HH:mm:ss)表示。
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime time)
        {
            string timeStr = (time.Hour < 10 ? ("0" + time.Hour.ToString()) : time.Hour.ToString()) + ":"
               + (time.Minute < 10 ? ("0" + time.Minute.ToString()) : time.Minute.ToString()) + ":"
               + (time.Second < 10 ? ("0" + time.Second.ToString()) : time.Second.ToString());

            return timeStr;
        }

        /// <summary>
        /// 将当前System.DateTime对象的值转换成其等效的字符串格式(yyyy-MM-dd HH:mm:ss)表示。
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime time)
        {
            string timeStr = time.Year.ToString() + "-"
                + (time.Month < 10 ? ("0" + time.Month.ToString()) : time.Month.ToString()) + "-"
                + (time.Day < 10 ? ("0" + time.Day.ToString()) : time.Day.ToString()) + " "
                + (time.Hour < 10 ? ("0" + time.Hour.ToString()) : time.Hour.ToString()) + ":"
                + (time.Minute < 10 ? ("0" + time.Minute.ToString()) : time.Minute.ToString()) + ":"
                + (time.Second < 10 ? ("0" + time.Second.ToString()) : time.Second.ToString());

            return timeStr;
        }

        /// <summary>
        /// 将当前System.DateTime对象的值转换成其等效的字符串格式(yyyyMMddHHmmss)表示。
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static string ToLongDateTimeString(this DateTime time)
        {
            string timeStr = time.Year.ToString()
                + (time.Month < 10 ? ("0" + time.Month.ToString()) : time.Month.ToString())
                + (time.Day < 10 ? ("0" + time.Day.ToString()) : time.Day.ToString())
                + (time.Hour < 10 ? ("0" + time.Hour.ToString()) : time.Hour.ToString())
                + (time.Minute < 10 ? ("0" + time.Minute.ToString()) : time.Minute.ToString())
                + (time.Second < 10 ? ("0" + time.Second.ToString()) : time.Second.ToString());

            return timeStr;
        }

        /// <summary>
        /// 获取此实例距当天的年数。
        /// </summary>
        /// <param name="time">指定时间(如出生日期等)</param>
        /// <returns></returns>
        public static int GetYearsToToday(this DateTime time)
        {
            int years = DateTime.Today.Year - time.Year;

            return years;
        }

        /// <summary>
        /// 返回指定日期的最后一秒。
        /// </summary>
        /// <param name="date">指定日期(如'2017-06-28')</param>
        /// <returns>返回指定日期最后一秒(如'2017-06-28 23:59:59')</returns>
        public static DateTime ToLastSecondOfDate(this DateTime date)
        {
            return date.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取指定日期对应的星期几。
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <param name="resultType">返回结果方式，1-星期几，其余返回对应第几天(数值形式的字符星期天对应0)</param>
        /// <returns></returns>
        public static string GetDayOfWeek(this DateTime date, int resultType = 1)
        {
            string weekName = string.Empty;
            string indexOfWeek = string.Empty;

            string day = date.DayOfWeek.ToString();
            switch (day)
            {
                case "Monday":
                    weekName = "星期一"; indexOfWeek = "1"; break;
                case "Tuesday":
                    weekName = "星期二"; indexOfWeek = "2"; break;
                case "Wednesday":
                    weekName = "星期三"; indexOfWeek = "3"; break;
                case "Thursday":
                    weekName = "星期四"; indexOfWeek = "4"; break;
                case "Friday":
                    weekName = "星期五"; indexOfWeek = "5"; break;
                case "Saturday":
                    weekName = "星期六"; indexOfWeek = "6"; break;
                case "Sunday":
                    weekName = "星期日"; indexOfWeek = "0"; break;
                default:
                    weekName = string.Empty; indexOfWeek = "-1"; break;
            };

            if (resultType == 1)
            {
                return weekName;
            }
            else
            {
                return indexOfWeek;
            }
        }
        #endregion

        #region 集合扩展

        /// <summary>
        /// 获取该List的第一个实例。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T FirstOrDefault_DIY<T>(this List<T> list)
        {
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 判断实例是否为空。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T model)
        {
            if (model == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断实例是否为空。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T model)
        {
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断List是否包含实例。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断List是否包含实例。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<T>(this List<T> list)
        {
            if (list != null && list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将List中某属性联合成逗号隔开的字符串。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string ToUnionProperty<T>(this List<T> list, string propertyName)
        {
            string unionStr = string.Empty;

            if (list != null && list.Count > 0)
            {
                for (int i = 0, j = list.Count; i < j; i++)
                {
                    T model = list[i];

                    PropertyInfo[] propertys = model.GetType().GetProperties();
                    // 属性与字段名称一致的进行赋值    
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (pi.Name.ToLower().Equals(propertyName.ToLower()))
                        {
                            unionStr += "'" + pi.GetValue(model, null) + "',";
                        }
                    }
                }

                unionStr = unionStr.TrimEnd(',');
            }

            return unionStr;
        }

        /// <summary>
        /// 判断DataTable是否包含实例。
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  判断DataTable是否包含实例。
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将DataTable中某字段联合成逗号隔开的字符串。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string ToUnionColumn(this DataTable dt, string columnName)
        {
            string unionStr = string.Empty;

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = null;
                for (int i = 0, j = dt.Rows.Count; i < j; i++)
                {
                    dr = dt.Rows[i];

                    // 属性与字段名称一致的进行赋值
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName.ToLower().Equals(columnName.ToLower()))
                        {
                            unionStr += "'" + dr[columnName] + "',";
                        }
                    }
                }

                unionStr = unionStr.TrimEnd(',');
            }

            return unionStr;
        }
        #endregion

        #region 字符扩展
        /// <summary>
        /// 指示指定的字符串是null或System.String.Empty字符串。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 指示指定的字符串不是null或System.String.Empty字符串。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 将对象转换为Int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            int returnInt = 0;
            if (obj == null)
            {
                return returnInt;
            }

            try
            {
                returnInt = int.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                string into = ex.Message + ex.StackTrace;

                returnInt = 0;
            }

            return returnInt;
        }

        /// <summary>
        /// 将对象转换为Guid类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid ToGuid(this object obj)
        {
            Guid returnGuid = Guid.Empty;
            if (obj == null)
            {
                return returnGuid;
            }

            try
            {
                returnGuid = new Guid(obj.ToString());
            }
            catch (Exception ex)
            {
                string into = ex.Message + ex.StackTrace;

                returnGuid = Guid.Empty;
            }

            return returnGuid;
        }

        /// <summary>
        /// 将对象转换为指定时间类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dateFormate"></param>
        /// <returns></returns>
        public static string ToFormateString(this object obj, string dateFormate)
        {
            string returnDtString = string.Empty;

            if (obj == null)
            {
                return returnDtString;
            }

            try
            {
                DateTime time = DateTime.Parse(obj.ToString());
                if (dateFormate == "yyyy-MM-dd")
                {
                    returnDtString = time.ToDateString();
                }
                else if (dateFormate == "HH:mm:ss")
                {
                    returnDtString = time.ToTimeString();
                }
                else if (dateFormate == "yyyy-MM-dd HH:mm:ss")
                {
                    returnDtString = time.ToDateTimeString();
                }
                else if (dateFormate == "yyyyMMddHHmmss")
                {
                    returnDtString = time.ToLongDateTimeString();
                }
                else if (dateFormate == "yyyy-MM")
                {
                    returnDtString = time.ToString("yyyy-MM");
                }
                else
                {
                    return returnDtString;
                }
            }
            catch (Exception ex)
            {
                string into = ex.Message + ex.StackTrace;

                returnDtString = string.Empty;
            }

            return returnDtString;
        }

        /// <summary>
        /// 指定字符串长度，超过则省略...
        /// </summary>
        /// <param name="obj">字符串实体</param>
        /// <param name="keptLength">指定长度</param>
        /// <returns></returns>
        public static string AppointLength(this string obj, int keptLength)
        {
            if (obj != null && obj.Length > keptLength)
            {
                obj = obj.Substring(0, keptLength) + "...";
            }

            return obj;
        }

        /// <summary>
        /// 移除字符串中指定字符集
        /// </summary>
        /// <param name="obj">字符串实体</param>
        /// <param name="parameters">待移除的字符集</param>
        /// <returns></returns>
        public static string RemoveChar(this string obj, string[] parameters)
        {
            if (obj != null && obj.Length > 0 && parameters != null && parameters.Length > 0)
            {
                foreach (string removeChar in parameters)
                {
                    if (removeChar != string.Empty)
                    {
                        obj = obj.Replace(removeChar, string.Empty);
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 单个json实体中插入指定键值
        /// </summary>
        /// <param name="obj">json实体</param>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string InsertJson(this string obj, string name, object value)
        {
            if (obj == null || obj.Length == 0)
            {
                obj = "{}";

                return obj.Insert(obj.Length - 1, ("\"" + name + "\":\"" + value + "\""));
            }
            else
            {
                return obj.Insert(obj.Length - 1, (",\"" + name + "\":\"" + value + "\""));
            }
        }

        #endregion

        #region 数值扩展

        /// <summary>
        /// 指定数值保留固定位数小数。
        /// </summary>
        /// <param name="num">指定数值</param>
        /// <param name="length">保留指定小数位数</param>
        /// <param name="type">小于0舍去多余小数值，等于0四舍五入，大于0舍去多余小数值并向前一位进一</param>
        /// <returns></returns>
        public static double GetFix(this double num, int length = 2, int type = 0)
        {
            if (length <= 0)
            {
                return num;
            }

            double tempTransNum = Math.Pow(10, length);

            num = num * tempTransNum;

            if (type < 0)//向下取整(舍)
            {
                num = Math.Floor(num);
            }
            else if (type > 0)//向上取整(入)
            {
                num = Math.Ceiling(num);
            }
            else//四舍五入
            {
                num = Math.Round(num);
            }

            num = num / tempTransNum;

            return num;
        }
        #endregion
    }
}
