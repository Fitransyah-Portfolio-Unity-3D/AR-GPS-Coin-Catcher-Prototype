using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;

public class PathDebug : MonoBehaviour
{
    public void PathOnOff()
    {
        var arObjects = FindObjectsOfType<PlaceAtLocation>();
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
