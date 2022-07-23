using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
using TMPro;

// This class placed in independent Object called CoinManager
// Exist in hierarchy

// This class handle : 
// Single Spawning by defined Latitude Longitude
// updating coin collected by player
// updating coin collected UI, text, sound and animation

public class CoinManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefabsToSpawn;
    [SerializeField]
    List<Transform> spawnerLocations = new List<Transform>();
    
    [SerializeField]
    GameObject UserCoinsUI;
    [SerializeField]
    Animator canvasAnimator;

    // sound manager temporary
    [SerializeField]
    AudioClip coinTakenClip;

    public int coinCollected = 0;

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

        var opts = new PlaceAtLocation.PlaceAtOptions()
        {
            HideObjectUntilItIsPlaced = true,
            MaxNumberOfLocationUpdates = 2,
            MovementSmoothing = 0.1f,
            UseMovingAverage = false
        };
        GameObject objectToPlaced =  Instantiate(prefabsToSpawn);
        PlaceAtLocation.AddPlaceAtComponent(objectToPlaced, loc, opts, useDebugMode : true);
        

    }
    Location RandomSpawnPoint(int indexSpawnerLocation)
    {
            Location spawnLocation = ARLocationManager.Instance.GetLocationForWorldPosition(spawnerLocations[indexSpawnerLocation].position);
            return spawnLocation;
    }
    #endregion

    void StartingCoin() // call this on awake
    {
        // take from the server here
        // coinCollected = coinAmounFromServer
        // call my  place at locations here, Call()
    }
    public void UpdateCoin() // Called by every Coin 
    {
        coinCollected++;
        UserCoinsUI.GetComponentInChildren<TMP_Text>().text = coinCollected.ToString();
        UserCoinsUI.GetComponentInChildren<AudioSource>().PlayOneShot(coinTakenClip);
        canvasAnimator.PlayInFixedTime("Coins_Flipping",0,1.5f);
    }

}
