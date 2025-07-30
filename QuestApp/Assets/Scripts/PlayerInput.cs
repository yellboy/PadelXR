using System;
using Assets.Scripts.Networking;
using Assets.Scripts.Utilities;
using Fusion;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private GameObject _leftController;

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (runner.IsServer)
            {
                return;
            }

            var data = new NetworkInputData();

            data.IndexButtonPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
            data.XButtonPressed = OVRInput.Get(OVRInput.Button.Three);
            data.LeftControllerPosition = _leftController.transform.position;

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
