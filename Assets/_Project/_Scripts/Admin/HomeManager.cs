using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Web;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class HomeManager : MonoBehaviour
{
    string serverEndpoint = "https://app.xrun.run/gateway.php?";
    private void Start()
    {
        string playerEmail = PlayerPrefs.GetString("email");
        StartCoroutine(StorePlayerData(playerEmail));
    }
    public void PlayButtonClicked()
    {
        SceneManager.LoadScene(4);
    }
    public void MyXrunButtonClicked()
    {
        SceneManager.LoadScene(8);
    }
    public void MyAdsButtonClicked()
    {
        SceneManager.LoadScene(5);
    }
    public void ShopButtonClicked()
    {
        SceneManager.LoadScene(6);
    }
    public void SettingButtonClicked()
    {
        SceneManager.LoadScene(7);
    }
    

    IEnumerator StorePlayerData(string email)
    {
        // building query
        string endpoint = serverEndpoint;
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "login-04-email";
        query["email"] = email;
        query["code"] = "3114";

        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();

        // requesting email verification
        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {
            // sending data request
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // cahching request response
                var rawData = www.downloadHandler.text;
                PlayerData responseData = new PlayerData();
                responseData = JsonConvert.DeserializeObject<PlayerData>(rawData);

                PlayerDataStatic.SetEmail(responseData.email);
                PlayerDataStatic.SetMemberNumber(responseData.member);
                PlayerDataStatic.SetFirstName(responseData.firstname);
                PlayerDataStatic.SetLastName(responseData.lastname);
                PlayerDataStatic.SetGender(responseData.gender);
                PlayerDataStatic.SetExtrastr(responseData.extrastr);
                PlayerDataStatic.SetMobileCode(responseData.mobilecode);
                PlayerDataStatic.SetCountry(responseData.country);
                PlayerDataStatic.SetCountryCode(responseData.countrycode);
                PlayerDataStatic.SetRegion(responseData.region);
                PlayerDataStatic.SetAges(responseData.ages);
            }
        }
    }
}
