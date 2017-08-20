using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace HK.Inui
{
    public sealed class CheatSheet : MonoBehaviour
    {
        [SerializeField]
        private Text text;

        void Start()
        {
            var builder = new StringBuilder();
			builder.AppendLine(string.Format("{0} = ポインタの指すメモリの値を1増やす", Inui.ReservedWord.Increment));
            builder.AppendLine(string.Format("{0} = ポインタの指すメモリの値を1減らす", Inui.ReservedWord.Decrement));
			builder.AppendLine(string.Format("{0} = ポインタを右に1つずらす", Inui.ReservedWord.MoveRight));
			builder.AppendLine(string.Format("{0} = ポインタを左に1つずらす", Inui.ReservedWord.MoveLeft));
			builder.AppendLine(string.Format("{0} = ポインタの指すメモリの値が0だったら次の{1}へ移動する", Inui.ReservedWord.WhileStart, Inui.ReservedWord.WhileEnd));
			builder.AppendLine(string.Format("{0} = 前の{1}へ戻る", Inui.ReservedWord.WhileEnd, Inui.ReservedWord.WhileStart));
			builder.AppendLine(string.Format("{0} = ポインタの指すメモリの値を出力する", Inui.ReservedWord.Echo));
            builder.AppendLine(string.Format("{0} = 現在の状態をデバッグ表示する", Inui.ReservedWord.Print));
            this.text.text = builder.ToString();
        }
    }
}
