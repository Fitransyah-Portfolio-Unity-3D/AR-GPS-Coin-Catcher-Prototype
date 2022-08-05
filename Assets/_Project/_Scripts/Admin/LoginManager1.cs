using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager1 : MonoBehaviour
{
    [Header("Controlling")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] CanvasGroup promptPanel;
    [SerializeField] CanvasGroup verificationPanel;

    [Header("Updating")]
    [SerializeField] TMP_Text promptText;
    [SerializeField] TMP_Text verificationText;

    [Header("Subscribing")]
    [SerializeField] DataManager1 dataManager1;

    public event Action OnSubmitCodeButtonClicked;

    float currentAlphaPromptPanel;
    float desiredAlphaPromptPanel;
    float currentAlphaVerificationPanel;
    float desiredAlphaPVerificationPanel;
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
        currentAlphaPromptPanel = 0;
        desiredAlphaPromptPanel = 0;
        currentAlphaVerificationPanel = 0;
        desiredAlphaPVerificationPanel = 0;
        promptPanel.gameObject.SetActive(false);
        verificationPanel.gameObject.SetActive(false);  
    }
    private void Update()
    {
        promptPanel.alpha = currentAlphaPromptPanel;
        currentAlphaPromptPanel = Mathf.MoveTowards(currentAlphaPromptPanel,desiredAlphaPromptPanel, fadeTime * Time.deltaTime);
        verificationPanel.alpha = currentAlphaVerificationPanel;
        currentAlphaVerificationPanel = Mathf.MoveTowards(currentAlphaVerificationPanel,desiredAlphaPVerificationPanel, fadeTime * Time.deltaTime);
    }
    private void OnEnable()
    {
        dataManager1.OnEmailVerificationSent += ActionAfterEmailVerificationSent;
        dataManager1.OnRequiredFieldsAreEmpty += ActionIfRequiredFieldsAreEmpty;
        dataManager1.OnEmailAlreadyExist += ActionEmailAlreadyExist;
        dataManager1.OnVerificationCodeFailed += ActionVerificationFailed;
        dataManager1.OnRegistrationSuccesfull += ActionRegistrationSuccesfull;
    }
    private void OnDisable()
    {
        dataManager1.OnEmailVerificationSent -= ActionAfterEmailVerificationSent;
        dataManager1.OnRequiredFieldsAreEmpty -= ActionIfRequiredFieldsAreEmpty;
        dataManager1.OnEmailAlreadyExist -= ActionEmailAlreadyExist;
        dataManager1.OnVerificationCodeFailed -= ActionVerificationFailed;
        dataManager1.OnRegistrationSuccesfull -= ActionRegistrationSuccesfull;
    }

    public void GoBackButtonClicked()
    {
        SceneManager.LoadScene(2);
    }
    public void OnClickNextPromptButton()
    {
        desiredAlphaPromptPanel = 0f;
        Invoke(invokeGamebjectNotActive, fadeTime);
    }
    public void OnSubmitCodeClicked()
    {
        OnSubmitCodeButtonClicked();
    }
    public void OnRepeatRegistrationClicked()
    {
        desiredAlphaPVerificationPanel = 0f;
        Invoke(invokeGamebjectNotActive, fadeTime);
    }
    void ActionIfRequiredFieldsAreEmpty()
    {
        promptText.text = "Kolom email, password dan nomor telfon tidak boleh kosong, silahkan ulangi pendaftaran";
        desiredAlphaPromptPanel = 1f;
        promptPanel.gameObject.SetActive(true);
    }
    void ActionEmailAlreadyExist()
    {
        promptText.text = "Email yang Anda gunakan sudah terdaftar di sistem kami, silahkan kembali ke menu Login";
        desiredAlphaPromptPanel = 1f;
        promptPanel.gameObject.SetActive(true);
    }
    void ActionAfterEmailVerificationSent()
    {
        verificationText.text = "Silahkan masukkan 6 nomor verifikasi yang terkirim ke email Anda";
        desiredAlphaPVerificationPanel = 1f;
        verificationPanel.gameObject.SetActive(true);
    }
    void ActionVerificationFailed()
    {
        verificationText.text = "Kode yang Anda masukkan salah silahkan cek email Anda \natau klik ulangi pendaftaran untuk memulai pendaftaran baru";
        desiredAlphaPVerificationPanel= 1f;
        verificationPanel.gameObject.SetActive(true );
    }
    void ActionRegistrationSuccesfull()
    {
        SceneManager.LoadScene(3);
    }
    void PrompSetNotActive()
    {
        promptPanel.gameObject.SetActive(false);
        verificationPanel.gameObject.SetActive(false);
    }
    // TODO :
    // save email in player prefs for in game web server request
}

