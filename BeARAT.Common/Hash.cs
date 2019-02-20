using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Common
{
    static class Hash
    {
        public static byte[] GetHashSha256(object seed)
        {
            byte[] bytes = ObjectToByteArray(seed);
            SHA256 algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(bytes);
            return hash;
        }

        public static string Hash2String(byte[] hash)
        {
            StringBuilder strBuilder = new StringBuilder();

            for(int i = 0; i < hash.Length; i++) {
                strBuilder.Append(hash[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }
}
