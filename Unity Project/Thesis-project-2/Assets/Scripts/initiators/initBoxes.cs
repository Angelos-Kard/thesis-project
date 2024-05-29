using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(initCastAndBoxesHandler)), RequireComponent(typeof(initRumbleHandler)), RequireComponent(typeof(initFlashHandler))]
public class initBoxes : MonoBehaviour {
    [HideInInspector()]
    public float userHeight = 0.0f;
    private float userWidth = 0.0f;
    private variablesAggregator variableAggInstance;
    private GameObject[] raycastBoxes;
    private int boxesPerRow;
    private int numberOfRows;

    void Start() {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
        userHeight = variableAggInstance.userHeight;
        userWidth = variableAggInstance.userWidth;
        raycastBoxes = variableAggInstance.raycastBoxes;
        boxesPerRow = variableAggInstance.boxesPerRow;
        numberOfRows = variableAggInstance.numberOfRows;

        if (userHeight <= 0) {
            userHeight = 1.8f;
        }

        initializePosition();
    }

    /// <summary>
    /// Based on the mode that has been selected, the cast boxes are placed and activated accordingly
    /// </summary>
    public void initializePosition() {
        GameObject[] castBoxesToUse;
        switch (variableAggInstance.enabledModeGlobal) {
            // Scan Mode
            case variablesAggregator.CastModeEnum.ScanMode:
                this.positionAndScaleBoxes(raycastBoxes, 3, 3, userHeight);
                break;
            // Continuous Mode
            case variablesAggregator.CastModeEnum.ContinuousMode:
                castBoxesToUse = this.findCastBoxesWithContinuousEnabled(raycastBoxes);
                this.positionAndScaleBoxes(castBoxesToUse, boxesPerRow, numberOfRows, userHeight);
                break;
            case variablesAggregator.CastModeEnum.StopMode:
                castBoxesToUse = this.findCastBoxesWithContinuousEnabled(raycastBoxes);
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
    ) {
        float boxLength = userWidth / boxesPerRow;
        float boxHeight = userHeight / numberOfRows;

        float boxPlacementX = -boxLength * (boxesPerRow - 1);
        float boxPlacementY = 0.0f;
        for (int i = 0; i < raycstBoxesToPosition.Length; i++) {
            if (i % boxesPerRow == 0) {
                boxPlacementX = -boxLength * (boxesPerRow - 1);

                if (i != 0) boxPlacementY -= boxHeight;
            }

            raycstBoxesToPosition[i].transform.position = new Vector3(boxPlacementX, boxPlacementY, -0.1f);
            raycstBoxesToPosition[i].transform.localScale = new Vector3(boxLength, boxHeight, boxLength);

            raycstBoxesToPosition[i].GetComponent<initSingleBox>().alertBox.transform.localScale = new Vector3(boxLength, boxHeight, 0.1f);

            raycstBoxesToPosition[i].SetActive(true);

            boxPlacementX += boxLength * (boxesPerRow - 1);
        }
    }

    /// <summary>
    /// In an array of RaycastHit (<paramref name="hitPointsList"/>), it locates the index of the hitPoint, ehose distance is closer to the user.
    /// </summary>
    /// <param name="hitPointsList">An array of hitPoints</param>
    /// <returns>It returns the index of the hitPoint closer to the user</returns>
    public int findClosestHitPointIndex(RaycastHit[] hitPointsList) {
        int closestHitPointIndex = -1;
        float minDistance = 999.0f;
        List<Vector3> relPosList = new List<Vector3>();
        for (int i = 0; i < hitPointsList.Length; i++) {
            if (hitPointsList[i].distance != 0.0f && hitPointsList[i].collider != null) {
                Vector3 relativePosition = variableAggInstance.userPosition.transform.InverseTransformPoint(hitPointsList[i].point);
                relPosList.Add(relativePosition);
                if (hitPointsList[i].distance < minDistance) {
                    minDistance = hitPointsList[i].distance;
                    closestHitPointIndex = i;
                }
            }
        }

        return closestHitPointIndex;
    }

    /// <summary>
    ///     Finds the castBoxes for which the continuous mode has been enabled,.
    /// </summary>
    /// <param name="allCastBoxes">An array of all the castBoxes in the scene</param>
    /// <returns>The castBoxes that meet the criteria</returns>
    public GameObject[] findCastBoxesWithContinuousEnabled(GameObject[] allCastBoxes) {
        List<GameObject> castBoxesWithContinous = new List<GameObject>();

        for (int i = 0; i < allCastBoxes.Length; i++) {
            if (allCastBoxes[i].GetComponent<initSingleBox>().continuousRaycast == true) {
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
    public void placeAlertBox(GameObject castBox, RaycastHit hitPoint) {
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
    public void placeAlertBox(GameObject alertBox, GameObject castBox, RaycastHit hitPoint) {
        alertBox.SetActive(true);
        var alertTrans = alertBox.transform;
        if (!alertBox.GetComponent<AudioSource>().isPlaying)
            alertBox.GetComponent<AudioSource>().Play();

        if (variableAggInstance.enabledModeGlobal == variablesAggregator.CastModeEnum.ScanMode) {
            alertBox.GetComponent<AudioSource>().loop = true;
        } else {
            alertBox.GetComponent<AudioSource>().loop = false;
        }

        var hitPointVec = hitPoint.point;
        var userPositionTrans = variableAggInstance.userPosition.transform;
        if (variableAggInstance.placeAlertBoxAtEyeLevel) {
            alertTrans.position = new Vector3(hitPointVec.x, userPositionTrans.position.y, hitPointVec.z);
        } else {
            alertTrans.position = hitPointVec;
        }
        alertBox.transform.rotation = castBox.transform.rotation;
    }

    /// <summary>
    /// Deactivates all the alert boxes, based on an array of castBoxes
    /// </summary>
    /// <param name="castBoxesArray">The array of castBoxes, whose alert boxes will be deactivated</param>
    public void deactivateAlertBox(GameObject[] castBoxesArray) {
        for (int i = 0; i < castBoxesArray.Length; i++) {
            if (castBoxesArray[i].GetComponent<initSingleBox>().alertBox.GetComponent<AudioSource>().isPlaying)
                castBoxesArray[i].GetComponent<initSingleBox>().alertBox.GetComponent<AudioSource>().Pause();
        }
    }

    /// <summary>
    /// Deactivates an alert box
    /// </summary>
    /// <param name="alertBox">The alert box which will be deactivated</param>
    public void deactivateAlertBox(GameObject alertBox) {
        if (alertBox.GetComponent<AudioSource>().isPlaying)
            if (alertBox.GetComponent<AudioSource>().loop)
                alertBox.GetComponent<AudioSource>().Pause();
    }

    /// <summary>
    /// Deactivates all the alert boxes, which have been defined and are present in the scene
    /// </summary>
    public void deactivateAllAlertBoxes() {
        deactivateAlertBox(variableAggInstance.raycastBoxes);
        deactivateAlertBox(variableAggInstance.commonAlertBox);
        for (int i = 0; i < variableAggInstance.handAlertBoxes.Length; i++) {
            deactivateAlertBox(variableAggInstance.handAlertBoxes[i]);
        }
    }

    /// <summary>
    /// Changes the pitch of the sound, based on the distance of the user from the obstacle.
    /// Three thresholds have been defined for different pitch levels
    /// </summary>
    /// <param name="alertBox">The alert box containing an AudioSource</param>
    /// <param name="hitPoint">The position of the obstacle</param>
    public void changeAlertPitch(GameObject alertBox, RaycastHit hitPoint) {
        changeAlertPitch(alertBox, hitPoint.distance);
    }

    /// <summary>
    /// Changes the pitch of the sound, based on the distance of the user from the obstacle.
    /// Three thresholds have been defined for different pitch levels
    /// </summary>
    /// <param name="alertBox">The alert box containing an AudioSource</param>
    /// <param name="obsDistance">The distance of the obstacle from the user</param>
    public void changeAlertPitch(GameObject alertBox, float obsDistance) {
        if (obsDistance > variableAggInstance.maxDistance
            || (obsDistance <= variableAggInstance.maxDistance && obsDistance > variableAggInstance.warningIndicatorThread)) {
            alertBox.GetComponent<AudioSource>().pitch = variableAggInstance.pitchChangeSafeDist;
        } else if (obsDistance <= variableAggInstance.warningIndicatorThread && obsDistance > variableAggInstance.dangerIndicatorThread) {
            alertBox.GetComponent<AudioSource>().pitch = variableAggInstance.pitchChangeWarningDist;
        } else if (obsDistance <= variableAggInstance.dangerIndicatorThread && obsDistance > 0.2f) {
            alertBox.GetComponent<AudioSource>().pitch = variableAggInstance.pitchChangeDangerousDist;
        } else {
            alertBox.GetComponent<AudioSource>().pitch = variableAggInstance.pitchChangeEndDist;
        }
    }

    /// <summary>
    /// Set the pitch of the sound to orginal value
    /// </summary>
    /// <param name="alertBox">The alert box containing an AudioSource</param>
    public void changeAlertPitch(GameObject alertBox) {
        alertBox.GetComponent<AudioSource>().pitch = variableAggInstance.pitchChangeSafeDist;
    }

    /// <summary>
    /// Disables the alert boxes, the flashes, the rumble and the pitch change.
    /// Moreover, positions the cast boxes based on the mode that has been activated
    /// </summary>
    public void resetStatus() {
        variableAggInstance.voiceCommandSound.GetComponent<AudioSource>().Play();
        this.GetComponent<initBoxes>().deactivateAllAlertBoxes();
        this.GetComponent<initFlashHandler>().StopFlashes();
        this.GetComponent<initRumbleHandler>().StopRumble();
        this.GetComponent<initBoxes>().changeAlertPitch(variableAggInstance.commonAlertBox);
        // this.GetComponent<initBoxes>().initializePosition();
    }
}