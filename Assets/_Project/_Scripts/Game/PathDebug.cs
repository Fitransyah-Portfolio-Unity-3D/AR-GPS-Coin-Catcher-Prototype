using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;

// This class placed in independent Object called PathDebug
// Exist in hierarchy

// This class handle : 
// Turn on and off path to coin
// Called by button in Canvas0/FeaturePanel/FeatureOneButton

public class PathDebug : MonoBehaviour
{

    public void PathOnOff()
    {
        PlaceAtLocation[] arObjects = FindObjectsOfType<PlaceAtLocation>();
        foreach (PlaceAtLocation itemobject in arObjects)
        {
            LineRenderer line = itemobject.GetComponent<LineRenderer>();
            if (line.enabled == true)
            {
                line.enabled = false;
            }
            else
            {
                line.enabled = true;
            }           
        }
    }
}
