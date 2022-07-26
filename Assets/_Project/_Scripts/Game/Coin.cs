using System;
using UnityEngine;

// this Class placed in every coin instance
// Instantiate runtime by CoinManager

// this class handle : 
// coin spin
// coin blinking called by Player/Raycast
// coin taken called by Player/Raycast
// Destroying self
// update server if taken
public class Coin : MonoBehaviour
{

    GameObject coin;
    Animator thisCoinAnimator;
    CoinData thisCoinData;

    [SerializeField]
    float yRotationSpeed;
    [SerializeField]
    float delayTime;
    public bool blinking;
    public bool coinTaken = false;

    void OnEnable()
    {
        // caching
        coin = this.gameObject;
        thisCoinAnimator = coin.GetComponent<Animator>();
        thisCoinData = GetComponent<CoinData>();

        // define y axis rotation
        int randomSwitch = UnityEngine.Random.Range(1, 3);
        if (randomSwitch == 1)
        {
            yRotationSpeed = 1f;
        }
        else
        {
            yRotationSpeed = -1f;
        }  
    }

    void Update()
    {
        // rotating
        coin.transform.Rotate(0, yRotationSpeed, 0, Space.World);

        // blinking animation or other effect here
        if (!blinking) return;
        thisCoinAnimator.SetBool("isBlinking", true);

        // self destroying sequence here
        if (!coinTaken) return;
        OnCoinTaken();
    }
    void OnCoinTaken()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, delayTime);
    }
}
