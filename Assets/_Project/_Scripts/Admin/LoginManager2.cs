using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;



public class LoginManager2 : MonoBehaviour
{
    [Header("Controlling")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] CanvasGroup promptPanel;
    

    [Header("Updating")]
    [SerializeField] TMP_Text promptText;

    [Header("Subscribing")]
    [SerializeField] DataManager2 dataManager2;

    float currentAlpha;
    float desiredAlpha;
    public float fadeTime;
    string invokeGamebjectNotActive = "PrompSetNotActive";


    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }   
    }
    private void Start()
    {
        currentAlpha = 0;
        desiredAlpha = 0;
        promptPanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        promptPanel.alpha = currentAlpha;
        currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, fadeTime * Time.deltaTime);
    }
    private void OnEnable()
    {
        dataManager2.OnRequiredFieldsAreEmpty += ActionIfRequiredFieldsAreEmpty;
        dataManager2.OnLoginSuccesfull += ActionLoginSuccesfull;
        dataManager2.OnLoginFailed += ActionLoginFailed;
    }
    private void OnDisable()
    {
        dataManager2.OnRequiredFieldsAreEmpty -= ActionIfRequiredFieldsAreEmpty;
        dataManager2.OnLoginSuccesfull -= ActionLoginSuccesfull;
        dataManager2.OnLoginFailed -= ActionLoginFailed;
    }
    public void GoBackButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
    public void KlikDiSini()
    {
        Debug.Log("Go to forgot password page!");
    }
    public void DaftarDiSini()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickNextPromptButton()
    {
        desiredAlpha = 0f;
        Invoke(invokeGamebjectNotActive, fadeTime);
    }
    void ActionLoginSuccesfull()
    {
        SceneManager.LoadScene(3);
    }
    void ActionLoginFailed()
    {
        promptPanel.gameObject.SetActive(true);
        desiredAlpha = 1f;
        promptText.text = "Data Anda tidak valid, silahkan ulangi atau daftar melalui halaman pendaftaran";

    }
    void ActionIfRequiredFieldsAreEmpty()
    {
        promptPanel.gameObject.SetActive(true);
        desiredAlpha = 1f;
        promptText.text = "Kolom email dan password tidak boleh kosong, silahkan ulangi pendaftaran";
    }
    void PrompSetNotActive()
    {
        promptPanel.gameObject.SetActive(false);
    }
    // TODO :
    // save email in player prefs for in game web server request


}
