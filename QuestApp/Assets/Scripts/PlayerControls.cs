using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] private GameObject _controllerAnchor;

    [SerializeField] private GameObject _ball;

    private bool _holdingBall = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_holdingBall)
        {
            TrackController();
        }
    }

    public void MoveBallToController()
    {
        var ballRb = _ball.GetComponent<Rigidbody>();
        ballRb.isKinematic = true;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        TrackController();
        _holdingBall = true;
    }

    private void TrackController()
    {
        _ball.transform.rotation = Quaternion.identity;
        _ball.transform.position = _controllerAnchor.transform.position;
    }

    public void DropBall()
    {
        _holdingBall = false;
        var ballRb = _ball.GetComponent<Rigidbody>();
        ballRb.isKinematic = false;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
    }

    public void MoveBallToDefault()
    {
        var ballRb = _ball.GetComponent<Rigidbody>();
        ballRb.isKinematic = true;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        _ball.transform.rotation = Quaternion.identity;
        _ball.transform.position = new Vector3(0, 10, 2.07f);
    }

}
