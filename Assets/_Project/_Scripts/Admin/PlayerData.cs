using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerData : MonoBehaviour
{
    IEnumerator GetPlayerData(string email)
    {
        // building query
        string endpoint = "https://app.xrun.run/gateway.php?";
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "login-04-email";
        query["email"] = email;
        query["code"] = "3114";
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();

        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;


            }

        }
    }
}
public class PlayerDataStructure
{
    public string data;
    public string email;
    public string member;
    public string firstname;
    public string lastname;
    public string gender;
    public string extrastr;
    public string mobilecode;
    public string country;
    public string countrycode;
    public string region;
    public string ages;
}
