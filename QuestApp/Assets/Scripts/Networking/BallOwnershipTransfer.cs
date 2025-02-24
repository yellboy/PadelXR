using Assets.Scripts.Utilities;
using Fusion;
using UnityEngine;

public class BallOwnershipTransfer : NetworkBehaviour
{
    [SerializeField] private GameObject _playerGameObject;
    
    private bool _transferring;
    private bool _ownershipFromUpdate = false;
    private bool _releaseRequested = false;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!_transferring)
        {
            return;
        }

        if (_releaseRequested)
        {
            DebugDisplay.Instance.UpdateDebugText("Release requested");
            if (Object.HasStateAuthority)
            {
                Object.ReleaseStateAuthority();
                DebugDisplay.Instance.UpdateDebugText("Released");
            }

            if (GetPlayerDistance() > 5f)
            {
                _releaseRequested = false;
                DebugDisplay.Instance.UpdateDebugText("Ball went away, can request ownership");
            }
        }
        else if (GetPlayerDistance() <= 5f)
        {
            DebugDisplay.Instance.UpdateDebugText("Ball close, requesting ownership");
            RequestBallOwnershipIfNeeded(true);
            DebugDisplay.Instance.UpdateDebugText("Ownership requested");
        }

        DebugDisplay.Instance.UpdateBallOwnershipText(Object.HasStateAuthority, !_ownershipFromUpdate);
    }

    private float GetPlayerDistance()
    {
        return (gameObject.transform.position - _playerGameObject.gameObject.transform.position).magnitude;
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
        _releaseRequested = true;
    }
}
