using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Collections.Generic;
using System.Text;



public class DataManager2 : MonoBehaviour
{
   


    [Header("Login POST Request Setting")]
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_InputField password;
    string loginEndpoint = "https://app.xrun.run/gateway.php?";

    [Header("Updating")]
    [SerializeField] LoginManager2 loginManager;

    [Header("Controlling")]
    [SerializeField] Button loginButton;

    public void OnClickLoginButton()
    {
        //Debug.Log(email.text);
        //Debug.Log(password.text);


        StartCoroutine(SendLoginRequest());
    }

    IEnumerator SendLoginRequest()
    {
        // API Name : login_API/login-checker-email ==> Valid email
        // sample parameter
        //"act=login-01&email=ggg@hhh.com&pin=111!!!aaaAAA&TP=1"
        //string parameter = "https://app.xrun.run/gateway.php?act=login-checker&email=TestTestTest@StartPopulateCoins.com&pin=xrun123!";
        //string parameter = "https://app.xrun.run/gateway.php?act=login-01&email=dbals33333@naver.com&pin=xrun123!&member=1680&extrastr=2f0zCZLdWk&tp=1&mobile=456398731";
        //string parameterOnly = "act=login-checker&email=TestTestTest@StartPopulateCoins.com&pin=xrun123!";
        string paramsOne = $"act=login-checker&";
        string paramsTwo = $"email={email.text}&";
        string paramsThree = $"pin={password.text}";

        using (UnityWebRequest www = UnityWebRequest.Get(loginEndpoint+paramsOne+paramsTwo+paramsThree))
        {
            loginButton.interactable = false;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                loginManager.ActionAfterRequest(www.downloadHandler.text);
            }
            loginButton.interactable = true;
        }
    }

}
