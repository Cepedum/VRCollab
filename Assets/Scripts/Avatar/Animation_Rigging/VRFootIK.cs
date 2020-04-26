using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{

    private Animator animator;
    private int onAnimatorIKCounter = 0;
    public Vector3 footOffset;

    [Range(0,1)]
    public float rightFootPosWeight = 1;
    [Range(0, 1)]
    public float rightFootRotWeight = 1;
    [Range(0, 1)]
    public float leftFootPosWeight = 1;
    [Range(0, 1)]
    public float leftFootRotWeight = 1;


    public Transform rightFootEnd;
    public Transform leftFootEnd;
    private float rightFootVerticalOffset;
    private float leftFootVerticalOffset;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        RaycastHit hit;
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        Vector3 rightFootEndOffset = Vector3.zero;
        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        Vector3 leftFootEndOffset = Vector3.zero;


        if (onAnimatorIKCounter == 0)
        {
            rightFootVerticalOffset = rightFootPos.y - rightFootEnd.position.y;
            leftFootVerticalOffset = leftFootPos.y - leftFootEnd.position.y;
            onAnimatorIKCounter++;
        }

        // Evita que el pie derecho atraviese el suelo
        bool hasHit = Physics.Raycast(rightFootPos + transform.TransformDirection(Vector3.up), transform.TransformDirection(Vector3.down), out hit,2.0f);
        bool hasHitAbove = Physics.Raycast(transform.TransformPoint(rightFootPos + new Vector3(0, -0.15f, 0)), transform.TransformDirection(Vector3.down), 2.0f);

        if (hasHit && !hasHitAbove)
        {
            // Este offset se calcula para evitar que la puntera atraviese el suelo con la animacion de andar
            float actualLeftFootVerticalOffset = leftFootPos.y - leftFootEnd.position.y;
            rightFootEndOffset = calculateVerticalFootOffset(actualLeftFootVerticalOffset, leftFootVerticalOffset);

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset + rightFootEndOffset);

            //Quaternion rightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            //animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);
        }
        else {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
        }


        // Evita que el pie izquierdo atraviese el suelo
        hasHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit, 2.0f);
        hasHitAbove = Physics.Raycast(transform.TransformPoint(leftFootPos + new Vector3(0, -0.15f, 0)), transform.TransformDirection(Vector3.down), 2.0f);

        if (hasHit && !hasHitAbove)
        {
            // Este offset se calcula para evitar que la puntera atraviese el suelo con la animacion de andar
            float actualRightFootVerticalOffset = rightFootPos.y - rightFootEnd.position.y;
            rightFootEndOffset = calculateVerticalFootOffset(actualRightFootVerticalOffset, rightFootVerticalOffset);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);

            //Quaternion leftFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            //animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
    
    private Vector3 calculateVerticalFootOffset(float actualRightFootVerticalOffset, float rightFootVerticalOffset)
    {
        if (actualRightFootVerticalOffset > rightFootVerticalOffset)
        {
            return new Vector3(0, actualRightFootVerticalOffset - rightFootVerticalOffset, 0);
        }
        else
        {
            return Vector3.zero;
        }
    }
    
}
