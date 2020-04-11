using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    public float speedTreshold = 0.1f;
    [Range(0,1)]
    public float smoothing = 1;
    private Animator animator;
    private Vector3 previousPos;
    private Vector3 previousRot;
    private VRRig vrRig;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRRig>();
        previousPos = vrRig.head.vrTarget.position;
        previousRot = vrRig.head.vrTarget.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        // Compute the speed
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
        // Local Speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        // Compute the rotation
        float headsetYRotationSpeed = (vrRig.head.vrTarget.eulerAngles.y - previousRot.y) / Time.deltaTime;
        previousRot = vrRig.head.vrTarget.eulerAngles;

        // Set Animator Values
        float previousDirectionX = animator.GetFloat("directionX");
        float previousDirectionY = animator.GetFloat("directionY");
        float previousRotation = animator.GetFloat("rotation");
        animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedTreshold);
        animator.SetFloat("directionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        animator.SetFloat("directionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
        //animator.SetFloat("rotation", Mathf.Lerp(previousRotation, Mathf.Clamp(headsetYRotationSpeed, -1, 1), smoothing));


    }
}
