using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;




public class DataManager1 : MonoBehaviour
{
    [Header("Register POST Request setting")]
    [SerializeField] TMP_Text email; 
    [SerializeField] TMP_Text phoneNumber;
    [SerializeField] TMP_Text password;
    [SerializeField] TMP_Text region;
    [SerializeField] TMP_Text  referral;
    [SerializeField] TMP_Text phoneCode;
    public string ageValue = null;
    public string sexValue = null;
    [SerializeField] string registerUrl;

    [Header("Updating")]
    [SerializeField] LoginManager1 loginManager;

    [Header("Controlling")]
    [SerializeField] Button registerButton;


    public void OnClickRegisterButton()
    {
        //Debug.Log(email.text);
        //Debug.Log(phoneNumber.text);
        //Debug.Log(password.text);
        //Debug.Log(region.text);
        //Debug.Log(referral.text);
        //Debug.Log(phoneCode.text);
        //Debug.Log(ageValue);
        //Debug.Log(sexValue);

        StartCoroutine(SendRegisterRequest());
    }

    IEnumerator SendRegisterRequest()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", email.text.ToString());
        form.AddField("password", password.text.ToString());
        form.AddField("phoneNumber", phoneNumber.text.ToString());
        form.AddField("region", region.text.ToString());
        form.AddField("phoneCode", phoneCode.text.ToString());
        form.AddField("age", ageValue);
        form.AddField("sex", sexValue);

        using (UnityWebRequest www = UnityWebRequest.Post(registerUrl, form))
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
            Debug.Log(www.responseCode);
            Debug.Log("Request Result : " + www.result);
            
            loginManager.ActionAfterRequest(www.downloadHandler.text);
            // button sign up interactable
            registerButton.interactable = true;

        }
    }
}   
