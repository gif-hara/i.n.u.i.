using UnityEngine;
using UnityEngine.UI;

namespace HK.Inui
{
    public sealed class ShortcutModeController : MonoBehaviour
    {
        [SerializeField]
        private InputField inputField;

        void Update()
        {
			if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
				this.OnInputInsert(KeyCode.Semicolon, Inui.ReservedWord.Increment);
				this.OnInputInsert(KeyCode.Minus, Inui.ReservedWord.Decrement);
				this.OnInputInsert(KeyCode.Period, Inui.ReservedWord.MoveRight);
                this.OnInputInsert(KeyCode.Comma, Inui.ReservedWord.MoveLeft);
				this.OnInputInsert(KeyCode.LeftBracket, Inui.ReservedWord.WhileStart);
				this.OnInputInsert(KeyCode.RightBracket, Inui.ReservedWord.WhileEnd);
				this.OnInputInsert(KeyCode.D, Inui.ReservedWord.Print);
				this.OnInputInsert(KeyCode.Return, System.Environment.NewLine);
                this.OnInputRun(KeyCode.Return);
			}
            else
            {
                this.OnInputInsert(KeyCode.Minus, Inui.ReservedWord.Decrement);
                this.OnInputInsert(KeyCode.Period, Inui.ReservedWord.Echo);
				this.OnInputInsert(KeyCode.LeftBracket, Inui.ReservedWord.WhileStart);
                this.OnInputInsert(KeyCode.RightBracket, Inui.ReservedWord.WhileEnd);
                this.OnInputInsert(KeyCode.Return, System.Environment.NewLine);
            }
            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                var text = this.inputField.text;
                var index = this.inputField.caretPosition;
                var deleted = false;
                while(index >= 0 && !deleted)
                {
					foreach (var r in Inui.ReservedWords)
					{
                        if(text.IndexOf(r, index, System.StringComparison.CurrentCulture) != -1)
                        {
                            this.inputField.text = text.Remove(index, r.Length);
                            deleted = true;
                            break;
                        }
					}
                    --index;
				}
            }
        }

        void OnEnable()
        {
            this.inputField.readOnly = true;
        }

        void OnDisable()
        {
            this.inputField.readOnly = false;
        }

        private void OnInputInsert(KeyCode keyCode, string reservedWord)
        {
            if (Input.GetKeyDown(keyCode))
			{
                this.InsertTo(reservedWord);
			}
		}

        private void OnInputRun(KeyCode keyCode)
        {
            if(Input.GetKeyDown(keyCode))
            {
                Inui.Run(this.inputField.text);
            }
        }

        private void InsertTo(string reservedWord)
        {
            this.inputField.text = this.inputField.text.Insert(this.inputField.caretPosition, reservedWord);
            this.inputField.caretPosition += reservedWord.Length;
		}
    }
}
