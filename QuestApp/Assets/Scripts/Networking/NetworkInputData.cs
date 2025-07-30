using Fusion;
using UnityEngine;

namespace Assets.Scripts.Networking
{
    public struct NetworkInputData : INetworkInput
    {
        public bool IndexButtonPressed;

        public bool XButtonPressed;

        public Vector3 LeftControllerPosition;
    }
}
