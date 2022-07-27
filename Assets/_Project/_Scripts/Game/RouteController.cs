using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
using ARLocation.MapboxRoutes;
public class RouteController : MonoBehaviour
{
    [SerializeField]
    MapboxRoute mapboxRoute;
    [SerializeField]
    string MapboxToken = "pk.eyJ1IjoieHJ1bmxsYyIsImEiOiJjbDVtZnZvYXcwMTYzM2hwOHhtYjV1czViIn0.UMgF3FlHnPNjTIQ_GcWWzg";

    [SerializeField]
    List<PlaceAtLocation> nearestCoins = new List<PlaceAtLocation>();

    public AbstractRouteRenderer RoutePathRenderer;
    public AbstractRouteRenderer NextTargetPathRenderer;
   private AbstractRouteRenderer currentPathRenderer => s.LineType == LineType.Route ? RoutePathRenderer : NextTargetPathRenderer;

    int listIndex;

    private void Start()
    {
        mapboxRoute.Settings.Language = MapboxApiLanguage.German;
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

    public void GrabCoins()
    {
        // grab 5 nearest coin
        for (int i = 0; i < 5; i++)
        {
            var foundCoin = GameObject.Find($"MyPlaceAtLocations - {i}").GetComponent<PlaceAtLocation>();
            nearestCoins.Add(foundCoin);
        }
        if (nearestCoins.Count > 0)
        {
            StartRoute(nearestCoins[0].Location);
            listIndex = 0;
        }
        else
        {
            Debug.LogError("No coin in the list!");
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
            }
            else if (listIndex == nearestCoins.Count)
            {
                listIndex = 0;
                StartRoute(nearestCoins[listIndex].Location);
            }
        }
        else
        {
            Debug.Log("No coin in the list!");
        }
        Debug.Log(listIndex.ToString());
        
        
    }
    public void StartRoute(Location dest)
    {
        s.destination = dest;
        loadRoute();
    }
    public void EndRoute()
    {
        currentPathRenderer.enabled = false;
        s.View = View.Normal;
    }
    private void loadRoute()
    {
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
