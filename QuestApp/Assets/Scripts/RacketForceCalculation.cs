using System.Collections;
using System.Collections.Generic;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PadelBall"))
        {
            Rigidbody ballRb = collision.rigidbody;

            //ballRb.AddForce(_velocity);

            // Apply spin (optional)
            ballRb.AddTorque(Vector3.right * 5f, ForceMode.Impulse);
        }
    }
}
