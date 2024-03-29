using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(initCastAndBoxesHandler)), RequireComponent(typeof(initBoxes)),
    RequireComponent(typeof(initFlashHandler)), RequireComponent(typeof(initRumbleHandler))]
public class continuousCast : MonoBehaviour
{
    private variablesAggregator variableAggInstance;
    private bool enableContinuousModeGlobal;
    private GameObject[] castBoxesWithContinuous;
    private initFlashHandler flashHandler;

    // Start is called before the first frame update
    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
        enableContinuousModeGlobal = variableAggInstance.enableContinuousModeGlobal;
        castBoxesWithContinuous = this.GetComponent<initBoxes>().findCastBoxesWithContinuousEnabled(variableAggInstance.raycastBoxes);
        flashHandler = this.GetComponent<initFlashHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enableContinuousModeGlobal)
        {
            List<RaycastHit> hitPointsList = new List<RaycastHit>();

            for (int i = 0; i < castBoxesWithContinuous.Length; i++)
            {
                RaycastHit hitPoint = castBoxesWithContinuous[i].GetComponent<singleRaycast>().SingleRaycastFunc(variableAggInstance);
                hitPointsList.Add(hitPoint);
            }
            int hitPointIndex = this.GetComponent<initBoxes>().findClosestHitPointIndex(hitPointsList.ToArray());
            if (hitPointIndex != -1)
            {
                switch (variableAggInstance.alertBoxType)
                {
                    case variablesAggregator.AlertBoxTypeEnum.BoxSpecific:
                        // Use the alert box of each castBox
                        this.GetComponent<initBoxes>().placeAlertBox(castBoxesWithContinuous[hitPointIndex], hitPointsList[hitPointIndex]);
                        break;

                    case variablesAggregator.AlertBoxTypeEnum.Common:
                        // Use a common alert for all castBoxes
                        this.GetComponent<initBoxes>().placeAlertBox(variableAggInstance.commonAlertBox, castBoxesWithContinuous[hitPointIndex], hitPointsList[hitPointIndex], variableAggInstance.placeAlertBoxAtEyeLevel);
                        break;

                    case variablesAggregator.AlertBoxTypeEnum.Peripheral:
                        this.GetComponent<initBoxes>().placePeripheralAlertBox(variableAggInstance.peripheralAlertBox, hitPointsList[hitPointIndex]);
                        break;
                }
                
                // Debug.Log(hitPointsList[hitPointIndex].distance);
                print(hitPointIndex + ": " + hitPointsList[hitPointIndex].point);

                if (variableAggInstance.activateRumble)
                {
                    float motorSpeed = this.GetComponent<initRumbleHandler>().FindMotorsLowAndHigh(hitPointsList[hitPointIndex].distance);
                    float stopFreq = this.GetComponent<initRumbleHandler>().GetStopFrequency(hitPointsList[hitPointIndex].distance);
                    Dictionary<string, float> motorSpeedDict = this.GetComponent<initRumbleHandler>().GetSpeedOfEachMotor(hitPointsList[hitPointIndex], motorSpeed);

                    print("Motor speed: " + motorSpeed);
                    if (motorSpeed > 0.2f)
                    {
                        this.GetComponent<initRumbleHandler>().RumblePulse(motorSpeedDict["low"], motorSpeedDict["high"], stopFreq, 0.5f);
                    }
                    else
                    {
                        this.GetComponent<initRumbleHandler>().StopRumble();
                    }
                }


                //*
                if (variableAggInstance.activateFlash)
                {
                    if (hitPointsList[hitPointIndex].distance <= variableAggInstance.distanceThreadToActivate)
                    {
                        variablesAggregator.lightPositionEnum[] lightPositionsToStart = flashHandler.FindHitPointPosition(hitPointsList[hitPointIndex]);
                        Color colorToUse = flashHandler.FindColorBasedOnDistance(hitPointsList[hitPointIndex].distance);
                        flashHandler.StartMultipleFlashes(lightPositionsToStart, colorToUse);
                    } else
                    {
                        flashHandler.StopFlashes();
                    }
                }
                //*/

            } else
            {
                this.GetComponent<initRumbleHandler>().StopRumble();
                switch (variableAggInstance.alertBoxType)
                {
                    case variablesAggregator.AlertBoxTypeEnum.BoxSpecific:
                        // Use the alert box of each castBox
                        this.GetComponent<initBoxes>().deactivateAlertBox(castBoxesWithContinuous);
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

    /// <summary>
    ///     Toggles the continuous mode on or off
    /// </summary>
    /// <remarks>
    ///     The function is called with the voice input <c>"Continuous Mode"</c>
    /// </remarks>
    public void continuousModeGlobalToggle()
    {
        enableContinuousModeGlobal = !enableContinuousModeGlobal;

        if (enableContinuousModeGlobal == true)
        {
            // Position of Continuous Mode
            castBoxesWithContinuous = this.GetComponent<initBoxes>().findCastBoxesWithContinuousEnabled(variableAggInstance.raycastBoxes);
            this.GetComponent<initBoxes>().positionAndScaleBoxes(
                castBoxesWithContinuous,
                variableAggInstance.boxesPerRow,
                variableAggInstance.numberOfRows,
                this.GetComponent<initBoxes>().userHeight
            );
        } else
        {
            // Disable alert boxes of cast boxes in continous mode
            this.GetComponent<initBoxes>().deactivateAlertBox(castBoxesWithContinuous);
            this.GetComponent<initBoxes>().deactivateAlertBox(variableAggInstance.commonAlertBox);
            /*for (int i = 0; i < castBoxesWithContinuous.Length; i++)
            {
                if (castBoxesWithContinuous[i].GetComponent<initSingleBox>().alertBox.activeSelf == true)
                {
                    castBoxesWithContinuous[i].GetComponent<initSingleBox>().alertBox.SetActive(false);
                }
            }*/

            // Position of Scan Mode
            this.GetComponent<initBoxes>().positionAndScaleBoxes(
                variableAggInstance.raycastBoxes,
                3,
                3,
                this.GetComponent<initBoxes>().userHeight
            );
        }

    }
}
