using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Collections.Specialized;


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

    CoinData serverRawData;
    List<PlaceAtLocation.LocationSettingsData> LocationsCreated = new List<PlaceAtLocation.LocationSettingsData>();
    [SerializeField] Text text;
    #region MyCostumClass
   
    
    #endregion
    public void Test()
    {
        StartCoroutine(CallCoins());
    }

    // ARLocationManager.Instance.GetLocationForWorldPosition() --> to get lat lon from Unity transform
    public IEnumerator CallCoins()
    {
        // 0. retreive player geo location
        // define how many to spawn
        // make web request
        // parse the result
        // store into instance of costum class ServerRawData
        Location playerLocation = ARLocationManager.Instance.GetLocationForWorldPosition(arCamera.gameObject.transform.position);
        latitudeApi = playerLocation.Latitude;
        longitudeApi = playerLocation.Longitude;

        string endpoint = "https://app.xrun.run/gateway.php?";
        string url = APICombiner(endpoint, spawnAmount, latitudeApi, longitudeApi);

        string complete = "https://app.xrun.run/gateway.php?act=coinmapping&member=1102&limit=35&lat=-6.2564442948084915&lng=106.85404328836381";
        // web request to server
        using (UnityWebRequest www = UnityWebRequest.Get(complete))
        {
            
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // cahching GET request response
                var rawData = www.downloadHandler.text;
                // store into local field
                serverRawData = JsonConvert.DeserializeObject<CoinData>(rawData);
            }
        }

        Debug.Log("Finish CallCoins Step 0");
        // 1. Prepare the Data
        // Iterating ServerRawData
        // create instance of scriptable object of LocationData class
        // give value to each scriptable object LocationData on its Location field
        // create LocationSettingData instance 
        // give value to each instance of LocationSettingData: 
        // LocationInputType, LocationData, Location etc
        // adding it to the List<PlaceAtLocation.LocationSettingsData>

        for (int i = 0; i < serverRawData.data.Count; i++)
        {
            LocationData newSo = ScriptableObject.CreateInstance<LocationData>();
            PlaceAtLocation.LocationSettingsData item = new PlaceAtLocation.LocationSettingsData();
            newSo.Location = new Location();
            newSo.Location.Latitude = double.Parse(serverRawData.data[i].Lat, System.Globalization.CultureInfo.InvariantCulture);
            newSo.Location.Longitude = double.Parse(serverRawData.data[i].Lng, System.Globalization.CultureInfo.InvariantCulture); 
            newSo.Location.Altitude = 0.05;
            newSo.Location.AltitudeMode = AltitudeMode.GroundRelative;
            newSo.Location.Label = serverRawData.data[i].Cointype;

            Debug.Log(newSo.Location.Latitude);
            Debug.Log(newSo.Location.Longitude);
            Debug.Log(newSo.Location.Altitude);

            item.LocationInput.LocationInputType = LocationPropertyData.LocationPropertyType.LocationData;
            item.LocationInput.LocationData = newSo;
            item.LocationInput.Location = newSo.Location;
            item.LocationInput.OverrideAltitudeData.OverrideAltitude = false;
            item.LocationInput.OverrideAltitudeData.Altitude = 0.5;
            item.LocationInput.OverrideAltitudeData.AltitudeMode = AltitudeMode.GroundRelative;
            LocationsCreated.Add(item);

        }

        Debug.Log("Finish CallCoins Step 1");
        // 2. Instantiate and Place At Location
        // for every index of List<PlaceAtLocation.LocationSettingsData>
        // caching newLoc = item.GetLocation()
        // AddLocation(newLoc, withprefab) --> my costum overload

        foreach (var entry in LocationsCreated)
        {
            var newLoc = entry.GetLocation();

            AddLocation(newLoc, myPrefab);
        }
        Debug.Log("Finish CallCoins Step 2 - End");
    }

    string APICombiner(string endpoint, int limitNumber, double latitude, double longitude)
    {
        NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
        
        queryString.Add("act", "coinmapping");
        queryString.Add("member", "1102");
        queryString.Add("limit", limitNumber.ToString());
        queryString.Add("lat", latitude.ToString());
        queryString.Add("lng", longitude.ToString());

        string urlString = System.Web.HttpUtility.UrlPathEncode(endpoint+queryString);
        return urlString;

    }
}
# region CostumClassForCoinData
public class CoinData
{
    public string xrunApi { get { return "app2000-01-rev"; } }
    public List<CoinDataProperty> data;

    public class CoinDataProperty
    {
        public string Coin { get; set; }
        public string Cointype { get; set; }
        public string Amount { get; set; }
        public string Countlimit { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public string Distance { get; set; }
        public string Advertisement { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Currency { get; set; }
        public string AdColor1 { get; set; }
        public string adColor2 { get; set; }
        public string Coins { get; set; }
        public string AdThumbnail { get; set; }
        public string AdThumbnail2 { get; set; }
        public string Tracking { get; set; }
        public string Isbigcoin { get; set; }
        public string Symbol { get; set; }
        public string BrandLogo { get; set; }
        public string Symbolimg { get; set; }
        public string Exad { get; set; }
        public string exco { get; set; }
    }
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