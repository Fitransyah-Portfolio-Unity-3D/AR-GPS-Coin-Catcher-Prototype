using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;



public class LoginManager2 : MonoBehaviour
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
                Scene thisScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(thisScene.buildIndex);
            }
        }

    }
    public void GoBackButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    string textFor401 = "Anda belum terdaftar, silahkan ke halaman pendafataran";
    public void ActionAfterRequest(string statusCode)
    {
        //switch (statusCode)
        //{
        //    case 200:

        //        // Go to Game
        //        SceneManager.LoadScene(3);
        //        break;

        //    case 401:
        //        // prompt panel animation up
        //        // update text from panel up
        //        promptText.text = textFor401;
        //        if (promptPanelCanvas.anchoredPosition.y == -700f)
        //        {
        //            promptPanelCanvas.DOAnchorPosY(0f, 0.75f, true);
        //        }
        //        break;
        //    default: return;
        //}
        promptText.text = statusCode;
        if (promptPanelCanvas.anchoredPosition.y == -700f)
        {
            promptPanelCanvas.DOAnchorPosY(0f, 0.75f, true);
        }
    }




    public void KlikDiSini()
    {
        Debug.Log("Go to forgot password page!");
    }
    public void DaftarDiSini()
    {
        SceneManager.LoadScene(1);
    }
}
