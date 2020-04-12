using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFloorOffset : MonoBehaviour
{
    public Transform avatar;
    private Vector3 initialPosition;
    private float minHeight;
    public float maxHeightOffset;

    private SetInitialPosition setInitialPosition;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = avatar.position;
        minHeight = avatar.position.y - maxHeightOffset;
        setInitialPosition = GetComponent<SetInitialPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!setInitialPosition.enabled){
            if (avatar.position.y > initialPosition.y)
            {
                transform.position = transform.position + new Vector3(0, initialPosition.y - avatar.position.y, 0);
            }
            else if (avatar.position.y < minHeight)
            {
                transform.position = transform.position + new Vector3(0, minHeight - avatar.position.y, 0);
            }
        }
    }
}
