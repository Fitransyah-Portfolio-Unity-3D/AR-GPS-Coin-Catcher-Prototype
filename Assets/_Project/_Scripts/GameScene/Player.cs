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
    [SerializeField]
    Button catchButton;
    
    [Space(2.0f)]
    [Header("Raycast variable")]
    [SerializeField]
    float raycastDistance;
    [SerializeField]
    float coinDestroyDelay;
    [SerializeField]
    bool arrowPowerOn;

    public Button CatchButton { get => catchButton; set => catchButton = value; }

    private void Update()
    {
        // Casting a ray to detect 3D Object on raycastDistance range
        Ray rayOrigin = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hittarget;
        // if its hit something
        if (Physics.Raycast(rayOrigin, out hittarget, raycastDistance))
        {
            #region Debugging Raycast
            Debug.Log("hit something");
            Debug.Log(hittarget.collider.gameObject);
            Debug.DrawLine(arCamera.transform.position, hittarget.collider.transform.position, Color.blue, 2f);
            #endregion
            // crosshair animation play
            crossHair.color = Color.red;
            uiAnimator.SetBool("isDetecting", true);

            // if catch button is pressed
            //if (Input.touchCount > 0)
            //{
            //    Touch touch = Input.GetTouch(0);

            //    if (touch.phase == TouchPhase.Began && hittarget.collider != null)
            //    {
            //        Destroy(hittarget.collider.gameObject, coinDestroyDelay);
            //        Coin coin = hittarget.collider.gameObject.GetComponent<Coin>();
            //        coin.blinking = true;
            //        coin.coinTaken = true;
            //        coin.GetComponent<Collider>().enabled = false;
            //        //Debug.Log(hittarget.collider.gameObject.name);
                    
            //    }
            //}
        }
        else
        {
            crossHair.color = Color.green;
            uiAnimator.SetBool("isDetecting", false);
        }
    }

    public void CatchButtonOnClicked()
    {
        // Casting a ray to detect 3D Object on raycastDistance range
        Ray rayOrigin = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hittarget;
        // if its hit something
        if (Physics.Raycast(rayOrigin, out hittarget, raycastDistance))
        {

            if (arrowPowerOn)
            {
                //
                arrowPowerOn = false;
            }


            Destroy(hittarget.collider.gameObject, coinDestroyDelay);
            Coin coin = hittarget.collider.gameObject.GetComponent<Coin>();
            coin.blinking = true;
            coin.coinTaken = true;
            coin.GetComponent<Collider>().enabled = false;
            //Debug.Log(hittarget.collider.gameObject.name);

        }
    }
}
