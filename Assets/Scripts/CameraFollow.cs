using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset ;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desirePosition = Target.position + offset;
        //Vector3 smoothPosition = Vector3.Lerp(transform.position,desirePosition,smoothSpeed);
        transform.position = desirePosition;// smoothPosition;

       // transform.LookAt(Target); 
    }
}
