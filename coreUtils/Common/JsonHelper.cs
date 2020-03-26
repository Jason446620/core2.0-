using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace coreUtils
{
    /// <summary>
    /// json 帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 获取单条数据【页面读取数据】
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SingleRecordToJson(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + ToJson(dt.Rows[i][j].ToString()) + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + ToJson(dt.Rows[i][j].ToString()) + "\"");
                        }

                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }

                    //JsonString.Append("} "); break;
                }
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 下拉选项；多选下拉 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + ToJson(dt.Rows[i][j].ToString()) + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + ToJson(dt.Rows[i][j].ToString()) + "\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return JsonString.ToString();
            }
        }
        /// <summary>
        /// 泛型T转json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ModelToJson<T>(T model)
        {
            StringBuilder JsonString = new StringBuilder();
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type type = typeof(T);
            //把所有的public属性加入到集合,考虑到Model中没有私有成员。
            //PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);  //获取所有属性
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); });
            int i = 0;
            JsonString.Append("{ ");
            foreach (PropertyInfo p in pList)
            {
                if (i.Equals(pList.Count - 1))
                {
                    if (p.GetValue(model, null) != null)
                    {
                        JsonString.Append("\"" + p.Name + "\":" + "\"" + ToJson(p.GetValue(model, null).ToString()) + "\"");
                    }
                    else
                    {

                        JsonString.Append("\"" + p.Name + "\":" + "\"" + p.GetValue(model, null) + "\"");
                    }

                }
                else
                {
                    if (p.GetValue(model, null) != null)
                    {
                        JsonString.Append("\"" + p.Name + "\":" + "\"" + ToJson(p.GetValue(model, null).ToString()) + "\",");
                    }
                    else
                    {
                        JsonString.Append("\"" + p.Name + "\":" + "\"" + p.GetValue(model, null) + "\",");
                    }
                }
                i++;
            }
            JsonString.Append("}");
            return JsonString.ToString();
        }
        /// <summary>
        /// dt转json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToCustomJson(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + ToJson(dt.Rows[i][j].ToString()) + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + ToJson(dt.Rows[i][j].ToString()) + "\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
            }
            return JsonString.ToString();
        }
        /// <summary>
        /// 特殊字符转换Json数据
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToJson(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// list转Json字符串
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string ListToJson<T>(List<T> a)
        {
            StringBuilder JSONString = new StringBuilder();
            JSONString.Append("[");

            foreach (var item in a)
            {
                var _json = ModelToJson(item);
                JSONString.Append(_json);
                JSONString.Append(",");
            }
            JSONString.Remove(JSONString.Length - 1, 1);
            JSONString.Append("]");

            return JSONString.ToString();
        }


        /// <summary>
        /// json泛型操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class FromJson<T> where T : class
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strJson"></param>
            /// <returns></returns>
            public static T JsonToModel(string strJson)
            {
                if (!string.IsNullOrEmpty(strJson))
                    return JsonConvert.DeserializeObject<T>(strJson);
                return null;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strJson"></param>
            /// <returns></returns>
            public static List<T> JsonToList(string strJson)
            {
                if (!string.IsNullOrEmpty(strJson))
                    return JsonConvert.DeserializeObject<List<T>>(strJson);
                return null;
            }
        }
    }
}
