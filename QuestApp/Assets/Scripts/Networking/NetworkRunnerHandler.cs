using System.Linq;
using Assets.Scripts.Utilities;
using Fusion;
using UnityEngine;

namespace Assets.Scripts.Networking
{
    public class NetworkRunnerHandler : MonoBehaviour
    {
        [SerializeField] private PlayerControls _playerControls;

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            var text =
$@"Player {player.PlayerId} joined.
Is server: {runner.IsServer}
Is master client: {player.IsMasterClient},
Is real player: {player.IsRealPlayer}.

Local player: {runner.LocalPlayer.PlayerId}.
Active players: {string.Join(", ", runner.ActivePlayers.Select(p => p.PlayerId))}";

            DebugDisplay.Instance.UpdateDebugText(text);

            if (player.PlayerId == runner.LocalPlayer.PlayerId)
            {
                _playerControls.PlayerSpawned();
            }
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            DebugDisplay.Instance.UpdateDebugText("Connected to server");
        }
    }
}
