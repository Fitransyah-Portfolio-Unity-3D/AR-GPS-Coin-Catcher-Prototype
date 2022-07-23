using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

// This class placed in independent Object called DataManager1
// Exist in hierarchy

// This class handle : 
// Retrieving data from Unity UI Input field
// Make a POST web request to server
// follow with necessary action based on status code or data response


public class DataManager1 : MonoBehaviour
{
    [Header("Register POST Request setting")]
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_Text phoneCode;
    [SerializeField] TMP_InputField phoneNumber;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TMP_InputField region;
    [SerializeField] TMP_InputField referral;
    public string ageValue = null;
    public string sexValue = null;
    //[SerializeField] string registerUrl;
    // string registerEndpoint = "https://app.xrun.run/gateway.php?";


    [Header("Updating")]
    [SerializeField] LoginManager1 loginManager;

    [Header("Controlling")]
    [SerializeField] Button registerButton;

    private void Update()
    {
        // check if any field null or empty when pressing register button
        // call window prompt with warning text
        // return window prompt if screen is clicked
    }

    public void OnClickRegisterButton()
    {
        StartCoroutine(SendRegisterRequest());
    }

    IEnumerator SendRegisterRequest()
    {
               
        WWWForm form = new WWWForm();

        form.AddField("email", email.text.ToString());
        form.AddField("password", passwordInputField.text.ToString());
        form.AddField("phoneNumber", phoneNumber.text.ToString());
        form.AddField("region", region.text.ToString());
        form.AddField("phoneCode", phoneCode.text.ToString());
        form.AddField("age", ageValue);
        form.AddField("sex", sexValue);

        // url or endpoint not fix yet
        using (UnityWebRequest www = UnityWebRequest.Post("www.google.com", form))
        {
            // button sign up non interactable
            registerButton.interactable = false;
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Request complete with status");
                
            }
            Debug.Log(www.downloadHandler.text);                    
            loginManager.ActionAfterRequest(www.downloadHandler.text);
            // devide with server team are we using status code?
            // server need to make status code

            // button sign up interactable
            registerButton.interactable = true;

        }
        
    }
}   
