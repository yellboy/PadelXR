using System.Linq;
using Assets.Scripts.Utilities;
using Fusion;
using Meta.WitAi;
using UnityEngine;

namespace Assets.Scripts.Networking
{
    public class NetworkRunnerHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;

        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _controllerAnchor;

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            var spawnPosition = runner.LocalPlayer.PlayerId == 2 ? new Vector3(0, 0, 8) : new Vector3(0, 0, -8);
            var spawnRotation = runner.LocalPlayer.PlayerId == 2 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);

            PlayerControls playerControls;
            if (runner.IsServer)
            {
                var playerObject = runner.Spawn(_playerPrefab, spawnPosition, spawnRotation, player);
                playerObject.gameObject.transform.SetParent(Camera.main.transform);

                playerControls = playerObject.GetComponent<PlayerControls>();
            }
            else
            {
                playerControls = FindObjectsOfType<PlayerControls>().Single(p => p.HasInputAuthority);
            }

            playerControls.Init(_controllerAnchor, _ball);


            if (player.PlayerId == runner.LocalPlayer.PlayerId)
            {
                runner.ProvideInput = true;
                playerControls.PlayerSpawned();
            }

            if (runner.LocalPlayer.PlayerId == 2)
            {
                playerControls.gameObject.transform.position = new Vector3(0, 0, 8);
                playerControls.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            var text =
$@"Player {player.PlayerId} joined.
Is server: {runner.IsServer}
Is master client: {player.IsMasterClient},
Is real player: {player.IsRealPlayer}.

Local player: {runner.LocalPlayer.PlayerId}.
Active players: {string.Join(", ", runner.ActivePlayers.Select(p => p.PlayerId))}";

            DebugDisplay.Instance.UpdateDebugText(text);

        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            DebugDisplay.Instance.UpdateDebugText("Connected to server");
        }
    }
}
