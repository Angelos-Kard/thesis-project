using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startRaycasting : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward); // forward direction

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, fwd, out hitInfo, 10.0f))
        {
            print("Found an object - distance: " + hitInfo.distance);
        } else
        {
            print("No object was hit");
        }
    }
}
