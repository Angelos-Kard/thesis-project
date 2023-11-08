using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initBoxes))]
public class continuousCast : MonoBehaviour
{
    private variablesAggregator variableAggInstance;
    private bool enableContinuousModeGlobal;
    private GameObject[] castBoxesWithContinuous;

    // Start is called before the first frame update
    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
        enableContinuousModeGlobal = variableAggInstance.enableContinuousModeGlobal;
        castBoxesWithContinuous = this.GetComponent<initBoxes>().findCastBoxesWithContinuousEnabled(variableAggInstance.raycastBoxes);
    }

    // Update is called once per frame
    void Update()
    {
        if (enableContinuousModeGlobal)
        {
            for (int i = 0; i < castBoxesWithContinuous.Length; i++)
            {
                castBoxesWithContinuous[i].GetComponent<singleRaycast>().SingleRaycastFunc();
            }
        }
    }

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
            for (int i = 0; i < castBoxesWithContinuous.Length; i++)
            {
                if (castBoxesWithContinuous[i].GetComponent<initSingleBox>().alertBox.activeSelf == true)
                {
                    castBoxesWithContinuous[i].GetComponent<initSingleBox>().alertBox.SetActive(false);
                }
            }
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
