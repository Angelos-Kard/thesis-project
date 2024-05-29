using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashingLight : MonoBehaviour
{
    public GameObject variableAggregatorObject;
    private variablesAggregator variableAggInstance;
    [Tooltip("Position of the GameObject relative to the user")]
    public variablesAggregator.lightPositionEnum lightPosition;
    private IEnumerator currentFlashRoutine = null;
    private bool shouldFlashStop = false;
    private MaterialPropertyBlock newColor;

    void Start() {
        variableAggInstance = variableAggregatorObject.GetComponent<variablesAggregator>();
        newColor = new MaterialPropertyBlock();
    }

    /// <summary>
    /// Starts the flash effect for the specific gameObject
    /// </summary>
    /// <param name="flashInterval">The duration of the flash effect</param>
    /// <param name="maxAlpha">The max value of the alpha (max opacity)</param>
    /// <param name="colorOfTheFlash">The color of the flash</param>
    public void StartFlash(float flashInterval, float maxAlpha, Color colorOfTheFlash) {

        newColor.SetColor("_Color", colorOfTheFlash);
        this.GetComponent<Renderer>().SetPropertyBlock(newColor);

        // 0 <= maxAlpha <= 1
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        if (currentFlashRoutine == null) {
            currentFlashRoutine = Flash(flashInterval, maxAlpha);
            shouldFlashStop = false;
            StartCoroutine(currentFlashRoutine);
        }

    }

    /// <summary>
    /// Stops the flash effect for the specific gameObject
    /// </summary>
    public void StopFlash() {
        shouldFlashStop = true;
        if (currentFlashRoutine != null) {
            StopCoroutine(currentFlashRoutine);
            currentFlashRoutine = null;

            newColor.SetColor("_Color", new Color(0, 0, 0, 0));
            this.GetComponent<Renderer>().SetPropertyBlock(newColor);
        }
    }

    /// <summary>
    /// Applies the flash effect to the gameObject. The effect is looped.
    /// </summary>
    /// <param name="flashInterval">The duration of one flash (in seconds)</param>
    /// <param name="maxAlpha">The max value of the alpha</param>
    /// <returns></returns>
    IEnumerator Flash(float flashInterval, float maxAlpha) {
        // Flash In
        float flashInDuration = flashInterval / 2;

        while(!shouldFlashStop) {
            for (float t = 0; t <= flashInDuration; t += Time.deltaTime) {
                Color colorThisFrame = newColor.GetColor("_Color");
                colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t / flashInDuration);
                newColor.SetColor("_Color", colorThisFrame);
                this.GetComponent<Renderer>().SetPropertyBlock(newColor);

                // wait until next frame
                yield return null;
            }

            // Flash Out
            float flashOutDuration = flashInterval / 2;
            for (float t = 0; t <= flashOutDuration; t += Time.deltaTime) {
                Color colorThisFrame = newColor.GetColor("_Color");
                colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t / flashOutDuration);
                newColor.SetColor("_Color", colorThisFrame);
                this.GetComponent<Renderer>().SetPropertyBlock(newColor);
                yield return null;
            }
        }

        // ensure alpha is 0
        newColor.SetColor("_Color", new Color(0, 0, 0, 0));
        this.GetComponent<Renderer>().SetPropertyBlock(newColor);
    }
}