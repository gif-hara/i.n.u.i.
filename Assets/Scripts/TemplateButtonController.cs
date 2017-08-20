using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace HK.Inui
{
    public sealed class TemplateButtonController : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField][Multiline]
		private string template;
		
        [SerializeField]
        private InputField inputField;

        public void OnPointerUp(PointerEventData eventData)
        {
            this.inputField.text = this.template;
        }
    }
}
