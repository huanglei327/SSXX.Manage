using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSXX.Manage.Common
{
    public class JsonHelper
    {
        /// 对象转为json  
        /// </summary>  
        /// <typeparam name="ObjType"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static string ObjToJsonString<ObjType>(ObjType obj) where ObjType : class
        {
            string s = JsonConvert.SerializeObject(obj);
            return s;
        }
        /// <summary>  
        /// json转为对象  
        /// </summary>  
        /// <typeparam name="ObjType"></typeparam>  
        /// <param name="JsonString"></param>  
        /// <returns></returns>  
        public static ObjType JsonStringToObj<ObjType>(string JsonString) where ObjType : class
        {
            ObjType s = JsonConvert.DeserializeObject<ObjType>(JsonString);
            return s;
        }


        // DataSet装换为泛型集合   
        public static List<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return null;
            if (p_TableIndex < 0)
                p_TableIndex = 0;

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化   
            List<T> result = new List<T>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值   
                        if (pi.Name.Equals(p_Data.Columns[i].ColumnName))
                        {
                            //  DataRowCollection dataRowCollection = new DataRowCollection();  
                            //dataRowCollection.Add(  
                            // p_Data.Columns[i].DataType = pi.GetType();   

                            // 数据库NULL值单独处理   
                            if (p_Data.Rows[j][i] != DBNull.Value)
                            {
                                try { pi.SetValue(_t, p_Data.Rows[j][i], null); }
                                catch { pi.SetValue(_t, int.Parse(p_Data.Rows[j][i].ToString()), null); }
                            }
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }


        // DataSet装换为泛型集合   
        public static List<T> DataSetToIList<T>(DataSet p_DataSet, string p_TableName)
        {
            int _TableIndex = 0;
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (string.IsNullOrEmpty(p_TableName))
                return null;
            for (int i = 0; i < p_DataSet.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值   
                if (p_DataSet.Tables[i].TableName.Equals(p_TableName))
                {
                    _TableIndex = i;
                    break;
                }
            }
            return DataSetToIList<T>(p_DataSet, _TableIndex);
        }
    }
}
