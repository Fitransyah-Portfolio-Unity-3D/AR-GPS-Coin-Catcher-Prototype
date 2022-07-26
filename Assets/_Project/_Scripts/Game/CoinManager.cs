using System;
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
    [Header("Updating")]
    [SerializeField]
    GameObject userCoinsUI;
    [SerializeField]
    Animator canvasAnimator;
    [SerializeField]
    TMP_Text availableCoinsUI;
    AudioManager audioManager;

    [Space(5)]
    [Header("Listening")]
    [SerializeField]
    MyPlaceAtLocations myPlaceAtLocations;
    [SerializeField]
    Player player;

    int coinCollected = 0;
    int coinsAvailable = 0;

    public int CoinCollected { get { return coinCollected; } set { coinCollected = value; } }
    public int CoinsAvailable { get { return coinsAvailable; } set { coinsAvailable = value; } }

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }
    private void OnEnable()
    {
        myPlaceAtLocations.OnCoinSpawn += AvailableCoinIncrementing;
        player.OnPlayerTakeCoin += AvailableCoinDecrementing;
        player.OnPlayerTakeCoin += UpdateCoinTaken;
    }
    private void OnDisable()
    {
        myPlaceAtLocations.OnCoinSpawn -= AvailableCoinIncrementing;
        player.OnPlayerTakeCoin -= AvailableCoinDecrementing;
        player.OnPlayerTakeCoin -= UpdateCoinTaken;

    }
    void StartingCoin() // call this on awake
    {
        // take from the server here
        // coinCollected = coinAmounFromServer
        // call my  place at locations here, Call()
    }
    public void UpdateCoinTaken() // Called by every Coin 
    {
        // modifiy : coin value, coin UI in game, coin UI audio
        coinCollected++;
        userCoinsUI.GetComponentInChildren<TMP_Text>().text = coinCollected.ToString();
        canvasAnimator.PlayInFixedTime("Coins_Flipping",0,1.5f);
        audioManager.UICoinTakenSound();
    }
    void AvailableCoinIncrementing()
    {
        coinsAvailable++;
        availableCoinsUI.text = "Available : " + coinsAvailable.ToString();
    }
    public void AvailableCoinDecrementing()
    {
        coinsAvailable--;
        availableCoinsUI.text = "Available : " + coinsAvailable.ToString();
    }

}
