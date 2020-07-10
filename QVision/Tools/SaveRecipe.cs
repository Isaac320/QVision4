using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace QVision.Tools
{
    class SaveRecipe
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">名字</param>
        private void SerializableNow(string path, string name,Dictionary<string,object> Dict)
        {
            FileStream fileStream = new FileStream(path + name, FileMode.Create);
            BinaryFormatter binF = new BinaryFormatter();
            binF.Serialize(fileStream, Dict);
            fileStream.Close();

        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">名字</param>
        public static Dictionary<string, object>  DeSerializeNow(string path, string name)
        {
            FileStream fileStream = new FileStream(path + name, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter binF = new BinaryFormatter();
            Dictionary<string, object> Dict = binF.Deserialize(fileStream) as Dictionary<string, object>;
            fileStream.Close();
            return Dict;
        }
    }
}
