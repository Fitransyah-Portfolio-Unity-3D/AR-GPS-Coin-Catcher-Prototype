using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
using TMPro;

// This class placed in independent Object called CoinManager
// Exist in hierarchy

// This class handle : 
// updating coin collected by player
// updating coin collected UI, text, sound and animation

// future
// fetching server coin amount
// sending to server coin amount

public class CoinManager : MonoBehaviour
{  
    [SerializeField]
    GameObject UserCoinsUI;
    [SerializeField]
    Animator canvasAnimator;
    AudioManager audioManager;

    public int coinCollected = 0;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }
    void StartingCoin() // call this on awake
    {
        // take from the server here
        // coinCollected = coinAmounFromServer
        // call my  place at locations here, Call()
    }
    public void UpdateCoin() // Called by every Coin 
    {
        // modifiy : coin value, coin UI in game, coin UI audio
        coinCollected++;
        UserCoinsUI.GetComponentInChildren<TMP_Text>().text = coinCollected.ToString();
        canvasAnimator.PlayInFixedTime("Coins_Flipping",0,1.5f);
        audioManager.UICoinTakenSound();
    }

}
