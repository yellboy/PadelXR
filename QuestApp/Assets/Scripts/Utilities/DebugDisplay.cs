using TMPro;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class DebugDisplay : MonoBehaviour
    {
        public static DebugDisplay Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI _debugText;

        [SerializeField] private TextMeshProUGUI _ballOwnershipText;

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

        public void UpdateBallOwnershipText(bool hasBallOwnership, bool requestedByPlayer)
        {
            var hasOwnershipText = $"Has ball ownership: {hasBallOwnership}.";
            var reasonText = string.Empty;
            if (hasBallOwnership)
            {
                var reason = requestedByPlayer ? "requested" : "automatic";
                reasonText = $"Reason: {reason}";
            }

            _ballOwnershipText.text =
@$"{hasOwnershipText}
{reasonText}";
        }
    }
}
