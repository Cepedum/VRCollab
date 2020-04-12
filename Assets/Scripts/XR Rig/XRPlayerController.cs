using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class XRPlayerController : MonoBehaviour
{
    [Header("Behaviour Options")]

    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private XRNode controllerNode = XRNode.LeftHand;

    private InputDevice controller;

    private List<InputDevice> devices = new List<InputDevice>();

    private Vector3 xMove;
    private Vector3 zMove;

    // Start is called before the first frame update
    void Start()
    {
        GetDevice();
    }

    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
        controller = devices.FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null){
            GetDevice();
        }

        CalculateMovement();
    }

    private void CalculateMovement()
    {
        Vector2 primary2DValue;

        InputFeatureUsage<Vector2> primary2DVector = CommonUsages.primary2DAxis;

        if (controller.TryGetFeatureValue(primary2DVector, out primary2DValue) && primary2DValue != Vector2.zero)
        {
            Vector3 right = transform.TransformDirection(Vector3.right);
            xMove = primary2DValue.x * right;

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            zMove = primary2DValue.y * forward;
        }
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector3 movement = (xMove + zMove) * speed * Time.deltaTime;
        transform.position = transform.position + movement;
    }
}
