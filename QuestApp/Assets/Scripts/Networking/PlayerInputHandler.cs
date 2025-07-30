using System;
using Assets.Scripts.Utilities;
using Fusion;

namespace Assets.Scripts.Networking
{
    public class PlayerInputHandler : NetworkBehaviour
    {
        public override void FixedUpdateNetwork()
        {
            if (Runner.IsClient)
            {
                return;
            }

            var success = GetInput<NetworkInputData>(out var input);

            var text =
$@"Received input: {success},
Pressed index: {input.IndexButtonPressed},
Pressed 3: {input.XButtonPressed},
Controller position: {input.LeftControllerPosition},
Timestamp: {DateTime.UtcNow}";

            DebugDisplay.Instance.UpdateCommunicationText(text);
        }
    }
}
