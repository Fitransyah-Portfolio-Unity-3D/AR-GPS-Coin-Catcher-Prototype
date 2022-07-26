using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This class placed in independent Object called Player
// Exist in hierarchy

// This class handle : 
// Raycasting from centre of screen
// Play Crosshair Animation when detecting coin
// Destroying coin when player take it (while detected)
// Trigger bool in coin blinking
// Trigger bool in coin taken
// Removing the collider of coin taken

public class Player : MonoBehaviour
{
    [Header("Raycast Setup")]
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Image crossHair;
    [SerializeField]
    private Animator uiAnimator;


    [Space(2.0f)]
    [Header("Raycast variable")]
    float raycastDistance;
    [SerializeField]
    float normalRaycastDistance;
    [SerializeField]
    float arrowRaycastDistance;
    [SerializeField]
    float coinDestroyDelay;
    [SerializeField]
    bool arrowPowerOn;

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
            // get "Coin" script component to alter values
            Coin coin = hittarget.collider.gameObject.GetComponent<Coin>();
            coin.blinking = true;
            coin.coinTaken = true;

            // destroy its collider to prevent multiple trigger
            coin.GetComponent<Collider>().enabled = false;
            Destroy(coin.gameObject, coinDestroyDelay);
        }
    }
    public void ArrowButtonOnClicked()
    {
        // animation / effet / sounds
        // visibility to coin = 100 m
        // raycastDistamce = arrowRaycastDistance
        // yield return 10s
        // visibility to coin = normal
        // raycastdistance = normalRaycastDistance
        // arrow power on false

    }
    public void HammerButtonOnClicked()
    {

    }
    IEnumerator ArrowCheatOn()
    {
        yield return null;
    }
    IEnumerator HammerCheatOn()
    {
        yield return null;
    }

}
