using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initCastAndBoxesHandler)), RequireComponent(typeof(initBoxes))]
public class scanCast : MonoBehaviour
{
    private variablesAggregator variableAggInstance;

    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
    }

    public void EnableScanMode()
    {
        GameObject[] allCastBoxes = variableAggInstance.raycastBoxes;
        List<RaycastHit> castHitPointsList = new List<RaycastHit>();

        for (int i = 0; i < allCastBoxes.Length; i++)
        {
            RaycastHit hitPoint = allCastBoxes[i].GetComponent<singleRaycast>().SingleRaycastFunc(variableAggInstance);
            castHitPointsList.Add(hitPoint);
        }

        //ToArray doesn't change the order of the elements
        int hitPointIndex = this.GetComponent<initBoxes>().findClosestHitPointIndex(castHitPointsList.ToArray());
        if (hitPointIndex != -1)
        {
            this.GetComponent<initBoxes>().placeAlertBox(variableAggInstance.commonAlertBox, allCastBoxes[hitPointIndex], castHitPointsList[hitPointIndex], variableAggInstance.placeAlertBoxAtEyeLevel);
        }
        else
        {
            switch (variableAggInstance.alertBoxType)
            {
                case variablesAggregator.AlertBoxTypeEnum.BoxSpecific:
                    // Use the alert box of each castBox
                    this.GetComponent<initBoxes>().deactivateAlertBox(variableAggInstance.alertBoxes);
                    break;

                case variablesAggregator.AlertBoxTypeEnum.Common:
                    // Use a common alert for all castBoxes
                    this.GetComponent<initBoxes>().deactivateAlertBox(variableAggInstance.commonAlertBox);
                    break;

                case variablesAggregator.AlertBoxTypeEnum.Peripheral:
                    this.GetComponent<initBoxes>().deactivateAlertBox(variableAggInstance.peripheralAlertBox);
                    break;
            }
        }
    }
}
