using System.Collections.Generic;
using UnityEngine;
using ARLocation;


public class MyPlaceAtLocation : MonoBehaviour
{
    [SerializeField]
    UnityEngine.GameObject prefabsToSpawn;
    [SerializeField]
    List<Transform> spawnerLocations = new List<Transform>();

    #region SingleSPawning
    public void SpawnCoinByPlayer() // call by Button Feature Two in UI
    {
        Location pickedLocation = RandomSpawnPoint(Random.Range(0, 4));
        var loc = new Location()
        {
            Latitude = pickedLocation.Latitude,
            Longitude = pickedLocation.Longitude,
            Altitude = 0,
            AltitudeMode = AltitudeMode.GroundRelative
        };

        var opts = new ARLocation.PlaceAtLocation.PlaceAtOptions()
        {
            HideObjectUntilItIsPlaced = true,
            MaxNumberOfLocationUpdates = 2,
            MovementSmoothing = 0.1f,
            UseMovingAverage = false
        };
        GameObject objectToPlaced = Instantiate(prefabsToSpawn);
        PlaceAtLocation.AddPlaceAtComponent(objectToPlaced, loc, opts, useDebugMode: true);


    }
    Location RandomSpawnPoint(int indexSpawnerLocation)
    {
        Location spawnLocation = ARLocationManager.Instance.GetLocationForWorldPosition(spawnerLocations[indexSpawnerLocation].position);
        return spawnLocation;
    }
    #endregion
}
