using System.Collections;
using Assets.Scripts.Networking;
using Fusion;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerControls : NetworkBehaviour
    {

        private GameObject _controllerAnchor;

        private GameObject _ball;

        private bool _holdingBallInHand = false;

        private bool _holdingBallAtDefault = false;

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();

            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (!_holdingBallInHand)
                {
                    _holdingBallInHand = true;
                    MoveBallToControllerAction();
                }

                TrackController();
            }
            else
            {
                if (_holdingBallInHand)
                {
                    _holdingBallInHand = false;
                    DropBallAction();
                }
            }

            if (OVRInput.Get(OVRInput.Button.Three))
            {
                if (!_holdingBallAtDefault)
                {
                    _holdingBallAtDefault = true;
                }

                MoveBallToDefaultAction();
            }
            else
            {
                if (_holdingBallAtDefault)
                {
                    _holdingBallAtDefault = false;
                    DropBallAction();
                }
            }

            if (Runner.IsClient)
            {
                return;
            }

            var success = GetInput<NetworkInputData>(out var input);

            if (!success)
            {
                return;
            }

            if (input.IndexButtonPressed)
            {
                if (!_holdingBallInHand)
                {
                    _holdingBallInHand = true;
                    MoveBallToControllerAction();
                }

                TrackController(input.LeftControllerPosition);
            }
            else
            {
                if (_holdingBallInHand)
                {
                    _holdingBallInHand = false;
                    DropBallAction();
                }
            }

            if (input.XButtonPressed)
            {
                if (!_holdingBallAtDefault)
                {
                    _holdingBallAtDefault = true;
                }

                MoveBallToDefaultAction();
            }
            else
            {
                if (_holdingBallAtDefault)
                {
                    _holdingBallAtDefault = false;
                    DropBallAction();
                }
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

        public void MoveBallToControllerAction()
        {
            var ballRb = _ball.GetComponent<Rigidbody>();
            ballRb.isKinematic = true;
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;

            print($"Ball moved to controller: {_ball.transform.position}");
        }

        private void TrackController(Vector3? position = null)
        {
            _ball.transform.rotation = Quaternion.identity;
            _ball.transform.position = position ?? _controllerAnchor.transform.position;

            print($"Ball tracking controller: {_ball.transform.position}");
        }

        public void PlayerSpawned()
        {
            StartCoroutine(nameof(ResetBallAfterSpawn));
        }

        private IEnumerator ResetBallAfterSpawn()
        {
            yield return new WaitForFixedUpdate();

            MoveBallToDefaultAction();

            yield return new WaitForFixedUpdate();

            DropBallAction();
        }

        public void Init(GameObject controllerAnchor, GameObject ball)
        {
            _controllerAnchor = controllerAnchor;
            _ball = ball;
        }
    }
}
