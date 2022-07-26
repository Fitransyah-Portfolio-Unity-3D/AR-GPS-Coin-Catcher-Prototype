using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Controlling")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] Image audioButton;


    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }

    public void BacksoundToggle()
    {
        if (!audioManager.backsound.isPlaying)
        {
            audioManager.backsound.Play();
            audioButton.color = new Color(audioButton.color.r, audioButton.color.g, audioButton.color.b, 1f);

        }
        else
        {
            audioManager.backsound.Stop();
            audioButton.color = new Color(audioButton.color.r, audioButton.color.g, audioButton.color.b, 0.25f);
            
        }
        
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
