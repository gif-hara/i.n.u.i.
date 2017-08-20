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
            /// ポインタの指すメモリの値が0だったら次の<see cref="WhileEnd"/>に進む
			/// </summary>
            public const string WhileStart = "何すかぁ？W";

			/// <summary>
            /// <see cref="WhileEnd"/>より手前の<see cref="WhileStart"/>に戻る
			/// </summary>
            public const string WhileEnd = "俺の友達それで死んだけど";

            /// <summary>
            /// 出力
            /// </summary>
            public const string Echo = "そういうとこよ";

            /// <summary>
            /// 現在のメモリの状態とポインタの位置を出力する
            /// </summary>
            public const string Print = "キッズじゃんWWW";
		}

        private readonly string[] ReservedWords = new string[]
        {
			ReservedWord.Increment,
            ReservedWord.Decrement,
            ReservedWord.MoveRight,
            ReservedWord.MoveLeft,
            ReservedWord.WhileStart,
			ReservedWord.WhileEnd,
            ReservedWord.Echo,
            ReservedWord.Print,
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
            var printedCount = 0;
            for (int sortWordIndex = 0; sortWordIndex < sortWords.Count; ++sortWordIndex)
            {
                var word = sortWords[sortWordIndex].Word;
                switch(word)
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
                        Assert.IsTrue(index >= 0, string.Format("{0}番目の{1}でポインタが負数になりました", sortWordIndex, ReservedWord.MoveLeft));
                        break;
                    case ReservedWord.WhileStart:
                        if(values[index] == 0)
                        {
                            for (int gotoIndex = sortWordIndex + 1; gotoIndex < sortWords.Count; ++gotoIndex)
                            {
                                if(sortWords[gotoIndex].Word == ReservedWord.WhileEnd)
                                {
                                    sortWordIndex = gotoIndex + 1;
                                    break;
                                }
                            }
                        }
                        break;
                    case ReservedWord.WhileEnd:
                        for (int gotoIndex = sortWordIndex - 1; gotoIndex >= 0; --gotoIndex)
                        {
                            if(sortWords[gotoIndex].Word == ReservedWord.WhileStart)
                            {
                                sortWordIndex = gotoIndex - 1;
                                break;
                            }
                        }

                        //Assert.IsTrue(false, string.Format("{0}に対応する{1}がありませんでした", ReservedWord.GotoPrevious, ReservedWord.GotoNext));
                        break;
                    case ReservedWord.Echo:
                        builder.Append((char)values[index]);
                        break;
                    case ReservedWord.Print:
                        var printBuilder = new StringBuilder();
                        ++printedCount;
                        printBuilder.AppendLine(string.Format("Print[{0}]", printedCount));
                        printBuilder.AppendLine(string.Format("Index = {0}", index));
                        for (int i = 0; i < values.Count; ++i)
                        {
                            printBuilder.AppendLine(string.Format("Value[{0}] = {1}", i, values[i]));
                        }
                        Debug.Log(printBuilder.ToString());
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("{0} は未対応です", word));
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
