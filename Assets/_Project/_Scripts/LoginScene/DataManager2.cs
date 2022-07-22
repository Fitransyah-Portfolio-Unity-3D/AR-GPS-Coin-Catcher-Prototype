using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Collections.Generic;
using System.Text;

using Models;
using Proyecto26;
using UnityEditor;

public class DataManager2 : MonoBehaviour
{
   


    [Header("Login POST Request Setting")]
    [SerializeField] TMP_Text email;
    [SerializeField] TMP_Text password;
    [SerializeField] string loginUrl;

    [Header("Updating")]
    [SerializeField] LoginManager2 loginManager;

    [Header("Controlling")]
    [SerializeField] Button loginButton;

    public void OnClickLoginButton()
    {
        Debug.Log(email.text);
        //Debug.Log(phoneNumber.text);
        Debug.Log(password.text);

        StartCoroutine(SendLoginRequest());
    }

    IEnumerator SendLoginRequest()
    {
        //WWWForm form = new WWWForm();

        //form.AddField("email", email.text.ToString());
        //form.AddField("password", password.text.ToString());


        //form.AddField("tp", "4");
        //form.AddField("act", "4");


        //using (UnityWebRequest www = UnityWebRequest.Post(loginUrl, form))
        //{
        //    // button sign up non interactable
        //    loginButton.interactable = false;
        //    www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        //    yield return www.SendWebRequest();

        //    if (www.result != UnityWebRequest.Result.Success)
        //    {
        //        Debug.Log(www.error);
        //    }
        //    else
        //    {
        //        Debug.Log("Request complete with status");

        //    }
        //    Debug.Log(www.responseCode);
        //    Debug.Log("Request Result : " + www.result);

        //    loginManager.ActionAfterRequest(www.responseCode);
        //    // button sign up interactable
        //    loginButton.interactable = true;

        //}

        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("act=login-01&email=ggg@hhh.com&pin=111!!!aaaAAA&TP=1"));
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        //UnityWebRequest www = UnityWebRequest.Post("https://app.xrun.run/gateway.php", formData);


        //www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(formData));
        //www.uploadHandler.contentType = "application/json";

        //yield return www.SendWebRequest();

        //if (www.result != UnityWebRequest.Result.Success)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Form upload complete!");
        //}
        //Debug.Log(www.downloadHandler.text);
        //Debug.Log(www.responseCode);




        //------------------------------------------------------

        //formData.Add(new MultipartFormDataSection("act=login-01&email=ggg@hhh.com&pin=111!!!aaaAAA&TP=1"));

        string path = "https://app.xrun.run/gateway.php?act=login-checker&email=TestTestTest@Test.com&pin=xrun123";
        //string path = "https://app.xrun.run/gateway.php?act=login-01&email=dbals33333@naver.com&pin=xrun123!&member=1680&extrastr=2f0zCZLdWk&tp=1&mobile=456398731";
        //string parameter = "act=login-checker&email=TestTestTest@Test.com&pin=xrun123!";

        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                loginManager.ActionAfterRequest(www.downloadHandler.text);
            }
        }
        


    }

}
