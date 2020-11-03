using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour
{

    public Camera cam;

    private Vector3 _mousePosition;


    void Update(){
        _mousePosition = cam.ScreenToWorldPoint( Input.mousePosition);
    }

    void FixedUpdate()
    {

        Vector2 dir = _mousePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        string ObjectName = ( dir.x > 0f )? "RightArm":"LeftArm";

        Quaternion rotation = Quaternion.Euler(0, 0, angle +((name == ObjectName )? 90f : -90f) );
        transform.rotation = rotation;

    }
}
