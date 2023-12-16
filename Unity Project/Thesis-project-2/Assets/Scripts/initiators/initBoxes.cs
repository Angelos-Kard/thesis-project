using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(initCastAndBoxesHandler))]
public class initBoxes : MonoBehaviour
{
    [HideInInspector()]
    public float userHeight = 0.0f;
    private float userWidth = 0.0f;
    private variablesAggregator variableAggInstance;
    private bool enabledContinuousMode;
    private GameObject[] raycastBoxes;
    private int boxesPerRow;
    private int numberOfRows;

    // Start is called before the first frame update
    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
        userHeight = variableAggInstance.userHeight;
        userWidth = variableAggInstance.userWidth;
        raycastBoxes = variableAggInstance.raycastBoxes;
        enabledContinuousMode = variableAggInstance.enableContinuousModeGlobal;
        boxesPerRow = variableAggInstance.boxesPerRow;
        numberOfRows = variableAggInstance.numberOfRows;

        if (userHeight <= 0)
        {
            userHeight = 1.8f;
        }

        // For some reason, 1 unit = 0.7cm
        // userHeight -= 0.30f;

        switch (enabledContinuousMode)
        {
            // Scan Mode
            case false:
                this.positionAndScaleBoxes(raycastBoxes, 3, 3, userHeight);
                break;
            // Continuous Mode
            case true:
                GameObject[] castBoxesToUse = this.findCastBoxesWithContinuousEnabled(raycastBoxes);
                this.positionAndScaleBoxes(castBoxesToUse, boxesPerRow, numberOfRows, userHeight);
                break;
        }

    }

    /// <summary>
    /// A method which places the castBoxes and their alert boxes in the correct position based on the parameters provided.
    /// </summary>
    /// <param name="raycstBoxesToPosition">An array of the castpoxes</param>
    /// <param name="boxesPerRow">The number of castBoxes which will be placed in each row</param>
    /// <param name="numberOfRows">The number of rows, in which the cast boxes will be placed</param>
    /// <param name="userHeight">The height of the user in meters</param>
    public void positionAndScaleBoxes
    (
        GameObject[] raycstBoxesToPosition,
        int boxesPerRow,
        int numberOfRows,
        float userHeight
    )
    {
        float boxLength = userWidth / boxesPerRow;
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

            raycstBoxesToPosition[i].transform.position = new Vector3(boxPlacementX, boxPlacementY, -0.1f);
            raycstBoxesToPosition[i].transform.localScale = new Vector3(boxLength, boxHeight, boxLength);

            raycstBoxesToPosition[i].GetComponent<initSingleBox>().alertBox.transform.localScale = new Vector3(boxLength, boxHeight, 0.1f);

            boxPlacementX += boxLength * (boxesPerRow - 1);
        }
    }

    /// <summary>
    /// In an array of RaycastHit (<paramref name="hitPointsList"/>), it locates the index of the hitPoint, ehose distance is closer to the user.
    /// </summary>
    /// <param name="hitPointsList">An array of hitPoints</param>
    /// <returns>It returns the index of the hitPoint closer to the user</returns>
    public int findClosestHitPointIndex(RaycastHit[] hitPointsList)
    {
        int closestHitPointIndex = -1;
        float minDistance = 999.0f;
        List<Vector3> relPosList = new List<Vector3>();
        for (int i = 0; i < hitPointsList.Length; i++)
        {
            if (hitPointsList[i].distance != 0.0f && hitPointsList[i].collider != null)
            {
                Vector3 relativePosition = variableAggInstance.userPosition.transform.InverseTransformPoint(hitPointsList[i].point);
                relPosList.Add(relativePosition);
                if (hitPointsList[i].distance < minDistance)
                {
                    minDistance = hitPointsList[i].distance;
                    closestHitPointIndex = i;
                }
            }
        }

        return closestHitPointIndex;
    }

    public void CalculateDeadZones()
    {

    }

    /// <summary>
    ///     Finds the castBoxes for which the continuous mode has been enabled,.
    /// </summary>
    /// <param name="allCastBoxes">An array of all the castBoxes in the scene</param>
    /// <returns>The castBoxes that meet the criteria</returns>
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

    /// <summary>
    /// Places the alert box of a <paramref name="castBox"/> in the position of the <paramref name="hitPoint"/>.
    /// </summary>
    /// <param name="castBox">The castBox whose alert box will be transformed</param>
    /// <param name="hitPoint">The hitPoint to which the alert box will be placed</param>
    public void placeAlertBox(GameObject castBox, RaycastHit hitPoint)
    {
        castBox.GetComponent<initSingleBox>().alertBox.SetActive(true);
        if (!castBox.GetComponent<initSingleBox>().alertBox.GetComponent<AudioSource>().isPlaying)
            castBox.GetComponent<initSingleBox>().alertBox.GetComponent<AudioSource>().Play();
        castBox.GetComponent<initSingleBox>().alertBox.transform.position = hitPoint.point;
        castBox.GetComponent<initSingleBox>().alertBox.transform.rotation = castBox.transform.rotation;
    }

    /// <summary>
    /// Places an <paramref name="alertBox"/> in the positiom of a <paramref name="hitPoint"/>.
    /// </summary>
    /// <param name="alertBox">The alert box which will be transformed</param>
    /// <param name="castBox">The castBox whose rotation will be used</param>
    /// <param name="hitPoint">The hitPoint to which the alert box will be placed</param>
    public void placeAlertBox(GameObject alertBox, GameObject castBox, RaycastHit hitPoint, bool placeAlertBoxToEarLevel)
    {
        alertBox.SetActive(true);
        var alertTrans = alertBox.transform;
        if (!alertBox.GetComponent<AudioSource>().isPlaying)
            alertBox.GetComponent<AudioSource>().Play();

        //*
        var hitPointVec = hitPoint.point;
        var userPositionTrans = variableAggInstance.userPosition.transform;
        if (variableAggInstance.placeAlertBoxAtEyeLevel)
        {
            alertTrans.position = new Vector3(hitPointVec.x, userPositionTrans.position.y, hitPointVec.z);
        }
        else
        {
            alertTrans.position = hitPointVec;
        }
        alertBox.transform.rotation = castBox.transform.rotation;
        //*/
    }

    public void placePeripheralAlertBox(GameObject alertBox, RaycastHit hitPoint)
    {
        alertBox.SetActive(true);
        if (!alertBox.GetComponent<AudioSource>().isPlaying)
        {
            alertBox.GetComponent<AudioSource>().Play();
        }
        Vector3 relativePosition = variableAggInstance.userPosition.transform.InverseTransformPoint(hitPoint.point);
        alertBox.transform.position = new Vector3(
            variableAggInstance.userPosition.transform.position.x + /*Normalize x value*/ (relativePosition.x),
            variableAggInstance.userPosition.transform.position.y + relativePosition.y,
            variableAggInstance.userPosition.transform.position.z
        );
    }

    /// <summary>
    /// Deactivates all the alert boxes, except the one, whose castBox casted and hit the point closest to the user
    /// </summary>
    /// <param name="castBoxesArray">The array of castBoxes</param>
    /// <param name="castHitPointIndex">The index of the castBox, whoe cast hit the point closest to the user</param>
    public void deactivateAlertBoxesExceptTheClosest(GameObject[] castBoxesArray, int castHitPointIndex)
    {
        for (int i = 0; i < castBoxesArray.Length; i++)
        {
            if (i != castHitPointIndex)
            {
                castBoxesArray[i].GetComponent<initSingleBox>().alertBox.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Deactivates all the alert boxes, based on an array of castBoxes
    /// </summary>
    /// <param name="castBoxesArray">The array of castBoxes, whose alert boxes will be deactivated</param>
    public void deactivateAlertBox(GameObject[] castBoxesArray)
    {
        for (int i = 0; i < castBoxesArray.Length; i++)
        {
            if (castBoxesArray[i].GetComponent<initSingleBox>().alertBox.GetComponent<AudioSource>().isPlaying)
                castBoxesArray[i].GetComponent<initSingleBox>().alertBox.GetComponent<AudioSource>().Pause();
        }
    }

    /// <summary>
    /// Deactivates an alert box
    /// </summary>
    /// <param name="alertBox">The alert box which will be deactivated</param>
    public void deactivateAlertBox(GameObject alertBox)
    {
        if (alertBox.GetComponent<AudioSource>().isPlaying)
            alertBox.GetComponent<AudioSource>().Pause();
    }
}
