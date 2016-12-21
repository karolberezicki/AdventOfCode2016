using System.Linq;

namespace Day21
{
    public static class StringExtensions
    {

        public static string SwapPostition(this string str, int position, int withPosition)
        {
            return str.SwapLetter(str[position], str[withPosition]);
        }

        public static string SwapLetter(this string str, char letter, char withLetter)
        {
            str = str.Replace(letter, '@');
            str = str.Replace(withLetter, letter);
            str = str.Replace('@', withLetter);

            return str;
        }

        public static string RotateRight(this string str, int count)
        {
            if (count >= str.Length)
            {
                count = count - str.Length;
            }

            return str.Substring(str.Length - count, count) + str.Substring(0, str.Length - count);
        }

        public static string RotateLeft(this string str, int count)
        {
            if (count >= str.Length)
            {
                count = count - str.Length;
            }

            return str.Substring(count, str.Length - count) + str.Substring(0, count);
        }

        public static string RotateByLetter(this string str, char letter)
        {
            int count = str.IndexOf(letter);

            if (count >= 4)
            {
                count++;
            }

            return str.RotateRight(count + 1);
        }

        public static string ReversePositions(this string str, int position, int withPosition)
        {
            return str.Substring(0, position) +
                   new string(str.Substring(position, withPosition - position + 1).Reverse().ToArray()) +
                   str.Substring(withPosition + 1, str.Length - withPosition - 1);
        }

        public static string Move(this string str, int position, int toPosition)
        {
            string letter = str[position].ToString();
            str = str.Replace(letter, "");
            return str.Insert(toPosition, letter);
        }
    }
}