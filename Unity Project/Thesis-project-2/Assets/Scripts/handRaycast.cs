using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections.Generic;
using UnityEngine;

public class handRaycast : MonoBehaviour
{
    public GameObject[] handAlertBoxes;
    public bool enabledHandAB = true;
    private Dictionary<string, GameObject> handAlertBoxesDict = new Dictionary<string, GameObject>();

    public void Start()
    {
        foreach (var handAlertBox in handAlertBoxes)
        {
            if (!handAlertBox.CompareTag("Untagged"))
            {
                handAlertBoxesDict.Add(handAlertBox.tag, handAlertBox);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (enabledHandAB == true)
        {
            var isHandPresent = false;

            foreach (var source in CoreServices.InputSystem.DetectedInputSources)
            {
                // Ignore anything that is not a hand because we want articulated hands
                if (source.SourceType == InputSourceType.Hand && source.SourceName != "None Hand")
                {
                    isHandPresent = true;
                    foreach (var p in source.Pointers)
                    {
                        if (p is IMixedRealityNearPointer)
                        {
                            // Ignore near pointers, we only want the rays
                            continue;
                        }
                        if (p.Result != null)
                        {
                            //var startPoint = p.Position;
                            var endPoint = p.Result.Details.Point;
                            var rayDist = p.Result.Details.RayDistance;
                            //var hitObject = p.Result.Details.Object;
                            if (rayDist <= 5.0f)
                            {
                                if (handAlertBoxesDict.ContainsKey(source.SourceName)) {
                                    handAlertBoxesDict[source.SourceName].SetActive(true);
                                    handAlertBoxesDict[source.SourceName].transform.position = endPoint;
                                }
                                /*var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                sphere.transform.localScale = Vector3.one * 0.01f;
                                sphere.transform.position = endPoint;*/
                            } else
                            {
                                if (handAlertBoxesDict.ContainsKey(source.SourceName))
                                {
                                    handAlertBoxesDict[source.SourceName].SetActive(false);
                                }
                            }
                        }

                        if (p.Controller.Interactions[2].BoolData == true)
                        {
                            if (handAlertBoxesDict.ContainsKey(source.SourceName))
                            {
                                if (handAlertBoxesDict[source.SourceName].GetComponent<AudioSource>().isPlaying == true)
                                {
                                    handAlertBoxesDict[source.SourceName].GetComponent<AudioSource>().Pause();
                                }
                            }
                        } else
                        {
                            if (handAlertBoxesDict.ContainsKey(source.SourceName))
                            {
                                if (handAlertBoxesDict[source.SourceName].GetComponent<AudioSource>().isPlaying == false
                                    && handAlertBoxesDict[source.SourceName].activeSelf)
                                {
                                    handAlertBoxesDict[source.SourceName].GetComponent<AudioSource>().Play();
                                }
                            }
                        }
                    }
                }
            }

            if (!isHandPresent)
            {
                foreach (var handAlertBox in handAlertBoxes)
                {
                    handAlertBox.SetActive(false);
                }
            }
        }
    }

    public void enabledHandABToggle()
    {
        enabledHandAB = !enabledHandAB;

        if (enabledHandAB == false)
        {
            foreach (var handAlertBox in handAlertBoxes)
            {
                handAlertBox.SetActive(false);
            }
        }
    }
}