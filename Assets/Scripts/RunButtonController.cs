﻿using UnityEngine;
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

        [SerializeField]
        private Text outputText;

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            var output = this.inui.Run(this.inputField.text);
            this.outputText.text = output;
        }
    }
}