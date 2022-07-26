using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this Class placed in every coin instance
// Instantiate runtime by CoinManager

// this class handle : 
// coin spin
// coin blinking called by Player
// coin taken called by Player
// sending data to coin manager if taken
public class Coin : MonoBehaviour
{

    GameObject coin;
    Animator thisCoinAnimator;
    MeshRenderer[] thisCoinsMeshes;
    CoinData thisCoinData;

    [SerializeField]
    float yRotationSpeed;
    public bool blinking;
    public bool coinTaken = false;

    void Awake()
    {
        // caching
        coin = this.gameObject;
        thisCoinAnimator = coin.GetComponent<Animator>();
        thisCoinsMeshes = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        thisCoinData = GetComponent<CoinData>();

        // define y axis rotation
        int randomSwitch = Random.Range(1, 3);
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

        // call CoinManager if tak
        CoinManager coinManager = GameObject.FindGameObjectWithTag("CoinManager").GetComponent<CoinManager>();
        if (coinTaken)
        {
            coinManager.UpdateCoin();
            coinTaken = false;
        }
        
    }
}
