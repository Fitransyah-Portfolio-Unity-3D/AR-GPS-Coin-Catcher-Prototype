using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoginManager0 : MonoBehaviour
{
    [SerializeField]
    float fillingSpeed;

    [SerializeField]
    Canvas mainScreen;
    [SerializeField]
    Image mainLogo;
    [SerializeField]
    Canvas loginCreateMenu;
    [SerializeField]
    AudioManager audioManager;
    [SerializeField]
    Button loginButton;
    [SerializeField]
    Button registerButton;

    public bool logoFinished;

    private void Awake()
    {
        loginButton.interactable = false;
        registerButton.interactable = false;
    }

    void Start()
    {
        mainScreen.gameObject.SetActive(true);
        if(audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }

    void Update()
    {
        if (mainLogo.fillAmount < 1f && mainLogo != null)
        {
            mainLogo.fillAmount += 1f * fillingSpeed * Time.deltaTime;
        }
        if (mainLogo.fillAmount == 1f && !logoFinished)
        {
            // start sequence
            StartCoroutine(FirstSequence()) ;
            logoFinished = true;
        }
    }
    IEnumerator FirstSequence()
    {
        yield return new WaitForSeconds(0.5f);
        mainScreen.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //loginManagerAudioSource.PlayOneShot(loginCreateMenuClip);
        audioManager.onePlay.PlayOneShot(audioManager.loginCreateMenuClip);
        loginCreateMenu.gameObject.SetActive (true);
        yield return new WaitForSeconds(5f);
        audioManager.backsound.Play();
        loginButton.interactable = true;
        registerButton.interactable = true;
    }
    public void OnRegisterButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
    public void OnLoginButtonClicked()
    {
        SceneManager.LoadScene(2);
    }

}
