using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initCastAndBoxesHandler))]
public class scanCast : MonoBehaviour
{
    private variablesAggregator variableAggInstance;

    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
    }

    // Update is called once per frame
    public void EnableScanMode()
    {
        GameObject[] allCastBoxes = variableAggInstance.raycastBoxes;

        for (int i = 0; i < allCastBoxes.Length; i++)
        {
            allCastBoxes[i].GetComponent<singleRaycast>().SingleRaycastFunc();
        }
    }
}
