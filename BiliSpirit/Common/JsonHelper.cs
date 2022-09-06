using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Common
{
    public sealed class JsonHelper
    {
        private static readonly JsonHelper instance = new JsonHelper();
        private JsonHelper() { }
        public static JsonHelper Instance { get { return instance; } }

        /// <summary>
        /// 序列化对像到json文件
        /// </summary>
        /// <param name="value">要序列化的对像</param>
        /// <param name="path">文件路径</param>
        public void SerializeObjectToFile(object value, string path)
        {
            string jsonStr = JsonConvert.SerializeObject(value);
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(FormatJsonStr(jsonStr));
            }
        }

        /// <summary>
        /// 序列化对像到json字符串
        /// </summary>
        /// <param name="value">要序列化的对像</param>
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// 将json文件反序列化为指定的.NET类型。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public T DeserializeObjectFroFile<T>(string path)
        {
            string jsonStr = string.Empty;
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                jsonStr = sr.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// 将json字符串反序列化为指定的.NET类型。
        /// </summary>
        public T DeserializeObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// 格式化json字符串
        /// </summary>
        public string FormatJsonStr(string str)
        {
            // 格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 实例克隆
        /// </summary>
        /// <param name="oldInstance">原实例</param>
        /// <returns>
        /// 返回克隆实例
        /// </returns>
        public T InstanceClone<T>(T oldInstance)
        {
            string jsonStr = JsonConvert.SerializeObject(oldInstance);
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }


        /// <summary>
        /// 从json中获取对应key的value值
        /// </summary>
        /// <param name="json字符串"></param>
        /// <param name="需要取value对应的key"></param>
        /// <returns></returns>
        public static string GetJsonValue(string strJson, string key)
        {
            string strResult = "";
            JObject jsonObj = JObject.Parse(strJson);
            strResult = GetNestJsonValue(jsonObj.Children(), key);
            return strResult;
        }

        /// <summary>
        /// 迭代获取eky对应的值
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetNestJsonValue(JEnumerable<JToken> jToken, string key)
        {
            IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken)enumerator.Current;
                if (jc is JObject || ((JProperty)jc).Value is JObject)
                {
                    return GetNestJsonValue(jc.Children(), key);
                }
                else
                {
                    if (((JProperty)jc).Name == key)
                    {
                        return ((JProperty)jc).Value.ToString();
                    }
                }
            }
            return null;
        }
    }
}
