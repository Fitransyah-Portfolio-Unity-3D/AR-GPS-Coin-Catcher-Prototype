using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this Class placed in every coin instance
// Instantiate runtime by CoinManager

// this class handle : 
// coin spin
// coin blinking called by Player
// coin taken called by Player
public class Coin : MonoBehaviour
{
    [SerializeField]
    float yRotationSpeed;
    GameObject coin;
    Animator thisCoinAnimator;
    [SerializeField]
    MeshRenderer[] thisCoinsMeshes;


    public bool blinking;
    public bool coinTaken = false;

    void Awake()
    {
        coin = this.gameObject;
        thisCoinAnimator = coin.GetComponent<Animator>();
        thisCoinsMeshes = this.gameObject.GetComponentsInChildren<MeshRenderer>();       
    }
    void Update()
    {
        coin.transform.Rotate(0, yRotationSpeed, 0, Space.World);

        if (!blinking) return;
        thisCoinAnimator.SetBool("isBlinking", true);
        // call coin controller public function update coin here
        CoinManager coinManager = GameObject.FindGameObjectWithTag("CoinManager").GetComponent<CoinManager>();
        if (coinTaken)
        {
            coinManager.UpdateCoin();
            coinTaken = false;
        }
        
    }
}
