using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System.Text;

namespace HK.Inui
{
    public sealed class Inui : MonoBehaviour
    {
        [SerializeField]
        private Text outputText;

        public static class ReservedWord
        {
			/// <summary>
			/// ポインタの指すメモリの値を1増やす
			/// </summary>
            public const string Increment = "マ？";

			/// <summary>
			/// ポインタの指すメモリの値を1減らす
			/// </summary>
			public const string Decrement = "がちやな";

            /// <summary>
            /// ポインタを右に1つずらす
            /// </summary>
            public const string MoveRight = "あるなぁ";

			/// <summary>
			/// ポインタを左に1つずらす
			/// </summary>
			public const string MoveLeft = "あるか？";

			/// <summary>
            /// ポインタの指すメモリの値が0だったら次の<see cref="GotoPrevious"/>に進む
			/// </summary>
			public const string GotoNext = "僕っすかぁ？W";

			/// <summary>
            /// ポインタの指すメモリの値が0でなければ前の<see cref="GotoNext"/>に戻る
			/// </summary>
			public const string GotoPrevious = "何すかぁ？W";

            /// <summary>
            /// 出力
            /// </summary>
            public const string Echo = "そういうとこよ";
		}

        private readonly string[] ReservedWords = new string[]
        {
			ReservedWord.Increment,
            ReservedWord.Decrement,
            ReservedWord.MoveRight,
            ReservedWord.MoveLeft,
            ReservedWord.GotoNext,
			ReservedWord.GotoPrevious,
            ReservedWord.Echo,
        };

        public class SortWord
        {
            public int Index;

            public string Word;
        }

        public string Run(string source)
        {
            return this.GetOutput(source);
        }

        /// <summary>
        /// 予約語のみのソートされたリストを返す
        /// </summary>
        private List<SortWord> GetSortedWords(string source)
        {
			var result = new List<SortWord>();

			for (int i = 0; i < ReservedWords.Length; ++i)
			{
				var reservedWord = ReservedWords[i];
				int sourceIndex = 0;
				while (source.Length > sourceIndex)
				{
					var indexOf = source.IndexOf(reservedWord, sourceIndex, StringComparison.CurrentCulture);
					if (indexOf != -1)
					{
						result.Add(new SortWord() { Index = indexOf, Word = reservedWord });
						sourceIndex = indexOf + reservedWord.Length;
					}
					else
					{
						break;
					}
				}
			}

			result.Sort((x, y) => x.Index - y.Index);

            return result;
		}

        private string GetOutput(string source)
        {
            var values = new List<int>();
            values.Add(0);
            var index = 0;
            var sortWords = this.GetSortedWords(source);
            var builder = new StringBuilder();
            sortWords.ForEach(s =>
            {
                switch(s.Word)
                {
                    case ReservedWord.Increment:
                        ++values[index];
                        break;
                    case ReservedWord.Decrement:
                        --values[index];
                        break;
                    case ReservedWord.MoveRight:
                        ++index;
                        if(values.Count <= index)
                        {
                            values.Add(0);
                        }
                        break;
                    case ReservedWord.MoveLeft:
                        --index;
                        Assert.IsTrue(index > 0, "indexが負数になりました");
                        break;
                    case ReservedWord.Echo:
                        builder.Append((char)values[index]);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("{0} は未対応です", s.Word));
                        break;
                }
            });

            return builder.ToString();
        }
    }
}
