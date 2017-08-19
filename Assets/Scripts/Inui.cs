using UnityEngine;
using UnityEngine.UI;

namespace HK.Inui
{
    public sealed class Inui : MonoBehaviour
    {
        [SerializeField]
        private Text outputText;

        public void Run(string source)
        {
            this.outputText.text = source;
        }
    }
}
