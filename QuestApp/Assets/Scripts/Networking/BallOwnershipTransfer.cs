using System.Linq;
using Assets.Scripts.Utilities;
using Fusion;
using UnityEngine;

public class BallOwnershipTransfer : NetworkBehaviour
{
    [SerializeField] private GameObject _playerGameObject;

    private Vector3 _previousPosition;
    
    private bool _transferring;
    private bool _ownershipFromUpdate = false;

    private bool _canRequestAuthority = false;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!_transferring)
        {
            return;
        }

        if (BallMovingTowardsPlayer())
        {
            if (_canRequestAuthority)
            {
                _canRequestAuthority = false;
                DebugDisplay.Instance.UpdateDebugText("Ball close, requesting ownership");
                RequestBallOwnershipIfNeeded(true);
                DebugDisplay.Instance.UpdateDebugText("Ownership requested");
            }
        }

        DebugDisplay.Instance.UpdateBallOwnershipText(Object.HasStateAuthority, !_ownershipFromUpdate);

        _previousPosition = gameObject.transform.position;
    }

    private bool BallMovingTowardsPlayer()
    {
        var previousDistance = (_previousPosition.Flattened() - _playerGameObject.gameObject.transform.position.Flattened()).magnitude;
        var newDistance = (gameObject.transform.position.Flattened() - _playerGameObject.gameObject.transform.position.Flattened()).magnitude;
        return newDistance < previousDistance;
    }

    public void StartTransferring()
    {
        _transferring = true;
    }

    public void RequestBallOwnershipIfNeeded(bool fromUpdate = false)
    {
        if (_transferring && !Object.HasStateAuthority)
        {
            Object.RequestStateAuthority();
        }

        _ownershipFromUpdate = fromUpdate;
    }

    public void ReleaseBallOwnershipIfNeeded()
    {
        if (Object.HasStateAuthority)
        {
            //Object.ReleaseStateAuthority();

            DebugDisplay.Instance.UpdateDebugText("Releasing ownership");

            RpcSignalRelease(Runner.LocalPlayer);
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RpcSignalRelease(PlayerRef playerRef)
    {
        if (playerRef == Runner.LocalPlayer)
        {
            return;
        }

        _canRequestAuthority = true;
        DebugDisplay.Instance.UpdateDebugText("Can ask ownership");
    }
}
