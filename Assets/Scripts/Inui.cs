using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace HK.Inui
{
    public sealed class Inui : MonoBehaviour
    {
        [SerializeField]
        private Text outputText;

        private readonly string[] ReservedWords = new string[]
        {
            "マ？", // ポインタの指すメモリの値を1増やす
            "がちやな", // ポインタの指すメモリの値を1減らす
            "あるなぁ", // ポインタを右に1つずらす
            "あるか？", // ポインタを左に1つずらす
            "僕っすかぁ？W", // ポインタの指すメモリの値が0だったら次の「何すかぁ？W」に進む
            "何すかぁ？W", // ポインタの指すメモリの値が0でなければ前の「僕っすかぁ？W」に戻る
            "そういうとこよ", // 出力
        };

        public class SortWord
        {
            public int Index;

            public string Word;
        }

        public string Run(string source)
        {
            return source;
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
    }
}
