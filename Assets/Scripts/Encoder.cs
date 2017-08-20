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
                var diff = c - current;
                var absDiff = Mathf.Abs(diff);
                var isPositive = diff > 0;
                var word = isPositive ? Inui.ReservedWord.Increment : Inui.ReservedWord.Decrement;
                result.AppendLine(string.Concat(Enumerable.Repeat(word, absDiff).ToArray()));
                result.AppendLine(Inui.ReservedWord.Echo);
                current = c;
            }
            return result.ToString();
        }
    }
}
