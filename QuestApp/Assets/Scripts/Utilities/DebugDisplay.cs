using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities
{
    public class DebugDisplay : MonoBehaviour
    {
        public static DebugDisplay Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI _debugText;

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void UpdateDebugText(string debugText)
        {
            _debugText.text = debugText;
        }
    }
}
