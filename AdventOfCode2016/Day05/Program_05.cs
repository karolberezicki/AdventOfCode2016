using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day05
{
    public class Program_05
    {
        public static void Main(string[] args)
        {
            string doorid = "cxdnnyjw";
            char placeholder = '_';
            MD5 md5 = MD5.Create();
            Console.CursorVisible = false;

            char[] password1 = new char[8];
            Utils.Populate(password1, placeholder);

            int iterator = 0;
            int countFoundChars = 0;

            Utils.WriteProgress(password1, iterator);

            while (password1.Any(c => c == placeholder))
            {
                string hash = Utils.CalculateMD5Hash(md5, doorid + iterator);
                if (hash.StartsWith("00000"))
                {
                    Utils.WriteProgress(password1, iterator);

                    password1[countFoundChars] = hash[5];
                    countFoundChars++;
                }
                iterator++;
            }


            char[] password2 = new char[8];
            Utils.Populate(password2, placeholder);

            iterator = 0;
            Utils.WriteProgress(password2, iterator);

            while (password2.Any(c => c == placeholder))
            {
                string hash = Utils.CalculateMD5Hash(md5, doorid + iterator);

                if (hash.StartsWith("00000"))
                {

                    if (int.TryParse(hash[5] + "", out int position))
                    {
                        if (position < password2.Length && password2[position] == placeholder)
                        {
                            password2[position] = hash[6];
                            Utils.WriteProgress(password2, iterator);

                        }
                    }
                }
                iterator++;

            }

            string partOnePassword = string.Join("", password1);
            string partTwoPassword = string.Join("", password2);

            Console.Clear();
            Console.WriteLine("Decrypted!!");
            Console.WriteLine("Part one password = {0}", partOnePassword);
            Console.WriteLine("Part two password = {0}", partTwoPassword);

            Console.ReadLine();

        }
    }

    public static class Utils
    {
        public static string CalculateMD5Hash(MD5 md5, string input)
        {
            // step 1, calculate MD5 hash from input
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static void Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }

        public static void WriteProgress(char[] password, int iteration)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Decrypting......");
            Console.WriteLine("Iteration: {0}", iteration.ToString("D8"));
            Console.WriteLine("Password: {0}", string.Join("", password));
        }
    }
}
