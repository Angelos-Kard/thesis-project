using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(initCastAndBoxesHandler))]
public class initFlashHandler : MonoBehaviour
{
    private variablesAggregator variableAggInstance;

    // Start is called before the first frame update
    void Start()
    {
        variableAggInstance = this.GetComponent<initCastAndBoxesHandler>().variableAggregatorObject.GetComponent<variablesAggregator>();
    }

    /// <summary>
    /// Disables the flash effect of all the gameObjects
    /// </summary>
    public void StopFlashes()
    {
        GameObject[] flashImagesArray = variableAggInstance.flashingImages;

        for (int i = 0; i < flashImagesArray.Length; i++)
        {
            flashImagesArray[i].GetComponent<flashingLight>().StopFlash();
        }
    }

    // TODO: To delete
    /// <summary>
    /// Disables the flash effect of specific gameObjects
    /// </summary>
    /// <param name="flashImagesArray">The array of gameObjects for which the flash effect will be disabled</param>
    public void StopFlashes(GameObject[] flashImagesArray)
    {
        for (int i = 0; i < flashImagesArray.Length; i++)
        {
            flashImagesArray[i].GetComponent<flashingLight>().StopFlash();
        }
    }

    /// <summary>
    /// Enables the flash effect for specific gameObjects, based on the position of the hitPoint
    /// </summary>
    /// <param name="lightPositions">The position of the hitPoint relative to the user</param>
    /// <param name="flashColor">The color of the flash</param>
    public void StartMultipleFlashes(variablesAggregator.lightPositionEnum[] lightPositions, Color flashColor)
    {
        GameObject[] flashImagesArray = variableAggInstance.flashingImages;

        for (int i = 0; i < flashImagesArray.Length; i++)
        {
            bool shouldBeEnabled = false;

            for (int j = 0; j < lightPositions.Length; j++)
            {
                if (flashImagesArray[i].GetComponent<flashingLight>().lightPosition == lightPositions[j]) shouldBeEnabled = true;
            }

            if (shouldBeEnabled)
            {
                flashImagesArray[i].GetComponent<flashingLight>().StartFlash(1, 0.75f, flashColor);
            } else
            {
                flashImagesArray[i].GetComponent<flashingLight>().StopFlash();
            }
        }
    }

    /// <summary>
    /// Detects the position of the hitPoint relative to the user
    /// </summary>
    /// <param name="hitPoint">The closest point of the mesh to the user</param>
    /// <returns>The position of the hitPoint (<see cref="variablesAggregator.lightPositionEnum"/>)</returns>
    public variablesAggregator.lightPositionEnum[] FindHitPointPosition(RaycastHit hitPoint)
    {
        List<variablesAggregator.lightPositionEnum> lightPositionToreturn = new List<variablesAggregator.lightPositionEnum>();
        Vector3 relativePosition = variableAggInstance.userPosition.transform.InverseTransformPoint(hitPoint.point);
        
        if (relativePosition.x > 0.3f)
        {
            lightPositionToreturn.Add(variablesAggregator.lightPositionEnum.Right);
        }
        else if (relativePosition.x < -0.3f)
        {
            lightPositionToreturn.Add(variablesAggregator.lightPositionEnum.Left);
        }

        if (relativePosition.y > 0)
        {
            lightPositionToreturn.Add(variablesAggregator.lightPositionEnum.Up);
        }
        else
        {
            lightPositionToreturn.Add(variablesAggregator.lightPositionEnum.Down);
        }

        return lightPositionToreturn.ToArray();
    }

    /// <summary>
    /// Based on the distance, it returns the color of the flash which will be applied to the gameObjects.
    /// There are three possible colors to be applied:
    /// <list type="bullet">
    ///     <item>
    ///         <description>Red: If the distance of the hitPoint is less than <see cref="variablesAggregator.dangerIndicatorThread">dangerIndicatorThread</see></description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             Yellow: If the distance of the hitPoint is more than <see cref="variablesAggregator.dangerIndicatorThread">dangerIndicatorThread</see>,
    ///             but less than <see cref="variablesAggregator.warningIndicatorThread">warningIndicatorThread</see>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             Yellow: If the distance of the hitPoint is more than <see cref="variablesAggregator.warningIndicatorThread">warningIndicatorThread</see>,
    ///             but less than <see cref="variablesAggregator.distanceThreadToActivate">distanceThreadToActivate</see>
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    public Color FindColorBasedOnDistance(float distance)
    {
        Color dangerColor = variableAggInstance.dangerColor;
        Color warningColor = Color.yellow;
        Color safeColor = Color.green;

        if (distance >= 0 && distance <= variableAggInstance.dangerIndicatorThread)
        {
            return dangerColor;
        }
        else if (distance > variableAggInstance.dangerIndicatorThread && distance <= variableAggInstance.warningIndicatorThread)
        {
            return warningColor;
        }
        else if (distance > variableAggInstance.warningIndicatorThread && distance < variableAggInstance.distanceThreadToActivate)
        {
            return safeColor;
        }
        else
        {
            return new Color(0, 0, 0, 0);
        }
        
    }

}
