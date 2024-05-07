using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(initCastAndBoxesHandler)), RequireComponent(typeof(continuousCast))]
public class stopCast : MonoBehaviour
{
    private variablesAggregator variableAggInstance;

    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
    }

    /// <summary>
    /// Toggle the stop mode. In stop mode, all alert boxes are disabled.
    /// </summary>
    /// <remarks>
    ///     The function is called with the voice input <c>"Stop Mode"</c>
    /// </remarks>
    public void EnableStopMode()
    {
        variablesAggregator.CastModeEnum previousMode = variableAggInstance.enabledModeGlobal;

        if (previousMode != variablesAggregator.CastModeEnum.StopMode)
        {
            variableAggInstance.enabledModeGlobal = variablesAggregator.CastModeEnum.StopMode;
            this.GetComponent<initBoxes>().resetStatus();
        }
    }
}
