using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialPosition : MonoBehaviour
{

    public Transform avatar;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = avatar.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // El offset se debe actualizar en el primer frame, ya que es cuando cambia la posicion del avatar segun el HMD
        // De esta forma, da igual la posicion inicial 
        transform.position = transform.position + new Vector3(0, initialPosition.y - avatar.position.y, 0);
        enabled = false;
    }
}
