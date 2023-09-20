using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using UnityEngine;

public class hideMesh : MonoBehaviour
{
    public bool hideMeshAtStart = false;

    private void Start()
    {
       if (hideMeshAtStart == true)
        {
            hideMeshFunc();
        }
    }

    // Start is called before the first frame update
    public void hideMeshFunc()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();

        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
    }

    public void showMeshFunc()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();

        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.Visible;
    }
}
