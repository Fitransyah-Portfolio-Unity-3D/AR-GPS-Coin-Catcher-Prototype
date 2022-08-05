using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocalData : MonoBehaviour
{
    public static class Fields
    {
        static string playerEmail;
        static string playerMemberNumber;

        public static string  PlayerEmail{ get { return playerEmail; } set { playerEmail = value; } }
        public static string  PlayerMemberNumber{ get { return playerMemberNumber; } set { playerMemberNumber = value; } }
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("email"))
        {
            // fetch data
        }
    }
    // fetch data method
    // class with data structure same with login-04-email
}
