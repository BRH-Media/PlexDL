using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PlexDL.Common.Extensions
{
    public static class DeepCloneExt
    {
        /// <summary>
        /// Clone this object's subobjects and their properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}