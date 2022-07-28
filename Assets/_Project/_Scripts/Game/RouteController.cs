using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
using ARLocation.MapboxRoutes;
using TMPro;

public class RouteController : MonoBehaviour
{
    [SerializeField]
    MapboxRoute mapboxRoute;
    [SerializeField]
    string MapboxToken = "pk.eyJ1IjoieHJ1bmxsYyIsImEiOiJjbDVtZnZvYXcwMTYzM2hwOHhtYjV1czViIn0.UMgF3FlHnPNjTIQ_GcWWzg";
    [SerializeField]
    TMP_Text debugText;

    [SerializeField]
    List<PlaceAtLocation> nearestCoins = new List<PlaceAtLocation>();

    public AbstractRouteRenderer RoutePathRenderer;
    public AbstractRouteRenderer NextTargetPathRenderer;
   private AbstractRouteRenderer currentPathRenderer => s.LineType == LineType.Route ? RoutePathRenderer : NextTargetPathRenderer;

    int listIndex;

    private void Awake()
    {
        mapboxRoute.Settings.Language = MapboxApiLanguage.Indonesian;
    }
    public LineType PathRendererType
    {
        get => s.LineType;
        set
        {
            if (value != s.LineType)
            {
                currentPathRenderer.enabled = false;
                s.LineType = value;
                currentPathRenderer.enabled = true;

                if (s.View == View.Route)
                {
                    mapboxRoute.RoutePathRenderer = currentPathRenderer;
                }
            }
        }
    }
    enum View
    {
        Normal,
        Route,
    }
    public enum LineType
    {
        Route,
        NextTarget
    }
    [System.Serializable]
    private class State
    {
        public List<GeocodingFeature> Results = new List<GeocodingFeature>();
        public View View = View.Normal;
        public Location destination;
        public LineType LineType = LineType.NextTarget;
        public string ErrorMessage;
    }
    [SerializeField]
    private State s = new State();

    public void ActivateRoute()
    {
        // grab 10 nearest coin
        for (int i = 0; i < 11; i++)
        {
            var foundCoin = GameObject.Find($"MyPlaceAtLocations - {i}").GetComponent<PlaceAtLocation>();
            nearestCoins.Add(foundCoin);
        }
        if (nearestCoins.Count > 0)
        {
            StartRoute(nearestCoins[0].Location);
            listIndex = 0;
            debugText.text = DisplayCoinData(nearestCoins[0].gameObject);
            
        }
        else
        {
            Debug.LogError("No coin in the list!");
            return;
        }
    }
    public void PickNextCoin()
    {
        if (nearestCoins.Count > 0)
        {
            if (listIndex < nearestCoins.Count)
            {
                listIndex++;
                StartRoute(nearestCoins[listIndex].Location);
                debugText.text = DisplayCoinData(nearestCoins[listIndex].gameObject);
            }
            else if (listIndex == nearestCoins.Count)
            {
                listIndex = 0;
                debugText.text = DisplayCoinData(nearestCoins[listIndex].gameObject);
                debugText.text = "Its end of list, back to the first in the list";
                return;
            }
        }
        else
        {
            Debug.Log("No coin to select");
        }   
    }
    public void PickPreviousCoin()
    {
        if (nearestCoins.Count > 0 )
        {
            if (listIndex > -1)
            {
                listIndex--;
                StartRoute(nearestCoins[listIndex].Location);
                debugText.text = DisplayCoinData(nearestCoins[listIndex].gameObject);
            }
            else if (listIndex == -1)
            {
                listIndex = nearestCoins.Count - 1;
                debugText.text = DisplayCoinData(nearestCoins[listIndex].gameObject);
                debugText.text = "Its the end of list, back to the last in the list";
                return;
            }
        }
        else
        {
            Debug.Log("No coin to select");
        }
    }
    string DisplayCoinData(GameObject coin)
    {
        CoinData coinData = coin.GetComponent<CoinData>();
        if (coinData == null)
        {
            return "This coin has no data!";
            
        }
        else
        {
            return $"Coin details : \n" +
            $"Coin ID : {coinData.Coin}\n" +
            $"Coin type : {coinData.Cointype}\n" +
            $"Coin type : {coinData.Cointype}\n" +
            $"Lat : {coinData.Lat}\n" +
            $"Lng : {coinData.Lng}\n" +
            $"Distance : {coinData.Distance}\n" +
            $"Distance : {coinData.Distance}\n" +
            $"Xrun value : {coinData.Coins}\n";
        }
    }
    public void DisableRoute()
    {
        
        s.View = View.Normal;
        currentPathRenderer.enabled = false;
        mapboxRoute.RoutePathRenderer = currentPathRenderer;
        nearestCoins.Clear();
        debugText.text = "No coin selected!";
    }
    public void StartRoute(Location dest)
    {
        s.destination = dest;
        if (s.destination != null)
        {
            var api = new MapboxApi(MapboxToken);
            
            var result = StartCoroutine(
                    mapboxRoute.LoadRoute(
                        new RouteWaypoint { Type = RouteWaypointType.UserLocation },
                        new RouteWaypoint { Type = RouteWaypointType.Location, Location = s.destination },
                        (err) =>
                        {
                            if (err != null)
                            {

                                Debug.Log(err);
                                return;
                            }


                            s.View = View.Route;

                            currentPathRenderer.enabled = true;
                            mapboxRoute.RoutePathRenderer = currentPathRenderer;
                            
                            //currentResponse = res;
                            //buildMinimapRoute(res);
                            
                        }));
        }
    }
}
