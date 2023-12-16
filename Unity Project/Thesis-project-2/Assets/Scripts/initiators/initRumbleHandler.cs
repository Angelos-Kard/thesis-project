using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(initCastAndBoxesHandler))]
public class initRumbleHandler : MonoBehaviour
{

    private variablesAggregator variableAggInstance;
    private variablesAggregator.RumbleModes rumbleModeSelected;
    // private PlayerInput _playerInput;
    private Gamepad gamepad = null;
    // private float rumbleDuration;
    private float pulseDuration = 0.0f;
    private float stopDuration = 0.0f;
    private float lowA = 0.0f;
    private float highA = 0.0f;
    private float rumbleStep = 0.0f;
    private float stopStep = 0.0f;
    private bool isMotorActive = false;
    private bool isRumbleCurrentlyActive = false;

    private void Awake()
    {
        // _playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
        rumbleModeSelected = variableAggInstance.rumbleModeSlected;
        // rumbleDuration = variableAggInstance.rumbleDuration;
        gamepad = GetGamepad();
    }

    /// <summary>
    /// Detects and returns the current gamepad
    /// </summary>
    /// <returns>The current gamepad</returns>
    public Gamepad GetGamepad()
    {
        return Gamepad.current;
        /*
        Gamepad gamepadTemp = null;
        Gamepad[] allGamepads = Gamepad.all.ToArray();
        InputDevice[] allInputDevices = _playerInput.devices.ToArray();
        bool gamepadFound = false;

        for (int i = 0; i < allGamepads.Length; i++)
        {
            for (int j = 0; j < allInputDevices.Length; j++)
            {
                if (allGamepads[i].deviceId == allInputDevices[j].deviceId)
                {
                    gamepadTemp = allGamepads[i];
                    break;
                }
            }

            if (gamepadFound) break;
        }

        return gamepadTemp;
        //*/
    }

    /// <summary>
    /// Disables the rumble effect in the current gamepad
    /// </summary>
    public void StopRumble()
    {
        Gamepad gamepad = GetGamepad();

        if (gamepad != null && isRumbleCurrentlyActive)
        {
            isRumbleCurrentlyActive = false;
            gamepad.SetMotorSpeeds(0, 0);
            lowA = 0;
            highA = 0;
            rumbleStep = 0;
            stopStep = 0;
            pulseDuration = 0;
            stopDuration = 0;
        }
    }

    /// <summary>
    /// Activates the rumble with a pulse effect in the current gamepad
    /// </summary>
    /// <param name="low">Speed of low-frequency motor</param>
    /// <param name="high">Speed of high-frequency motor</param>
    /// <param name="stopTime">The time between each rumble (in seconds)</param>
    /// <param name="burstTime">The duration of the rumble (in seconds)</param>
    public void RumblePulse(float low, float high, /*float duration,*/ float stopTime = 0.5f, float burstTime = 0.5f)
    {
        if (rumbleModeSelected == variablesAggregator.RumbleModes.Pulse)
        {
            isRumbleCurrentlyActive = true;
            lowA = low;
            highA = high;
            rumbleStep = burstTime;
            stopStep = stopTime;
            if (pulseDuration == 0.0f)
            {
                pulseDuration = Time.time + burstTime;
                isMotorActive = true;
                if (gamepad != null) gamepad.SetMotorSpeeds(lowA, highA);
            }
            // rumbleDuration = Time.time + duration

            // Invoke(nameof(StopRumble), duration);
        }
    }

    /// <summary>
    /// Based on the distance of the hitPoint, the speed of the motors are calculated.
    /// The speed is inversely propotional of the distance of the user from the hitPoint
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    public float FindMotorsLowAndHigh (float distance)
    {
        return 1.0f - Mathf.Floor((distance / variableAggInstance.maxDistance) * 100.0f) / 100.0f;
    }

    /// <summary>
    /// Based on the distance of the hitPoint, it calculates the duration between each rumble
    /// </summary>
    /// <param name="hitPointDistance">The distance of the hitPoint from the user</param>
    /// <returns>The stop time</returns>
    public float GetStopFrequency (float hitPointDistance)
    {
        float stopStep = 0.0f;

        if (0 <= hitPointDistance && hitPointDistance <= variableAggInstance.dangerIndicatorThread)
        {
            stopStep = 0.25f;
        }
        else if (variableAggInstance.dangerIndicatorThread < hitPointDistance && hitPointDistance <= variableAggInstance.warningIndicatorThread)
        {
            stopStep = 1.0f;
        }
        else if (variableAggInstance.warningIndicatorThread < hitPointDistance && hitPointDistance <= variableAggInstance.distanceThreadToActivate)
        {
            stopStep = 2.0f;
        }

        return stopStep;
    }

    /// <summary>
    /// Applies the speed to the correct motor based on the position of the hitPoint relative to the user
    /// </summary>
    /// <param name="hitPoint">The closest point of an obstacle relative to the user</param>
    /// <param name="motorSpeed">The speed, which will be applied to one of the motors</param>
    /// <returns>A dictionary which dictates what the speed of each motor should be</returns>
    public Dictionary<string, float> GetSpeedOfEachMotor(RaycastHit hitPoint, float motorSpeed)
    {
        Vector3 relativePosition = variableAggInstance.userPosition.transform.InverseTransformPoint(hitPoint.point);
        Dictionary<string, float> motorToEnable = new Dictionary<string, float>()
        {
            {"low", 0.0f},
            {"high", 0.0f}
        };

        // float floorX = Mathf.Floor(relativePosition.x * 100.0f) / 100.0f;

        if (relativePosition.x >= 0.0f)
        {
            motorToEnable["high"] = motorSpeed;
        }
        if (relativePosition.x <=    0.0f)
        {
            motorToEnable["low"] = motorSpeed;
        }

        return motorToEnable;
    }

    /// <summary>
    /// Toggles the activation of the rumble effect based on the button pressed by the user on the gamepad
    /// </summary>
    /// <remarks>
    /// <para>If the user presses A on the gamepad, the rumble effect will be activated (if it was deactivated before).</para>
    /// <para>If the user presses B on the gamepad, the rumble effect will be deactivated (if it was activated before).</para>
    /// </remarks>
    private void rumbleToggleGamepad()
    {
        if (gamepad.bButton.wasPressedThisFrame && variableAggInstance.activateRumble)
        {
            variableAggInstance.activateRumble = false;
            StopRumble();
        }
        else if (gamepad.aButton.wasPressedThisFrame && !variableAggInstance.activateRumble)
        {
            variableAggInstance.activateRumble = true;
        }
    }

    private void Update()
    {
        gamepad = GetGamepad();
        if (gamepad == null) return;

        rumbleToggleGamepad();

        if (!variableAggInstance.activateRumble) return;

        // if (Time.time > rumbleDuration) return;

        if (!isRumbleCurrentlyActive) return;

        switch (rumbleModeSelected)
        {
            /*case variablesAggregator.RumbleModes.Constant:
                break;*/

            case variablesAggregator.RumbleModes.Pulse:
                if (isMotorActive && Time.time > pulseDuration)
                {
                    isMotorActive = !isMotorActive;
                    stopDuration = Time.time + stopStep;
                    gamepad.SetMotorSpeeds(0, 0);
                } else if (!isMotorActive && Time.time > stopDuration)
                {
                    isMotorActive = !isMotorActive;
                    pulseDuration = Time.time + rumbleStep; // Update pulseDuration
                    gamepad.SetMotorSpeeds(lowA, highA);

                }
                break;

            /*case variablesAggregator.RumbleModes.Linear:
                break;*/
        }
    }

}
