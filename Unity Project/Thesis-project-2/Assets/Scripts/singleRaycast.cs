using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initSingleBox))]
public class singleRaycast : MonoBehaviour
{
    private bool m_HitDetect;
    private RaycastHit hitInfo;

    /// <summary>
    ///     Casts a raycast or boxcast from the position of a castBox and detects a hitPoint.
    /// </summary>
    /// <param name="variableAggInstance">The variable aggregator GameObjetc</param>
    /// <returns>
    ///     The hitPoint of the cast
    /// </returns>
    public RaycastHit SingleRaycastFunc(variablesAggregator variableAggInstance) {
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
}