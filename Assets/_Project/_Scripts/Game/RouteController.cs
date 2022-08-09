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
    GameObject ARLocationRoot;
    [SerializeField]
    TMP_Text debugText;

    [SerializeField]
    List<PlaceAtLocation> nearestCoins = new List<PlaceAtLocation>();
    [SerializeField]
    PlaceAtLocation currentTarget;

    public AbstractRouteRenderer RoutePathRenderer;
    public AbstractRouteRenderer NextTargetPathRenderer;
   private AbstractRouteRenderer currentPathRenderer => s.LineType == LineType.Route ? RoutePathRenderer : NextTargetPathRenderer;

    int listIndex;
    [SerializeField]
    string MapboxToken;
    private void Awake()
    {
        mapboxRoute.Settings.Language = MapboxApiLanguage.Indonesian;
        MapboxToken = MyMapboxToken.Token;
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
        if (ARLocationRoot.transform.childCount > 0)
        {
            currentTarget = ARLocationRoot.transform.GetChild(0).GetComponent<PlaceAtLocation>();
            StartRoute(currentTarget.Location);
            debugText.text = DisplayCoinData(currentTarget.gameObject);
        }
        else
        {
            debugText.text = "Currently no spawned object, please wait or make sure your location is enabled!";
        }
        

    }
    public void PickNextCoin()
    {
        int currentIndex = currentTarget.transform.GetSiblingIndex();
        if (currentIndex == ARLocationRoot.transform.childCount - 1)
        {
            debugText.text = "No more object available please please press previous target";
        }
        else if (currentIndex < ARLocationRoot.transform.childCount)
        {
            currentTarget = ARLocationRoot.transform.GetChild(currentIndex + 1).GetComponent<PlaceAtLocation>();
            StartRoute(currentTarget.Location);
            debugText.text = DisplayCoinData(currentTarget.gameObject);
        }

        
    }
    public void PickPreviousCoin()
    {
        int currentIndex = currentTarget.transform.GetSiblingIndex();
        if (currentIndex == 0)
        {
            debugText.text = "No more object available please please press next target";
        }
        else if (currentIndex > -1)
        {
            currentTarget = ARLocationRoot.transform.GetChild(currentIndex - 1).GetComponent<PlaceAtLocation>();
            StartRoute(currentTarget.Location);
            debugText.text = DisplayCoinData(currentTarget.gameObject);
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
        currentTarget = null;
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
    }// TODO : Build Minimap feature
}
