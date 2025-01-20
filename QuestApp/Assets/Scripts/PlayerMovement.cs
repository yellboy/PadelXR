
using UnityEngine;
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

        print("thumbstick: " + thumbstick);

        var movementRight = thumbstick.x * Speed * Time.deltaTime;
        var movementForward = thumbstick.y * Speed * Time.deltaTime;

        gameObject.transform.position += Camera.main.transform.forward * movementForward +
                                          Camera.main.transform.right * movementRight;
    }
}
