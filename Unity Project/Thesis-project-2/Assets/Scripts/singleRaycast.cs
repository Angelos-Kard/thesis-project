using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initSingleBox))]
public class singleRaycast : MonoBehaviour
{
    // public GameObject variableAggregatorObject;
    /*public GameObject handlerObject;*/
    // private variablesAggregator variableAggInstance;

    private bool m_HitDetect;
    private RaycastHit hitInfo;

    public void Start()
    {
        // variableAggInstance = variableAggregatorObject.GetComponent<variablesAggregator>();
    }

    /// <summary>
    ///     Castes a raycast or boxcast from the position of a castBox and detects a hitPoint.
    /// </summary>
    /// <param name="variableAggInstance">The variable aggregator GameObjetc</param>
    /// <returns>
    ///     The hitPoint of the cast
    /// </returns>
    public RaycastHit SingleRaycastFunc(variablesAggregator variableAggInstance)
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

        return hitInfo;        
    }

    //*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

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
            // Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            // Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        }
    }
    //*/
}
