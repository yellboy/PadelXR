using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] private GameObject _controllerAnchor;

    [SerializeField] private GameObject _ball;

    [SerializeField] private BallOwnershipTransfer _ballOwnershipTransfer;

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
        _ballOwnershipTransfer.RequestBallOwnershipIfNeeded();

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
        print($"Dropping the ball: {_ball.transform.position}");
    }

    public void MoveBallToDefault()
    {
        _ballOwnershipTransfer.RequestBallOwnershipIfNeeded();

        var ballRb = _ball.GetComponent<Rigidbody>();
        ballRb.isKinematic = true;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        _ball.transform.rotation = Quaternion.identity;
        _ball.transform.position = new Vector3(0, 10, 2.07f);

        print($"Ball moved to default: {_ball.transform.position}");
    }

    public void PlayerSpawned()
    {
        StartCoroutine(nameof(ResetBallAfterSpawn));

        MoveBallToDefault();
        DropBall();
    }

    private IEnumerator ResetBallAfterSpawn()
    {
        yield return new WaitForFixedUpdate();

        MoveBallToDefault();

        yield return new WaitForFixedUpdate();

        DropBall();
    }
}
