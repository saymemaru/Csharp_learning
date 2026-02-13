using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 字符串常用操作
{
    internal static class 字符串
    {
        //How are you -> you are How
        public static void WordsReverse(ref string words)
        {
            string[] wordsArray = words.Split("");

            Array.Reverse(wordsArray);

            string result = string.Join(" ", wordsArray);
            words = result;

        }

        //How are you -> woH era uoy
        public static void WordsCharsReverse(ref string words)
        {
            string[] wordsArray = words.Split("");
            foreach (string word in wordsArray)
            {
                word.Reverse();
            }

            string result = string.Join(" ", wordsArray);
            words = result;
        }
    }
}
