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

    [SerializeField] private BallOwnershipTransfer _ballOwnershipTransfer;

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
        DebugDisplay.Instance.UpdateDebugText("Collision exit");
        if (collision.gameObject.CompareTag("PadelBall"))
        {
            DebugDisplay.Instance.UpdateDebugText("Ball collision exit");
            _ballOwnershipTransfer.ReleaseBallOwnershipIfNeeded();
        }
    }
}
