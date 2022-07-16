using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Image crossHair;

    private void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Debug.Log("Touch happening");
        //        Ray rayOrigin = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f) );
        //        RaycastHit hitTarget;
        //        if (Physics.Raycast(rayOrigin,out hitTarget , 10f))
        //        {
        //            Debug.Log("Hit something");
        //            Debug.Log(hitTarget.collider.gameObject);
        //            Debug.DrawLine(arCamera.transform.position, hitTarget.collider.transform.position, Color.blue , 2f);
        //        }



        //    }
        //}

        Ray rayOrigin = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hittarget;
        if (Physics.Raycast(rayOrigin, out hittarget, 10f))
        {
            Debug.Log("hit something");
            Debug.Log(hittarget.collider.gameObject);
            Debug.DrawLine(arCamera.transform.position, hittarget.collider.transform.position, Color.blue, 2f);
            crossHair.color = Color.yellow;
        }
        else
        {
            crossHair.color = Color.green;
        }

    }


}
