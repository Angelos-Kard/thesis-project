using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(initCastAndBoxesHandler))]
public class initBoxes : MonoBehaviour
{
    public float userHeight = 0.0f;
    private variablesAggregator variableAggInstance;
    private variablesAggregator.CastModeEnum castMode;
    private GameObject[] raycastBoxes;
    private int boxesPerRow;
    private int numberOfRows;

    // Start is called before the first frame update
    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
        userHeight = variableAggInstance.userHeight;
        raycastBoxes = variableAggInstance.raycastBoxes;
        castMode = variableAggInstance.enabledModeGlobal;
        boxesPerRow = variableAggInstance.boxesPerRow;
        numberOfRows = variableAggInstance.numberOfRows;

        if (userHeight <= 0)
        {
            userHeight = 1.8f;
        }

        // For some reason, 1 unit = 0.7cm
        userHeight -= 0.30f;

        // Scan Mode
        switch (castMode)
        {
            case variablesAggregator.CastModeEnum.ScanMode:
                this.positionAndScaleBoxes(raycastBoxes, 3, 3, userHeight);
                break;
            case variablesAggregator.CastModeEnum.ContinuousMode:
                GameObject[] castBoxesToUse = this.findCastBoxesWithContinuousEnabled(raycastBoxes);
                this.positionAndScaleBoxes(castBoxesToUse, boxesPerRow, numberOfRows, userHeight);
                break;
        }

    }

    public void positionAndScaleBoxes
    (
        GameObject[] raycstBoxesToPosition,
        int boxesPerRow,
        int numberOfRows,
        float userHeight
    )
    {
        float boxLength = userHeight / boxesPerRow;
        float boxHeight = userHeight / numberOfRows;

        float boxPlacementX = -boxLength * (boxesPerRow - 1);
        float boxPlacementY = 0.0f;
        for (int i = 0; i < raycstBoxesToPosition.Length; i++)
        {
            if (i % boxesPerRow == 0)
            {
                boxPlacementX = -boxLength * (boxesPerRow - 1);

                if (i != 0) boxPlacementY -= boxHeight;
            }

            raycstBoxesToPosition[i].transform.position = new Vector3(boxPlacementX, boxPlacementY, 0.0f);
            raycstBoxesToPosition[i].transform.localScale = new Vector3(boxLength, boxHeight, 0.1f);

            raycstBoxesToPosition[i].GetComponent<initSingleBox>().alertBox.transform.localScale = new Vector3(boxLength, boxHeight, 0.1f);

            boxPlacementX += boxLength;
        }
    }

    public GameObject[] findCastBoxesWithContinuousEnabled(GameObject[] allCastBoxes)
    {
        List<GameObject> castBoxesWithContinous = new List<GameObject>();

        for (int i = 0; i < allCastBoxes.Length; i++)
        {
            if (allCastBoxes[i].GetComponent<initSingleBox>().continuousRaycast == true)
            {
                castBoxesWithContinous.Add(allCastBoxes[i]);
            }
        }

        return castBoxesWithContinous.ToArray();
    }
}
