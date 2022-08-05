using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicked : MonoBehaviour
{
    [SerializeField]
    AudioClip clickedAudioClip;
    [SerializeField]
    AudioSource thisButtonAudioSource;
    [SerializeField]
    Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButtonAudioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    { 
        thisButton.onClick.AddListener( delegate { PlayClickedSound(); });
    }
    private void OnDisable()
    {
        thisButton.onClick.RemoveListener(delegate { PlayClickedSound(); });
    }
    void PlayClickedSound()
    {
        thisButtonAudioSource.PlayOneShot(clickedAudioClip);
    }
}
