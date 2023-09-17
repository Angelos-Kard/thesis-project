using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleRaycast : MonoBehaviour
{
    [Tooltip("The object which will operate as an alert box for the specific raycast.")]
    public GameObject alertBox;
    [Tooltip("If it set to 'True', rays will be casted in every frame.")]
    public bool continuousRaycast = false;

    public void FixedUpdate()
    {
        if (continuousRaycast)
        {
            SingleRaycastFunc();
        }
    }

    public void ContinuousModeToggle()
    {
        continuousRaycast = !continuousRaycast;
    }
    public void SingleRaycastFunc()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward); // forward direction

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, fwd, out hitInfo, 5.0f))
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
