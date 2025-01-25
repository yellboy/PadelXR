
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    private const float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        var movementRight = thumbstick.x * Speed * Time.deltaTime;
        var movementForward = thumbstick.y * Speed * Time.deltaTime;

        var flatForward = Camera.main.transform.forward.Flattened().normalized;
        var flatRight = Camera.main.transform.right.Flattened().normalized;

        gameObject.transform.position += flatForward * movementForward + flatRight * movementRight;
    }
}
