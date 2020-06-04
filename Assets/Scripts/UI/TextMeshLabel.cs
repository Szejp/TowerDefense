using TMPro;
using UnityEngine;

namespace AFSInterview.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshLabel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMesh;

        public string Text
        {
            get => textMesh.text;
            set => textMesh.text = value;
        }

        private void Awake()
        {
            if (textMesh == null)
                textMesh.GetComponent<TextMeshProUGUI>();
        }
    }
}