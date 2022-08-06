using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using ARLocation;
using Newtonsoft.Json;
using TMPro;



// This class placed in independent Object called CoinManager
// Exist in hierarchy

// This class handle : 
// Making call to xrun Api - app2000-01-rev
// Contain 2 Costum Class for Server Data Deserialiasation
// Place it in 3D World using GeoLocation

// This class info :
// Inherit from ARLocation.PlaceAtLocations

public class MyPlaceAtLocations : PlaceAtLocations
{
    [Space(2.0f)]
    [Header("Gameobject Setup")]
    [SerializeField] GameObject myPrefab;
    [SerializeField] Camera arCamera;

    [Space(2.0f)]
    [Header("Spawning Setup")]
    ServerCoinData serverRawData;
    [TextArea(5,8)]
    public string serverResults;

    [SerializeField]int spawnAmount;
    double latitudeApi;
    double longitudeApi;
    
    [Space(2.0f)]
    [Header("Debugging")]
    [SerializeField]
    TMP_Text debugText ;

    public event Action OnCoinSpawn;
    public event Action OnCoinSpawnStartPopulating;
    public event Action OnCoinSpawnFinishPopulating;

    public void StartPopulateCoins()
    {
        OnCoinSpawnStartPopulating();
        if (spawnAmount == 0 && serverRawData == null)
        {
            Debug.LogWarning("No data in server, so call server again");
            debugText.text = "No data in server, so call server again";
            int randomSpawnAmount = UnityEngine.Random.Range(200, 501);
            spawnAmount = randomSpawnAmount;
            StartCoroutine(CallServer(spawnAmount));
        }
        else if (serverRawData != null)
        {
            Debug.LogWarning("Server data still exist so populate from last data");
            debugText.text = "Server data still exist so populate from last data";
            PopulateCoins(serverRawData);
        }
    }
 
    // ARLocationManager.Instance.GetLocationForWorldPosition() --> to get lat lon from Unity transform
    IEnumerator CallServer(int spawnAmountFinal)
    {
        yield return new WaitForSeconds(3f);
        // retreive player geo location
        Location playerLocation = ARLocationManager.Instance.GetLocationForWorldPosition(arCamera.gameObject.transform.position);
        latitudeApi = playerLocation.Latitude;
        longitudeApi = playerLocation.Longitude;

        //Debugging
        //if (latitudeApi == 0 || longitudeApi == 0)
        //{
        //    latitudeApi = -6.2564442948084915;
        //    longitudeApi = 106.85404328836381;
        //}

        // building query
        string endpoint = "https://app.xrun.run/gateway.php?";
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "coinmapping";
        query["member"] = "1102";
        query["limit"] = spawnAmountFinal.ToString();
        query["lat"] = latitudeApi.ToString();
        query["lng"] = longitudeApi.ToString();
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();
        Debug.Log("Final Uri : \n" + endpoint);
        Debug.Log("Spanwing" + spawnAmountFinal);

        // web request to server
        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // cahching request response
                var rawData = www.downloadHandler.text;
                // store into costum class
                serverRawData = JsonConvert.DeserializeObject<ServerCoinData>(rawData);
                serverResults = rawData;
                
                PopulateCoins(serverRawData);
                debugText.text = $"Server Called finish calling {spawnAmountFinal} coins";
            }
        }
    }
    void PopulateCoins(ServerCoinData coinsData)
    {
        // Prepare the Data
        // Iterating ServerRawData
        // instantiate prefab
        // store all coin properties into prefab component "CoinData"
        // create instance of scriptable object of LocationData class
        // give value to each scriptable object LocationData on its Location field
        // create LocationSettingData instance 
        // give value to each instance of LocationSettingData: 
        // LocationInputType, LocationData, Location etc
        // Place it into GeoLocation
        // notifiy listener/subscriber for OnCoinSpawn
        debugText.text = coinsData.ToString();
        System.Random rand = new System.Random();
        for (int i = 0; i < coinsData.data.Count; i++)
        {
            CoinData prefabCoinDataComponent = myPrefab.GetComponent<CoinData>();

            prefabCoinDataComponent.Coin = coinsData.data[i].Coin;
            prefabCoinDataComponent.Cointype = coinsData.data[i].Cointype;
            prefabCoinDataComponent.Amount = coinsData.data[i].Amount;
            prefabCoinDataComponent.Countlimit = coinsData.data[i].Countlimit;
            prefabCoinDataComponent.Lng = coinsData.data[i].Lng;
            prefabCoinDataComponent.Lat = coinsData.data[i].Lat;
            prefabCoinDataComponent.Distance = coinsData.data[i].Distance;
            prefabCoinDataComponent.Advertisement = coinsData.data[i].Advertisement;
            prefabCoinDataComponent.Brand = coinsData.data[i].Brand;
            prefabCoinDataComponent.Title = coinsData.data[i].Title;
            prefabCoinDataComponent.Contents = coinsData.data[i].Contents;
            prefabCoinDataComponent.Currency = coinsData.data[i].Currency;
            prefabCoinDataComponent.AdColor1 = coinsData.data[i].AdColor1;
            prefabCoinDataComponent.AdColor2 = coinsData.data[i].AdColor2;
            prefabCoinDataComponent.Coins = coinsData.data[i].Coins;
            prefabCoinDataComponent.AdThumbnail = coinsData.data[i].AdThumbnail;
            prefabCoinDataComponent.AdThumbnail2 = coinsData.data[i].AdThumbnail2;
            prefabCoinDataComponent.Tracking = coinsData.data[i].Tracking;
            prefabCoinDataComponent.Isbigcoin = coinsData.data[i].Isbigcoin;
            prefabCoinDataComponent.Symbol = coinsData.data[i].Symbol;
            prefabCoinDataComponent.BrandLogo = coinsData.data[i].BrandLogo;
            prefabCoinDataComponent.Symbolimg = coinsData.data[i].Symbolimg;
            prefabCoinDataComponent.Exad = coinsData.data[i].Exad;
            prefabCoinDataComponent.Exco = coinsData.data[i].Exco;

            LocationData locationData = ScriptableObject.CreateInstance<LocationData>();
            ARLocation.PlaceAtLocation.LocationSettingsData locationSettinsData = new ARLocation.PlaceAtLocation.LocationSettingsData();
            locationData.Location = new Location();

            locationData.Location.Latitude = double.Parse(prefabCoinDataComponent.Lat, System.Globalization.CultureInfo.InvariantCulture);
            locationData.Location.Longitude = double.Parse(prefabCoinDataComponent.Lng, System.Globalization.CultureInfo.InvariantCulture);
            locationData.Location.Altitude = (rand.NextDouble() + 0.2);
            locationData.Location.AltitudeMode = AltitudeMode.GroundRelative; 
            locationData.Location.Label = prefabCoinDataComponent.Coin;

            locationSettinsData.LocationInput.LocationInputType = LocationPropertyData.LocationPropertyType.LocationData;
            locationSettinsData.LocationInput.LocationData = locationData;
            locationSettinsData.LocationInput.Location = locationData.Location;
            locationSettinsData.LocationInput.OverrideAltitudeData.OverrideAltitude = false;


            AddLocation(locationSettinsData.LocationInput.Location, myPrefab);

            OnCoinSpawn();

            debugText.text = $"Finished populating the {i + 1} coin";

        }
        OnCoinSpawnFinishPopulating();
    }
}
# region CostumClassForCoinData
public class ServerCoinData
{
    public string xrunApi { get { return "app2000-01-rev"; } }
    public List<CoinData> data;
}
#endregion
