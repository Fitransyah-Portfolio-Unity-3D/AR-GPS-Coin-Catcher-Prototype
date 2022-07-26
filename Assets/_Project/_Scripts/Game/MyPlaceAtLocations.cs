using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using ARLocation;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Collections.Specialized;
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
    [SerializeField]int spawnAmount;
    double latitudeApi;
    double longitudeApi;
    // string endpoint = $"https://app.xrun.run/gateway.php?act=coinmapping&member=1102&limit=10&lat=-6.2564442948084915&lng=106.85404328836381";
    ServerCoinData serverRawData;
    //List<PlaceAtLocation.LocationSettingsData> LocationsCreated = new List<PlaceAtLocation.LocationSettingsData>();
    [SerializeField] Transform parentForWorldInstance;


    [Space(2.0f)]
    [Header("Debugging")]
    [SerializeField]
    InputField spawnAmountInput;
    [SerializeField]
    TMP_Text debugText;

    public void Test()
    {
        StartCoroutine(CallCoins());
        Coin coin = new Coin();
    }

    // ARLocationManager.Instance.GetLocationForWorldPosition() --> to get lat lon from Unity transform
    public IEnumerator CallCoins()
    {
        // retreive player geo location
        Location playerLocation = ARLocationManager.Instance.GetLocationForWorldPosition(arCamera.gameObject.transform.position);
        latitudeApi = playerLocation.Latitude;
        longitudeApi = playerLocation.Longitude;

        //Debugging
        if (latitudeApi == 0 || longitudeApi == 0)
        {
            latitudeApi = -6.2564442948084915;
            longitudeApi = 106.85404328836381;
        }

        // building query
        string endpoint = "https://app.xrun.run/gateway.php?";
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "coinmapping";
        query["member"] = "1102";
        query["limit"] = spawnAmount.ToString();
        query["lat"] = latitudeApi.ToString();
        query["lng"] = longitudeApi.ToString();
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();
        Debug.Log("Final Uri : \n" + endpoint);
        Debug.Log("Spanwing" + spawnAmount);

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
                debugText.text = rawData;
            }
        }

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
        System.Random rand = new System.Random();
        for (int i = 0; i < serverRawData.data.Count; i++)
        {
            CoinData prefabCoinDataComponent = myPrefab.GetComponent<CoinData>();

            prefabCoinDataComponent.Coin = serverRawData.data[i].Coin;
            prefabCoinDataComponent.Cointype = serverRawData.data[i].Cointype;
            prefabCoinDataComponent.Amount = serverRawData.data[i].Amount;
            prefabCoinDataComponent.Countlimit = serverRawData.data[i].Countlimit;
            prefabCoinDataComponent.Lng = serverRawData.data[i].Lng;
            prefabCoinDataComponent.Lat = serverRawData.data[i].Lat;
            prefabCoinDataComponent.Distance = serverRawData.data[i].Distance;
            prefabCoinDataComponent.Advertisement = serverRawData.data[i].Advertisement;
            prefabCoinDataComponent.Brand = serverRawData.data[i].Brand;
            prefabCoinDataComponent.Title = serverRawData.data[i].Title;
            prefabCoinDataComponent.Contents = serverRawData.data[i].Contents;
            prefabCoinDataComponent.Currency = serverRawData.data[i].Currency;
            prefabCoinDataComponent.AdColor1 = serverRawData.data[i].AdColor1;
            prefabCoinDataComponent.AdColor2 = serverRawData.data[i].AdColor2;
            prefabCoinDataComponent.Coins = serverRawData.data[i].Coins;
            prefabCoinDataComponent.AdThumbnail = serverRawData.data[i].AdThumbnail;
            prefabCoinDataComponent.AdThumbnail2 = serverRawData.data[i].AdThumbnail2;
            prefabCoinDataComponent.Tracking = serverRawData.data[i].Tracking;
            prefabCoinDataComponent.Isbigcoin = serverRawData.data[i].Isbigcoin;
            prefabCoinDataComponent.Symbol = serverRawData.data[i].Symbol;
            prefabCoinDataComponent.BrandLogo = serverRawData.data[i].BrandLogo;
            prefabCoinDataComponent.Symbolimg = serverRawData.data[i].Symbolimg;
            prefabCoinDataComponent.Exad = serverRawData.data[i].Exad;
            prefabCoinDataComponent.Exco = serverRawData.data[i].Exco;

            LocationData locationData = ScriptableObject.CreateInstance<LocationData>();
            PlaceAtLocation.LocationSettingsData locationSettinsData = new PlaceAtLocation.LocationSettingsData();
            locationData.Location = new Location();

            locationData.Location.Latitude = double.Parse(prefabCoinDataComponent.Lat, System.Globalization.CultureInfo.InvariantCulture);
            locationData.Location.Longitude = double.Parse(prefabCoinDataComponent.Lng, System.Globalization.CultureInfo.InvariantCulture);
            locationData.Location.Altitude = rand.NextDouble() + 1.15;
            locationData.Location.AltitudeMode = AltitudeMode.GroundRelative;
            locationData.Location.Label = prefabCoinDataComponent.Coin;

            locationSettinsData.LocationInput.LocationInputType = LocationPropertyData.LocationPropertyType.LocationData;
            locationSettinsData.LocationInput.LocationData = locationData;
            locationSettinsData.LocationInput.Location = locationData.Location;
            locationSettinsData.LocationInput.OverrideAltitudeData.OverrideAltitude = false;


            AddLocation(locationSettinsData.LocationInput.Location, myPrefab);

            //LocationsCreated.Add(locationSettinsData);
        }

        //Debug.Log("Finish CallCoins Step 1");
        // 2. Instantiate and Place At Location
        // for every index of List<PlaceAtLocation.LocationSettingsData>
        // caching newLoc = item.GetLocation()
        // AddLocation(newLoc, withprefab) --> my costum overload

        //foreach (var entry in LocationsCreated)
        //{
        //    var newLoc = entry.GetLocation();
        //    CoinData data = new CoinData();
        //    Coin prefabData = myPrefab.GetComponent<Coin>();
        //    prefabData.properties = data;
        //    AddLocation(newLoc, myPrefab);
        //}
        //Debug.Log("Finish CallCoins Step 2 - End");
    }

    public void UpdatingSpawnAmount()
    {
        spawnAmount =  int.Parse(spawnAmountInput.text);
    }
}
# region CostumClassForCoinData
public class ServerCoinData
{
    public string xrunApi { get { return "app2000-01-rev"; } }
    public List<CoinData> data;
}
#endregion

// XRUN COIN JSON DATA
//"coin":"11112316",
//"cointype":"9411",
//"amount":"0",
//"countlimit":"1",
//"lng":"106.853741",
//"lat":"-6.256539",
//"distance":"35.22395892338059"
//,"advertisement":"668",
//"brand":"XRUN",
//"title":"xrun",
//"contents":"\u3153\u3153",
//"currency":"11",
//"adcolor1":"#283750",
//"adcolor2":"#283750",
//"coins":"0.01",
//"adthumbnail":"2411",
//"adthumbnail2":"2412",
//"tracking":null,
//"isbigcoin":"0",
//"symbol":"XRUN",
//"brandlogo":"2413",
//"symbolimg":"2465",
//"exad":null,
//"exco":null