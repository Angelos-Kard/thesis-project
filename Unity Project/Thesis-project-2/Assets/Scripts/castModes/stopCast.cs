using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initCastAndBoxesHandler)), RequireComponent(typeof(continuousCast))]
public class stopCast : MonoBehaviour
{
    private variablesAggregator variableAggInstance;

    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
    }
    public void EnableStopMode()
    {
        if (variableAggInstance.enableContinuousModeGlobal == true)
        {
            this.GetComponent<continuousCast>().continuousModeGlobalToggle();
        }

        GameObject[] allCastBoxes = variableAggInstance.raycastBoxes;
        this.GetComponent<initBoxes>().deactivateAlertBox(allCastBoxes);
        this.GetComponent<initBoxes>().deactivateAlertBox(variableAggInstance.commonAlertBox);
        /*for (int i = 0; i < allCastBoxes.Length; i++)
        {
            if (allCastBoxes[i].GetComponent<initSingleBox>().alertBox.activeSelf)
            {
                allCastBoxes[i].GetComponent<initSingleBox>().alertBox.SetActive(false);
            }
        }*/
    }
}
