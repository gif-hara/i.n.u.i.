using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System.Text;

namespace HK.Inui
{
    public static class Inui
    {
        private const int ExecuteCountMax = 1000000;

        public static class ReservedWord
        {
			/// <summary>
			/// ポインタの指すメモリの値を1増やす
			/// </summary>
            public const string Increment = "マ？";

			/// <summary>
			/// ポインタの指すメモリの値を1減らす
			/// </summary>
			public const string Decrement = "が？";

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
            public const string Echo = "ケンタッキー買ってきます！";

            /// <summary>
            /// 現在のメモリの状態とポインタの位置を出力する
            /// </summary>
            public const string Print = "キッズじゃんWWW";
		}

        public static readonly string[] ReservedWords = new string[]
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

        public static string Run(string source)
        {
			var values = new List<int>();
			values.Add(0);
			var index = 0;
            var planeSources = GetPlaneSource(source);
			var result = new StringBuilder();
			var printedCount = 0;
            var totalExecuteCount = 0;
            var executeCount = new Dictionary<string, int>();
            foreach(var r in ReservedWords)
            {
                executeCount.Add(r, 0);
            }
            for (int planeSourceIndex = 0; planeSourceIndex < planeSources.Count; ++planeSourceIndex)
            {
                var word = planeSources[planeSourceIndex].Word;
                ++executeCount[word];
				switch (word)
                {
                    case ReservedWord.Increment:
                        ++values[index];
                        break;
                    case ReservedWord.Decrement:
                        --values[index];
                        break;
                    case ReservedWord.MoveRight:
                        ++index;
                        if (values.Count <= index)
                        {
                            values.Add(0);
                        }
                        break;
                    case ReservedWord.MoveLeft:
                        --index;
                        if (index < 0)
                        {
                            return string.Format("<color=red>{0}番目の \"{1}\" でポインタが負数になりました</color>", executeCount[word], ReservedWord.MoveLeft);
                        }
						break;
					case ReservedWord.WhileStart:
						if (values[index] == 0)
						{
							var successWhileStart = false;
							for (int gotoIndex = planeSourceIndex + 1; gotoIndex < planeSources.Count; ++gotoIndex)
							{
								if (planeSources[gotoIndex].Word == ReservedWord.WhileEnd)
								{
									planeSourceIndex = gotoIndex;
									successWhileStart = true;
									break;
								}
							}
                            if(!successWhileStart)
                            {
                                return string.Format("<color=red>{0}番目の \"{1}\" に対応する \"{1}\" がありませんでした", executeCount[word], ReservedWord.WhileStart, ReservedWord.WhileEnd);
                            }
						}
						break;
					case ReservedWord.WhileEnd:
						var successWhileEnd = false;
						for (int gotoIndex = planeSourceIndex - 1; gotoIndex >= 0; --gotoIndex)
						{
							if (planeSources[gotoIndex].Word == ReservedWord.WhileStart)
							{
								planeSourceIndex = gotoIndex - 1;
								successWhileEnd = true;
								break;
							}
						}
                        if(!successWhileEnd)
                        {
                            return string.Format("<color=red>{0}番目の \"{1}\" に対応する \"{2}\" がありませんでした</color>", executeCount[word], ReservedWord.WhileEnd, ReservedWord.WhileStart);
                        }
						break;
					case ReservedWord.Echo:
						result.Append((char)values[index]);
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
                ++totalExecuteCount;
                if(totalExecuteCount > ExecuteCountMax)
                {
                    return string.Format("<color=red>実行回数が{0}回を超えたので停止します</color>", ExecuteCountMax);
                }
			}

            return result.ToString();
        }

        /// <summary>
        /// 予約語のみのデータリストを返す
        /// </summary>
        private static List<SortWord> GetPlaneSource(string source)
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
    }
}
