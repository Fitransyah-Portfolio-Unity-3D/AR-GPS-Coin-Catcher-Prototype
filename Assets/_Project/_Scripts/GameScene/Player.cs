using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Image crossHair;
    [SerializeField]
    private Animator uiAnimator;

    [SerializeField]
    float raycastDistance;
    [SerializeField]
    float destroyTime;

    private void Update()
    {
        Ray rayOrigin = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hittarget;
        if (Physics.Raycast(rayOrigin, out hittarget, raycastDistance))
        {
            #region Debugging Raycast
            Debug.Log("hit something");
            Debug.Log(hittarget.collider.gameObject);
            Debug.DrawLine(arCamera.transform.position, hittarget.collider.transform.position, Color.blue, 2f);
            #endregion

            crossHair.color = Color.red;
            uiAnimator.SetBool("isDetecting", true);
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && hittarget.collider != null)
                {
                    Destroy(hittarget.collider.gameObject, destroyTime);
                    Coin coin = hittarget.collider.gameObject.GetComponent<Coin>();
                    coin.blinking = true;
                    coin.coinTaken = true;
                    coin.GetComponent<Collider>().enabled = false;
                    //Debug.Log(hittarget.collider.gameObject.name);
                    
                }
            }
        }
        else
        {
            crossHair.color = Color.green;
            uiAnimator.SetBool("isDetecting", false);
        }
    }
}
