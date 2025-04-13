using System;
using Assets.Scripts.Utilities;
using Fusion;

namespace Assets.Scripts.Networking
{
    public class PlayerInputHandler : NetworkBehaviour
    {
        public override void FixedUpdateNetwork()
        {
            var success = GetInput<NetworkInputData>(out var input);

            var text =
$@"Received input: {success},
Pressed: {input.IndexButtonPressed},
Timestamp: {DateTime.UtcNow}";

            DebugDisplay.Instance.UpdateCommunicationText(text);
        }
    }
}
