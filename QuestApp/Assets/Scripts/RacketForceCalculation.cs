using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class RacketForceCalculation : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _racketRigidbody;

    [SerializeField]
    private GameObject _container;

    private Vector3 _lastPosition;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var newPosition = _container.transform.position;
        var newRotation = _container.transform.rotation;
        _racketRigidbody.MovePosition(newPosition);
        _racketRigidbody.MoveRotation(newRotation);

        // Calculate velocity based on position changes
        _velocity = (newPosition - _lastPosition) / Time.fixedDeltaTime;
        _lastPosition = gameObject.transform.position;
    }

    void OnCollisionExit(Collision collision)
    {
        DebugDisplay.Instance.UpdateCollisionText($"Collision exit {DateTime.Now.TimeOfDay}");
        if (collision.gameObject.CompareTag("PadelBall"))
        {
            DebugDisplay.Instance.UpdateCollisionText($"Ball collision exit {DateTime.Now.TimeOfDay}");
            //_ballOwnershipTransfer.ReleaseBallOwnershipIfNeeded();
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        DebugDisplay.Instance.UpdateCollisionText($"Collision enter {DateTime.Now.TimeOfDay}");
        if (collision.gameObject.CompareTag("PadelBall"))
        {
            DebugDisplay.Instance.UpdateCollisionText($"Ball collision enter {DateTime.Now.TimeOfDay}");
        }
    }
}
