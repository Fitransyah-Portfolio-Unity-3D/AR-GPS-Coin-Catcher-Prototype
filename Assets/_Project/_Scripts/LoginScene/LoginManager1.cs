using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class LoginManager1 : MonoBehaviour
{
    [Header("Controlling")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] RectTransform promptPanelCanvas;

    [Header("Updating")]
    [SerializeField] TMP_Text promptText;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }

    private void Update()
    {
        // panel is up and screen touch 
        // panel animation play down
        if (Input.touchCount > 0)
        {
            if (promptPanelCanvas.anchoredPosition.y > -700f)
            {
                promptPanelCanvas.DOAnchorPosY(-700f, 0.75f, true);
                Scene thisScene =  SceneManager.GetActiveScene();
                SceneManager.LoadScene(thisScene.buildIndex);
            }
        }
        
    }
    public void GoBackButtonClicked()
    {
        SceneManager.LoadScene(2);
    }


    string textFor406 = "Email yang anda gunakan sudah terdaftar.\n Silahkan masuk melalui 'Login page'";
    string textFor201 = "Anda berhasil terdaftar!\n Silahkan masuk dengan akun baru Anda";
    string textFor400 = "Data yang Anda masukkan salah, silahkan pastikan mengisi email, nomor telfon dan password";
    public void ActionAfterRequest(string statusCode)
    {
        //switch (statusCode)
        //{
        //    case 406:
                
        //        // prompt panel animation up
        //        // update text from panel up
        //        promptText.text = textFor406;
        //        if (promptPanelCanvas.anchoredPosition.y == -700f)
        //        {
        //            promptPanelCanvas.DOAnchorPosY(0f, 0.75f, true);
        //        }
        //        break;

        //        case 201:
        //        // prompt panel animation up
        //        // update text from panel up
        //        promptText.text = textFor201;
        //        if (promptPanelCanvas.anchoredPosition.y == -700f)
        //        {
        //            promptPanelCanvas.DOAnchorPosY(0f, 0.75f, true);
        //        }
        //        break;
        //        case 400:
        //        // prompt panel animation up
        //        // update text from panel up
        //        promptText.text = textFor400;
        //        if (promptPanelCanvas.anchoredPosition.y == -700f)
        //        {
        //            promptPanelCanvas.DOAnchorPosY(0f, 0.75f, true);
        //        }
        //        break;

        //    default: return;
        //}


    }
}

