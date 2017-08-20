using UnityEngine;
using System.Text;
using System.Linq;
namespace HK.Inui
{
    /// <summary>
    /// Inuinf*ck言語に変換するクラス
    /// </summary>
    public static class Encoder
    {
        public static string Encode(string message)
        {
            var result = new StringBuilder();
            var current = 0;
            foreach(var c in message)
            {
                if(!IsAlphabetAndNumber(c))
                {
                    return string.Format("<color=red>英数字ではない文字があります</color>");
                }

                var diff = c - current;
                var absDiff = Mathf.Abs(diff);
                var isPositive = diff > 0;
                var word = isPositive ? Inui.ReservedWord.Increment : Inui.ReservedWord.Decrement;
                result.Append(string.Concat(Enumerable.Repeat(word, absDiff).ToArray()));
                result.Append(Inui.ReservedWord.Echo);
                current = c;
            }
            return result.ToString();
        }

        private static bool IsAlphabetAndNumber(char c)
        {
            return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c >= '0' && c <= '9';
        }
    }
}
