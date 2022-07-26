using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance = null;
    
    public AudioClip loginCreateMenuClip;
    public AudioClip backsoundClip;
    public AudioClip coinTakenClip;
    public AudioSource onePlay;
    public AudioSource backsound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        } 
    }
    private void Start()
    {
        backsound.clip = backsoundClip;
    }
    public void UICoinTakenSound()
    {
        onePlay.PlayOneShot(coinTakenClip);
    }

}
