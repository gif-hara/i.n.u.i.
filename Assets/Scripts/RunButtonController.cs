using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HK.Inui
{
    public sealed class RunButtonController : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField]
        private Inui inui;

        [SerializeField]
        private InputField inputField;

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            this.inui.Run(this.inputField.text);
        }
    }
}
