using System.Security.Cryptography;
using System.Text;

namespace Day14
{
    public static class Utils
    {

        public static string CreateMd5MultiHash(string input, int times)
        {
            string hash = input;

            for (int i = 0; i < times; i++)
            {
                hash = CreateMd5(hash);
            }



            return hash;
        }

        public static string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte t in hashBytes)
                {
                    sb.Append(t.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static void Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }
    }
}