using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class positionRaycasts : MonoBehaviour
{
    [Tooltip("User's height in centimeters (cm)"), Range(0, 250)]
    public int userHeight = 0;
    public GameObject[] raycastBoxes;

    // Start is called before the first frame update
    void Start()
    {
        if (userHeight > 0)
        {

        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
