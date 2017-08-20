using UnityEngine;
using UnityEngine.UI;
using System;

namespace HK.Inui
{
    public sealed class ShortcutModeController : MonoBehaviour
    {
        [SerializeField]
        private InputField inputField;

        [SerializeField]
        private InputField output;

        void Update()
        {
            this.OnInputAction(KeyCode.Semicolon, () => this.InsertTo(Inui.ReservedWord.Increment));
            this.OnInputAction(KeyCode.Minus, () => this.InsertTo(Inui.ReservedWord.Decrement));
            this.OnInputAction(KeyCode.LeftBracket, () => this.InsertTo(Inui.ReservedWord.WhileStart));
            this.OnInputAction(KeyCode.RightBracket, () => this.InsertTo(Inui.ReservedWord.WhileEnd));

			if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                this.OnInputAction(KeyCode.Period, () => this.InsertTo(Inui.ReservedWord.MoveRight));
                this.OnInputAction(KeyCode.Comma, () => this.InsertTo(Inui.ReservedWord.MoveLeft));
                this.OnInputAction(KeyCode.D, () => this.InsertTo(Inui.ReservedWord.Print));
                this.OnInputAction(KeyCode.Return, this.Run);
                this.OnInputAction(KeyCode.Backspace, this.DeleteWord);
			}
            else
            {
                this.OnInputAction(KeyCode.Period, () => this.InsertTo(Inui.ReservedWord.Echo));
                this.OnInputAction(KeyCode.Return, () => this.InsertTo(System.Environment.NewLine));
				this.OnInputAction(KeyCode.Backspace, this.DeleteChar);
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

        private void Run()
        {
			this.output.text = Inui.Run(this.inputField.text);
		}

        private void OnInputAction(KeyCode keyCode, Action action)
        {
            if(Input.GetKeyDown(keyCode))
            {
                action();
            }
        }

        private void InsertTo(string reservedWord)
        {
            this.inputField.text = this.inputField.text.Insert(this.inputField.caretPosition, reservedWord);
            this.inputField.caretPosition += reservedWord.Length;
		}

        private void DeleteWord()
        {
			var text = this.inputField.text;
			var index = this.inputField.caretPosition;
			var deleted = false;
			while (index >= 0 && !deleted)
			{
				foreach (var r in Inui.ReservedWords)
				{
					if (text.IndexOf(r, index, System.StringComparison.CurrentCulture) != -1)
					{
						this.inputField.text = text.Remove(index, r.Length);
						this.inputField.caretPosition = index;
						deleted = true;
						break;
					}
				}
				--index;
			}
		}

        private void DeleteChar()
        {
            if(this.inputField.caretPosition <= 0)
            {
                return;
            }
            var oldCaretPosition = this.inputField.caretPosition;
            this.inputField.text = this.inputField.text.Remove(this.inputField.caretPosition - 1, 1);
            if (this.inputField.caretPosition == oldCaretPosition)
            {
				this.inputField.caretPosition--;
			}
        }
    }
}
