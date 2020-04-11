using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    private Vector3 fixedRotation;

    // Start is called before the first frame update
    void Start()
    {
        fixedRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = fixedRotation;
    }
}
