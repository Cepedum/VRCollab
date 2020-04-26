using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    [SerializeField]
    private XRNode controllerNode;
    private InputDevice controller;

    [SerializeField]
    private GameObject avatar;
    private Animator handAnimator;

    private string triggerVariable;
    private string gripVariable;

    private void GetDevices()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
        controller = devices.FirstOrDefault();
    }

    // Start is called before the first frame update
    void Start()
    {       
        GetDevices();
        handAnimator = avatar.GetComponent<Animator>();

        if (controllerNode == XRNode.LeftHand)
        {
            triggerVariable = "triggerLeft";
            gripVariable = "gripLeft";
        }
        else if (controllerNode == XRNode.RightHand)
        {
            triggerVariable = "triggerRight";
            gripVariable = "gripRight";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            GetDevices();
        }
        else
        {
            UpdateHandAnimation();
        }
    }

    private void UpdateHandAnimation()
    {
        if (controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat(triggerVariable, triggerValue);
        }
        else
        {
            handAnimator.SetFloat(triggerVariable, 0);
        }

        if (controller.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat(gripVariable, gripValue);
        }
        else
        {
            handAnimator.SetFloat(gripVariable, 0);
        }
    }
}
