using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initSingleBox))]
public class singleRaycast : MonoBehaviour
{
    public GameObject variableAggregatorObject;
    /*public GameObject handlerObject;*/
    private variablesAggregator variableAggInstance;

    private bool m_HitDetect;
    private RaycastHit hitInfo;

    public void Start()
    {
        variableAggInstance = variableAggregatorObject.GetComponent<variablesAggregator>();
    }

    public void SingleRaycastFunc()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward); // forward direction

        float maxDistance = variableAggInstance.maxDistance;

        variablesAggregator.CastTypeEnum castType = variableAggInstance.castType;

        switch (castType) {
            case variablesAggregator.CastTypeEnum.RayCast:
                m_HitDetect = Physics.Raycast(transform.position, fwd, out hitInfo, 5.0f);
                break;

            case variablesAggregator.CastTypeEnum.BoxCast:
                m_HitDetect = Physics.BoxCast(transform.position, transform.localScale, fwd, out hitInfo, transform.rotation, maxDistance);
                break;
        }


        if (m_HitDetect)
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.yellow);
            print(this.name + ": Found an object - distance: " + hitInfo.distance);
            this.GetComponent<initSingleBox>().alertBox.SetActive(true);
            this.GetComponent<initSingleBox>().alertBox.transform.position = hitInfo.point;
            this.GetComponent<initSingleBox>().alertBox.transform.rotation = this.transform.rotation;
        }
        else
        {
            this.GetComponent<initSingleBox>().alertBox.SetActive(false);
            print(this.name + ": No object was hit");
        }
    }

    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float m_MaxDistance = handlerObject.GetComponent<positionRaycasts>().maxDistance;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * hitInfo.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * hitInfo.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        }
    }
    //*/
}
