using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;

public class PlayerControls : NetworkBehaviour
{

    [SerializeField] private GameObject _controllerAnchor;

    [SerializeField] private GameObject _ball;

    private bool _holdingBall = false;

    private bool _moveBallToController = false;

    private bool _dropBall = false;

    private bool _moveBallToDefault = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (_moveBallToDefault)
        {
            _moveBallToDefault = false;
            MoveBallToDefaultAction();
        }

        if (_dropBall)
        {
            _holdingBall = false;
            _dropBall = false;
            DropBallAction();
        }

        if (_holdingBall)
        {
            if (_moveBallToController)
            {
                _moveBallToController = false;
                MoveBallToControllerAction();
            }

            TrackController();
        }
    }

    private void MoveBallToDefaultAction()
    {
        var ballRb = _ball.GetComponent<Rigidbody>();
        ballRb.isKinematic = true;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        _ball.transform.rotation = Quaternion.identity;
        _ball.transform.position = new Vector3(0, 10, 2.07f);

        print($"Ball moved to default: {_ball.transform.position}");
    }

    private void DropBallAction()
    {
        var ballRb = _ball.GetComponent<Rigidbody>();
        ballRb.isKinematic = false;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        print($"Dropping the ball: {_ball.transform.position}");
    }

    public void MoveBallToController()
    {
        _moveBallToController = true;
        _holdingBall = true;
    }

    public void MoveBallToControllerAction()
    {
        var ballRb = _ball.GetComponent<Rigidbody>();
        ballRb.isKinematic = true;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        print($"Ball moved to controller: {_ball.transform.position}");
    }

    private void TrackController()
    {
        _ball.transform.rotation = Quaternion.identity;
        _ball.transform.position = _controllerAnchor.transform.position;

        print($"Ball tracking controller: {_ball.transform.position}");
    }

    public void DropBall()
    {
        _dropBall = true;
    }

    public void MoveBallToDefault()
    {
        _moveBallToDefault = true;
    }

    public void PlayerSpawned()
    {
        StartCoroutine(nameof(ResetBallAfterSpawn));

        //MoveBallToDefault();
        //DropBall();
    }

    private IEnumerator ResetBallAfterSpawn()
    {
        yield return new WaitForFixedUpdate();

        MoveBallToDefaultAction();

        yield return new WaitForFixedUpdate();

        DropBallAction();
    }
}
