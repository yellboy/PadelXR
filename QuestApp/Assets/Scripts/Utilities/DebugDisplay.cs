using TMPro;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class DebugDisplay : MonoBehaviour
    {
        public static DebugDisplay Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI _debugText;

        [SerializeField] private TextMeshProUGUI _ballOwnershipText;

        [SerializeField] private TextMeshProUGUI _collisionText;

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

        public void UpdateCommunicationText(string text)
        {
            _ballOwnershipText.text = text;
        }

        public void UpdateCollisionText(string collisionText)
        {
            _collisionText.text = collisionText;
        }
    }
}
