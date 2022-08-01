using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class LoginManager1 : MonoBehaviour
{
    [Header("Controlling")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] RectTransform promptPanel;
    [SerializeField] RectTransform verificationCodePanel;

    [Header("Updating")]
    [SerializeField] TMP_Text promptText;
    [SerializeField] TMP_Text verificationCodeText;

    [Header("Subscribing")]
    [SerializeField] DataManager1 dataManager1;

    public bool promptIsOut = false;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }
    private void OnEnable()
    {
        dataManager1.OnEmailVerificationSent += ActionAfterEmailVerificationSent;
        dataManager1.OnRequiredFieldsAreEmpty += ActionIfRequiredFieldsAreEmpty;
        dataManager1.OnEmailAlreadyExist += ActionEmailAlreadyExist;
        dataManager1.OnVerificationCodeFailed += ActionVerificationFailed;
        dataManager1.OnRepeatRegistrationClicked += ActionRepeatRegistrationClicked;
    }
    private void OnDisable()
    {
        dataManager1.OnEmailVerificationSent -= ActionAfterEmailVerificationSent;
        dataManager1.OnRequiredFieldsAreEmpty -= ActionIfRequiredFieldsAreEmpty;
        dataManager1.OnEmailAlreadyExist -= ActionEmailAlreadyExist;
        dataManager1.OnVerificationCodeFailed -= ActionVerificationFailed;
    }
    private void Update()
    {
        // panel is up and screen touch 
        // panel animation play down
        if (Input.touchCount > 0)
        {
            if (promptPanel.anchoredPosition.y > -700f && promptIsOut)
            {
                promptPanel.DOAnchorPosY(-700f, 0.75f, true);
                promptIsOut = false;
            }
        }
    }
    public void GoBackButtonClicked()
    {
        SceneManager.LoadScene(2);
    }

    void ActionIfRequiredFieldsAreEmpty()
    {
        promptText.text = "Kolom email, password dan nomor telfon tidak boleh kosong, silahkan ulangi pendaftaran";
        promptPanel.DOAnchorPosY(0f, 0.75f, true);
        promptIsOut = true;
    }
    void ActionEmailAlreadyExist()
    {
        promptText.text = "Email yang Anda gunakan sudah terdaftar di sistem kami, silahkan kembali ke menu Login";
        promptPanel.DOAnchorPosY(0f, 0.75f, true);
        promptIsOut = true;
    }
    void ActionAfterEmailVerificationSent()
    {
        verificationCodeText.text = "Silahkan masukkan 6 nomor verifikasi yang terkirim ke email Anda";
        verificationCodePanel.DOAnchorPosY(0f, 0.75f, true);
    }
    void ActionVerificationFailed()
    {
        verificationCodeText.text = "Kode yang Anda masukkan salah silahkan cek email Anda \natau klik ulangi pendaftaran untuk memulai pendaftaran baru";
    }
    void ActionRepeatRegistrationClicked()
    {
        verificationCodePanel.DOAnchorPosY(-700f, 0.75f, true);
    }

}

