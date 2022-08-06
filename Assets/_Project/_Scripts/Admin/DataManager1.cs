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
// Communicating with server for registration process


public class DataManager1 : MonoBehaviour
{
    [Header("Register Request setting")]
    string serverEndpoint = "https://app.xrun.run/gateway.php?";
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_Text phoneCode;
    [SerializeField] TMP_InputField phoneNumber;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TMP_Text region;
    [SerializeField] TMP_InputField referral;
    public string ageValue = null;
    public string sexValue = null;
    [SerializeField] TMP_InputField verificationCodeInputField;

    RegistrationForm newMember;

    public event Action OnRequiredFieldsAreEmpty;
    public event Action OnEmailAlreadyExist;
    public event Action OnEmailVerificationSent;
    public event Action OnVerificationCodeFailed;
    public event Action OnRegistrationSuccesfull;

    [Header("Updating")]
    [SerializeField] LoginManager1 loginManager;

    [Header("Controlling")]
    [SerializeField] Button registerButton;
    [SerializeField] Button submitVerificationCodeButton;
    [SerializeField] Button repeatRegistrationButton;
    private void OnEnable()
    {
        loginManager.OnSubmitCodeButtonClicked += OnClickSubmitVerificationCodeButton;
    }
    private void OnDisable()
    {
        loginManager.OnSubmitCodeButtonClicked -= OnClickSubmitVerificationCodeButton;
    }

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
        newMember = new RegistrationForm();
        newMember.Email = email.text.ToString();
        newMember.Pin = passwordInputField.text.ToString();
        newMember.Mobilecode = phoneCode.text.ToString();
        newMember.Mobile = phoneNumber.text.ToString();
        newMember.Countrycode = "ID";

        string fullRegionString = region.text.ToString();
        int startIndex = fullRegionString.IndexOf("(");
        int endIndex = fullRegionString.IndexOf(")");
        string regionCode = fullRegionString.Substring(startIndex +1 , endIndex - startIndex -1);
        newMember.Region = regionCode;
        newMember.Age = ageValue; //Int16.Parse(ageValue);
        newMember.Gender = sexValue; //Int16.Parse(sexValue);

        Debug.Log(newMember.Age + newMember.Gender);

        var jsonNewMember = JsonConvert.SerializeObject(newMember);
        Debug.Log(jsonNewMember.ToString());

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
                Debug.Log(www.downloadHandler.text);
                // if request succesfull bring up email verfication code panel
                OnEmailVerificationSent();
                
            }

            // button sign up interactable
            registerButton.interactable = true;

        }
        Debug.Log("EmailVerification Finish");
    }

    void OnClickSubmitVerificationCodeButton()
    {
        StartCoroutine(EmailPasscodeVerification());
    }

    IEnumerator EmailPasscodeVerification()
    {
        // building query
        string endpoint = serverEndpoint;
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
                    StartCoroutine(RegisterNewMember());
                }
            }

            // both button interactable
            submitVerificationCodeButton.interactable = true;
            repeatRegistrationButton.interactable = true;

        }

    }
    IEnumerator RegisterNewMember()
    {
        Debug.Log(newMember.Mobilecode);
        Debug.Log(newMember.Region);
        
        // building query
        string endpoint = serverEndpoint;
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "login-05-joinAndAccount";
        query["email"] = newMember.Email;
        query["pin"] = newMember.Pin;
        query["mobilecode"] = newMember.Mobilecode;
        query["mobile"] = newMember.Mobile;
        query["countrycode"] = "ID";
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
                Debug.Log("Registration result : \n" + www.downloadHandler.text);
                //PlayerPrefs.SetString("email", newMember.Email);
                OnRegistrationSuccesfull();             
            }

            // both button interactable
            submitVerificationCodeButton.interactable = true;
            repeatRegistrationButton.interactable = true;

        }
    }
    public class ResponseData
    {
        public string Data { get; set; }   
        public string Value { get; set; }
    }
    public class RegistrationForm
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
        public string  Country { get; set; }
    }
}   
