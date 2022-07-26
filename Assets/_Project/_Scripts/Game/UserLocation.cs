using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
using TMPro;    

// This class placed in independent Object called UserLocation
// Exist in hierarchy

// This class handle :
// Updating user latitude longitude
// Sending the data into Canvas0/StatsGroup/UserLocation UI

public class UserLocation : MonoBehaviour
{
    [SerializeField]
    Camera arCamera;
    [SerializeField]
    TMP_Text userLocationTextLatitude, userLocationTextLongitude;

    private void Update() //updating runtime
    {      
        Location cameraLocation = ARLocationManager.Instance.GetLocationForWorldPosition(arCamera.transform.position);
        userLocationTextLatitude.text = "Lat : " + cameraLocation.Latitude.ToString();
        userLocationTextLongitude.text = "Lng : " + cameraLocation.Longitude.ToString();
    }

}
