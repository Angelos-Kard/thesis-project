using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleRaycast : MonoBehaviour
{
    [Tooltip("The object which will operate as an alert box for the specific raycast.")]
    public GameObject alertBox;

    public void singleRaycastFunc()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward); // forward direction

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, fwd, out hitInfo, 10.0f))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.yellow);
            print(this.name + ": Found an object - distance: " + hitInfo.distance);
            alertBox.SetActive(true);
            alertBox.transform.position = hitInfo.point;
        }
        else
        {
            alertBox.SetActive(false);
            print(this.name + ": No object was hit");
        }
    }
}