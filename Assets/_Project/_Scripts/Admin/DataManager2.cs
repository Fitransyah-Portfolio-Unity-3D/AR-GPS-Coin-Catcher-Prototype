using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Web;

public class DataManager2 : MonoBehaviour
{
    [Header("Login Request Setting")]
    string serverEndpoint = "https://app.xrun.run/gateway.php?";
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_InputField password;

    public event Action OnRequiredFieldsAreEmpty;
    public event Action OnLoginSuccesfull;
    public event Action OnLoginFailed;

    [Header("Controlling")]
    [SerializeField] Button loginButton;

    public void OnClickLoginButton()
    {
        
        if (IsRequiredFieldsAreEmpty())
        {
            OnRequiredFieldsAreEmpty();
            
        }
        if (!IsRequiredFieldsAreEmpty())
        {
            StartCoroutine(SendLoginRequest());
        }
        
    }
    bool IsRequiredFieldsAreEmpty()
    {
        if (String.IsNullOrEmpty(email.text.ToString()))
        {
            return true;
        }
        else if (String.IsNullOrEmpty(password.text.ToString()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator SendLoginRequest()
    {
        // building query
        string endpoint = serverEndpoint;
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "login-checker";
        query["email"] = email.text.ToString();
        query["pin"] = password.text.ToString();
        query["code"] = "3114";
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();

        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {
            loginButton.interactable = false;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                if (response == "OK")
                {
                    PlayerPrefs.SetString("email", email.text.ToString());
                    OnLoginSuccesfull();
                    
                }
                else
                {
                    OnLoginFailed();
                }
            }
            loginButton.interactable = true;
        }
    }

}
