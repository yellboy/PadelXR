using Assets.Scripts.Utilities;
using Fusion;
using UnityEngine;

namespace Assets.Scripts.Networking
{
    public class NetworkRunnerHandler : MonoBehaviour
    {
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            DebugDisplay.Instance.UpdateDebugText($"Player joined {player.PlayerId}. Is master client: {player.IsMasterClient}");
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            DebugDisplay.Instance.UpdateDebugText("Connected to server");
        }
    }
}
