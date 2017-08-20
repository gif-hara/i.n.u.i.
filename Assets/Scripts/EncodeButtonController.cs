using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HK.Inui
{
    public sealed class EncodeButtonController : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField]
        private InputField inputField;

        [SerializeField]
        private InputField outputText;

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            var output = Encoder.Encode(this.inputField.text);
            this.outputText.text = output;
        }
    }
}
