using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initCastAndBoxesHandler)), RequireComponent(typeof(initBoxes))]
public class scanCast : MonoBehaviour {
    private variablesAggregator variableAggInstance;

    void Start() {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
    }

    public void EnableScanMode() {

        checkCurrentMode();
        
        GameObject[] allCastBoxes = variableAggInstance.raycastBoxes;
        List<RaycastHit> castHitPointsList = new List<RaycastHit>();

        for (int i = 0; i < allCastBoxes.Length; i++) {
            RaycastHit hitPoint = allCastBoxes[i].GetComponent<singleRaycast>().SingleRaycastFunc(variableAggInstance);
            castHitPointsList.Add(hitPoint);
        }

        //ToArray doesn't change the order of the elements
        int hitPointIndex = this.GetComponent<initBoxes>().findClosestHitPointIndex(castHitPointsList.ToArray());
        if (hitPointIndex != -1) {
            switch (variableAggInstance.alertBoxType) {
                case variablesAggregator.AlertBoxTypeEnum.BoxSpecific:
                    // Use the alert box of each castBox
                    break;

                case variablesAggregator.AlertBoxTypeEnum.Common:
                    // Use a common alert for all castBoxes
                    this.GetComponent<initBoxes>().placeAlertBox(variableAggInstance.commonAlertBox, allCastBoxes[hitPointIndex], castHitPointsList[hitPointIndex]);
                    break;
            }
        } else {
            switch (variableAggInstance.alertBoxType) {
                case variablesAggregator.AlertBoxTypeEnum.BoxSpecific:
                    // Use the alert box of each castBox
                    this.GetComponent<initBoxes>().deactivateAlertBox(variableAggInstance.raycastBoxes);
                    break;

                case variablesAggregator.AlertBoxTypeEnum.Common:
                    // Use a common alert for all castBoxes
                    this.GetComponent<initBoxes>().deactivateAlertBox(variableAggInstance.commonAlertBox);
                    break;
            }
        }
    }

    public void checkCurrentMode() {
        variablesAggregator.CastModeEnum previousMode = variableAggInstance.enabledModeGlobal;

        if (previousMode != variablesAggregator.CastModeEnum.ScanMode) {
            variableAggInstance.enabledModeGlobal = variablesAggregator.CastModeEnum.ScanMode;
            this.GetComponent<initBoxes>().resetStatus();
        }
    }
}
