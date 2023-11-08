using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variablesAggregator : MonoBehaviour
{
    // Variables for debugging purposes
    [Header("--- General Attributes ---")]
    [Tooltip("User's height in centimeters (cm)"), Range(0.0f, 2.5f)]
    public float userHeight = 0.0f;
    [Tooltip("The max distance of the raycasts")]
    public float maxDistance = 5.0f;
    public enum CastTypeEnum
    {
        RayCast,
        BoxCast
    };
    [Tooltip("The cast type which will be used")]
    public CastTypeEnum castType;
    [Tooltip("The list of the raycast boxes")]
    public GameObject[] raycastBoxes;
    [Tooltip("The list of the alert boxes")]
    public GameObject[] alertBoxes;
    public enum CastModeEnum
    {
        ContinuousMode,
        ScanMode
    }
    [Tooltip("The cast mode which will be used")]
    public CastModeEnum enabledModeGlobal;
    public bool enableContinuousModeGlobal = false;

    [Header("--- Scan/Continuous Mode Attributes ---")]
    [Range(1.0f, 100.0f)]
    public int boxesPerRow=3;
    [Range(1.0f, 100.0f)]
    public int numberOfRows=3;

    [Header("--- Hand Mode Attributes---")]
    [Tooltip("The list of the hand alert boxes")]
    public GameObject[] handAlertBoxes;
    public bool enabledHandAB = true;
}
