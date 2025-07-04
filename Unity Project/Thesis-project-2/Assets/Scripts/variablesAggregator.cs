using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variablesAggregator : MonoBehaviour
{
    public GameObject voiceCommandSound;
    // Variables for debugging purposes
    [Header("--- General Attributes ---")]
    public GameObject userPosition;
    [Tooltip("User's height in meters (m)"), Range(0.0f, 2.5f)]
    public float userHeight = 0.0f;
    [Tooltip("User's width in meters (m)"), Range(0.0f, 2.5f)]
    public float userWidth = 0.0f;
    [Tooltip("The max distance of the raycasts"), Range(0.0f, 5.0f)]
    public float maxDistance = 5.0f;
    public enum CastTypeEnum
    {
        RayCast,
        BoxCast
    };
    [Tooltip("The cast type which will be used")]
    public CastTypeEnum castType;
    [Tooltip("The list of the raycast boxes"), ]
    public GameObject[] raycastBoxes;
    [Header("--- Alert Boxes ---")]
    [Tooltip("The common alert box")]
    public GameObject commonAlertBox;
    public enum AlertBoxTypeEnum
    {
        BoxSpecific,
        Common
    }
    public AlertBoxTypeEnum alertBoxType;
    [Tooltip("The alert box is placed at the height level of the eyes instead of the height level of the hitPoint")]
    public bool placeAlertBoxAtEyeLevel = true;
    public enum CastModeEnum
    {
        ContinuousMode,
        ScanMode,
        HandsMode,
        StopMode
    }
    [Header("--- Cast Modes ---")]
    [Tooltip("The cast mode which will be used")]
    public CastModeEnum enabledModeGlobal;

    [Header("--- Scan/Continuous Mode Attributes ---")]
    [Tooltip("The number of cast boxes placed in each row"), Range(1.0f, 100.0f)]
    public int boxesPerRow=3;
    [Tooltip("The number of cast boxes placed in each column"), Range(1.0f, 100.0f)]
    public int numberOfRows=3;

    [Header("--- Hand Mode Attributes---")]
    [Tooltip("The list of the hand alert boxes")]
    public GameObject[] handAlertBoxes;
    public enum lightPositionEnum
    {
        Up,
        Right,
        Down,
        Left
    }
    [Header("--- Flashing warning attributes ---")]
    [Tooltip("Enables the flash effect")]
    public bool activateFlash = false;
    [Tooltip("The gameObjects which have the flashing effect")]
    public GameObject[] flashingImages;
    [Tooltip("The color of the flash for dangerous distance"), ColorUsage(false)]
    public Color dangerColor;
    [Tooltip("The color of the flash for warning"), ColorUsage(false)]
    public Color warningColor;
    [Tooltip("The color of the flash for safe distance"), ColorUsage(false)]
    public Color safeColor;

    [Header("--- Rumble attributes ---")]
    [Tooltip("Enbales the rumble effect")]
    public bool activateRumble;
    public enum RumbleModes
    {
        Constant,
        Linear,
        Pulse
    }
    #if UNITY_EDITOR
        [Help("The logic for Constant and Linear rumble was never implemented", UnityEditor.MessageType.Warning)]
    #endif
    public RumbleModes rumbleModeSlected;
    [Header("--- Pitch Change attributes ---")]
    [Tooltip("The pitch of the alert sound changes based on the distance of the obstacle from the user")]
    public bool changeAlertPitch = true;
    [Tooltip("The value of the pitch for a safe distance"), Range(1.0f, 3.0f)]
    public float pitchChangeSafeDist = 1.0f;
    [Tooltip("The value of the pitch for a cautious distance"), Range(1.0f, 3.0f)]
    public float pitchChangeWarningDist = 1.15f;
    [Tooltip("The value of the pitch for a dangerous distance"), Range(1.0f, 3.0f)]
    public float pitchChangeDangerousDist = 1.5f;
    [Tooltip("The value of the pitch for a dangerous distance"), Range(1.0f, 3.0f)]
    public float pitchChangeEndDist = 2.5f;
    [Header("--- Flashing, Rumble & Pitch Change common attributes ---")]
    [Tooltip("The maximum distance considered safe"), Min(0.0f)]
    public float distanceThreadToActivate = 2.0f;
    [Tooltip("The maximum distance for which a user should be cautious"), Min(0.0f)]
    public float warningIndicatorThread = 1.5f;
    [Tooltip("The maximum ditsance considered dangerous"), Min(0.0f)]
    public float dangerIndicatorThread = 0.5f;
    
}
