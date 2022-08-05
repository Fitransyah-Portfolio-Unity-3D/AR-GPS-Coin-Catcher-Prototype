using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField]
    GameObject thisGameobjet;
    void Start()
    {
        thisGameobjet = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        
            thisGameobjet.transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime);
        
    }
}
