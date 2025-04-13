using System;
using Assets.Scripts.Networking;
using Assets.Scripts.Utilities;
using Fusion;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var data = new NetworkInputData();

            data.IndexButtonPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);

            var success = input.Set(data);

            var text =
$@"Sending input: {success},
Player: {runner.LocalPlayer},
Pressed: {data.IndexButtonPressed},
Timestamp: {DateTime.UtcNow}";

            DebugDisplay.Instance.UpdateCommunicationText(text);
        }
    }
}
