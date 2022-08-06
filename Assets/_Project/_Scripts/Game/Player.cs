using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Web;
using UnityEngine.Networking;

// This class placed in independent Object called Player
// Exist in hierarchy

// This class handle : 
// Raycasting from centre of screen
// Play Crosshair Animation when detecting coin
// checking coin status when collecting/server check
// if true do trigger :
// Trigger bool in coin blinking
// Trigger bool in coin taken
// if false show error messages

public class Player : MonoBehaviour
{
    [Header("Raycast Setup")]
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Image crossHair;
    [SerializeField]
    private Animator uiAnimator;
    [SerializeField]
    Button arrowButton;
    [SerializeField]
    Button hammerButton;

    [Header("Variables")]
    float raycastDistance;
    [SerializeField]
    float normalRaycastDistance;
    [SerializeField]
    float arrowRaycastDistance;
    [SerializeField]
    float coinDestroyDelay;
    [SerializeField]
    bool arrowPowerOn;

    public event Action OnPlayerTakeCoin;

    private void Awake()
    {
        raycastDistance = normalRaycastDistance;
    }
    private void Update()
    {
        // Detection system
        // Casting a ray to detect 3D Object on normalRaycastDistance range
        Ray rayOrigin = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hittarget;
        // if its hit something
        if (Physics.Raycast(rayOrigin, out hittarget, raycastDistance))
        {
            if (arrowPowerOn)
            {
                // start Co-Routine ArrowCheatOn
            }

            // if target not coin, return (later)
            UpdatingCrosshair(Color.red, true);
        }
        else
        {
            UpdatingCrosshair(Color.green, false);
        }
    }
    void UpdatingCrosshair(Color crosshairColor, bool setBoolValue)
    {
        crossHair.color = crosshairColor;
        uiAnimator.SetBool("isDetecting", setBoolValue);
    }
    public void CatchButtonOnClicked()
    {
        // Collecting Coin System
        // Casting a ray to detect 3D Object on normalRaycastDistance range
        Ray rayOrigin = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hittarget;
        // if its hit something
        if (Physics.Raycast(rayOrigin, out hittarget, raycastDistance))
        {
            // Version 1
            // get "Coin" script component to trigger states
            Coin coin = hittarget.collider.gameObject.GetComponent<Coin>();
            coin.blinking = true;
            coin.coinTaken = true;

            OnPlayerTakeCoin();

            // Version 2
            // Freeze the game, panel up
            // StartCo-Routine Validating Coin
            
        }
    }
    public void ArrowButtonOnClicked()
    {
        // animation / effect / sounds
        // visibility to coin = 100 m
        // raycastDistamce = arrowRaycastDistance
        // yield return 10s
        // visibility to coin = normal
        // raycastdistance = normalRaycastDistance
        // arrowAnimator power on false
        StartCoroutine(ArrowCheatOn());

    }
    public void HammerButtonOnClicked()
    {
        // 
    }
    IEnumerator ValidatingCoin()
    {
        // building query
        string endpoint = "https://app.xrun.run/gateway.php?";
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        //query["act"] = "coinmapping";
        //query["member"] = "1102";
        //query["limit"] = spawnAmount.ToString();
        //query["lat"] = latitudeApi.ToString();
        //query["lng"] = longitudeApi.ToString();
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();
        Debug.Log("Final Uri : \n" + endpoint);
       

        // web request to server
        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                // Updating text panel with network error
            }
            else
            {
                
                if (www.downloadHandler.text == "NO")
                {
                    // updating panel with error message
                    // close panel
                    // return
                }
                else
                {
                    // get "Coin" script component to trigger states
                    //Coin coin = hittarget.collider.gameObject.GetComponent<Coin>();
                    //coin.blinking = true;
                    //coin.coinTaken = true;

                    //OnPlayerTakeCoin();
                }

            }
        }

    }
    IEnumerator ArrowCheatOn()
    {
        Image arrowButtonImage = GameObject.FindGameObjectWithTag("Arrow").GetComponent<Image>();
        Color previousColor = arrowButtonImage.color;
        arrowButtonImage.color = Color.white;
        raycastDistance = arrowRaycastDistance;
        arCamera.orthographic = true;
        arCamera.orthographicSize = 2f;
        arCamera.farClipPlane = 120f;
        yield return new WaitForSeconds(20f);
        arrowButtonImage.color = previousColor;
        arCamera.orthographicSize = 5f;
        arCamera.farClipPlane = 20f;
        arCamera.orthographic = false;
        raycastDistance = normalRaycastDistance;
    }
    IEnumerator HammerCheatOn()
    {
        yield return null;
    }

}
