using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class BallOwnershipTransfer : MonoBehaviour
{
    [SerializeField] private NetworkObject _ballNetworkObject;
    
    private bool _transferring;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_transferring &&
            !_ballNetworkObject.HasInputAuthority && 
            (_ballNetworkObject.gameObject.transform.position - gameObject.transform.position).magnitude <= 1)
        {
            _ballNetworkObject.RequestStateAuthority();
        }
    }

    public void StartTransferring()
    {
        _transferring = true;
    }
}
