using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HK.Inui
{
    public sealed class ToggleShortcutButtonController : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField]
        private ShortcutModeController controller;

        [SerializeField]
        private Text buttonText;

		[SerializeField]
		private string enableText;

		[SerializeField]
		private string disableText;

        void Start()
        {
            this.UpdateButtonText();
        }

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            this.controller.enabled = !this.controller.enabled;
            this.UpdateButtonText();
        }

        private void UpdateButtonText()
        {
			this.buttonText.text = this.controller.enabled ? this.enableText : this.disableText;
		}
    }
}
