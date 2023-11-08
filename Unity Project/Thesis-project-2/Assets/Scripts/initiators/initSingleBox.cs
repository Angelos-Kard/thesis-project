using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initSingleBox : MonoBehaviour
{
    [Tooltip("The object which will operate as an alert box for the specific raycast.")]
    public GameObject alertBox;
    [Tooltip("If it set to 'True', rays will be casted in every frame.")]
    public bool continuousRaycast = false;
}
