using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Web;
using Newtonsoft.Json;

// This class placed in independent Object called DataManager1
// Exist in hierarchy

// This class handle : 
// Retrieving data from Unity UI Input field
// Make a POST web request to server
// follow with necessary action based on status code or data response


public class DataManager1 : MonoBehaviour
{
    [Header("Register Request setting")]
    string serverEndpoint = "https://app.xrun.run/gateway.php?";
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_Text phoneCode;
    [SerializeField] TMP_InputField phoneNumber;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TMP_InputField region;
    [SerializeField] TMP_InputField referral;
    public string ageValue = null;
    public string sexValue = null;
    [SerializeField] TMP_InputField verificationCodeInputField;

    JoinForm newMember;

    public event Action OnRequiredFieldsAreEmpty;
    public event Action OnEmailAlreadyExist;
    public event Action OnEmailVerificationSent;
    public event Action OnVerificationCodeFailed;
    public event Action OnRepeatRegistrationClicked;

    [Header("Updating")]
    [SerializeField] LoginManager1 loginManager;

    [Header("Controlling")]
    [SerializeField] Button registerButton;
    [SerializeField] Button submitVerificationCodeButton;
    [SerializeField] Button repeatRegistrationButton;

    public void OnClickRegisterButton()
    {
        if (IsRequiredFieldsAreEmpty())
        {
            OnRequiredFieldsAreEmpty();
        }
        else
        {
            StartCoroutine(CheckExistUser());
        }
        
    }
    bool IsRequiredFieldsAreEmpty()
    {
        if (String.IsNullOrEmpty(email.text.ToString()))
        {
            return true;
        }
        else if (String.IsNullOrEmpty(passwordInputField.text.ToString()))
        {
            return true;
        }
        else if (String.IsNullOrEmpty(phoneNumber.text.ToString()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator CheckExistUser()
    {
        // building query
        string endpoint = serverEndpoint;
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "login-04-email";
        query["email"] = email.text.ToString();
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();

        // checking email existence in server
        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {
            // button sign up non interactable
            registerButton.interactable = false;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // cahching request response
                var rawData = www.downloadHandler.text;
                ResponseData responseData = new ResponseData();
                responseData = JsonConvert.DeserializeObject<ResponseData>(rawData);
                Debug.Log(responseData.Data);
                if (responseData.Data == "true")
                {
                    OnEmailAlreadyExist();
                }
                else if (responseData.Data == "false")
                {
                    // if email not exist in server continue process
                    StartCoroutine(EmailVerification());
                }

            }
        }
        // button sign up interactable
        registerButton.interactable = true;
        Debug.Log("CheckExistUser Finish");
    }
    IEnumerator EmailVerification()
    {
        // store registration data in instance
        newMember = new JoinForm();
        newMember.Email = email.text.ToString();
        newMember.Pin = passwordInputField.text.ToString();
        newMember.Mobilecode = phoneCode.text.ToString();
        newMember.Mobile = phoneNumber.text.ToString();
        newMember.Countrycode = "ID";
        newMember.Region = region.text.ToString();
        newMember.Age = ageValue;
        newMember.Sex = sexValue;
        

        // building query
        string endpoint = serverEndpoint;
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "check-02-email";
        query["email"] = newMember.Email ;
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();

        // sending verification email
        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {
            // button sign up non interactable
            registerButton.interactable = false;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // if request succesfull bring up email verfication code panel
                OnEmailVerificationSent();
                
            }

            // button sign up interactable
            registerButton.interactable = true;

        }
        Debug.Log("EmailVerification Finish");
    }

    public void OnClickSubmitVerificationCodeButton()
    {
        StartCoroutine(EmailPasscodeVerification());
    }

    IEnumerator EmailPasscodeVerification()
    {
        // building query
        string endpoint = "https://app.xrun.run/gateway.php?";
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "login-03-email";
        query["email"] = newMember.Email;
        query["code"] = verificationCodeInputField.text;
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();

        // requesting email verification
        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {
            // both button non interactable
            submitVerificationCodeButton.interactable = false;
            repeatRegistrationButton.interactable = false;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // cahching request response
                var rawData = www.downloadHandler.text;
                ResponseData responseData = new ResponseData();
                responseData = JsonConvert.DeserializeObject<ResponseData>(rawData);
                Debug.Log(responseData.Data);
                if (responseData.Data == "false")
                {
                    // update text verification panel
                    // let user click button
                    OnVerificationCodeFailed();
                }
                else if (responseData.Data == "ok")
                {
                    // continue to next sequence
                    // register the user and login game
                }
            }

            // both button interactable
            submitVerificationCodeButton.interactable = true;
            repeatRegistrationButton.interactable = true;

        }

    }
    public void OnClickRepeatRegisterButton()
    {
        OnRepeatRegistrationClicked();
    }
    public class ResponseData
    {
        public string Data { get; set; }   
        public string Value { get; set; }
    }
    public class JoinForm
    {
        public string Email { get; set; }
        public string  Pin { get; set; }
        public string  Firstname { get; set; }
        public string  Lastname { get; set; }
        public string  Mobile { get; set; }
        public string  Mobilecode { get; set; }
        public string Countrycode { get; set; }
        public string Region{ get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string  Country { get; set; }
    }
}   
