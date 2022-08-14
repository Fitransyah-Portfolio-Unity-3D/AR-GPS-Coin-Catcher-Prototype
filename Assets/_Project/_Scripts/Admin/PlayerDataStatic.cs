using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataStatic 
{
    static string data;
    static string email;
    static string member;
    static string firstname;
    static string lastname;
    static string gender;
    static string extrastr;
    static string mobilecode;
    static string country;
    static string countrycode;
    static string region;
    static string ages;

    public static string Data { get { return data; } }
    public static string Email { get { return email; } }
    public static string Member { get { return member; } }
    public static string Firstname { get { return firstname; } }
    public static string Lastname { get { return lastname; } }
    public static string Gender { get { return gender; } }
    public static string Extrastr { get { return extrastr; } }
    public  static string Mobilecode { get { return mobilecode; } }
    public static string Country { get { return country; } }
    public static string Countrycode { get { return countrycode; } }
    public  static string Region { get { return region; } }
    public static string Ages { get { return ages; } }

    public static void SetEmail (string playerEmail)
    {
        email = playerEmail;
    }
    public static void SetMemberNumber(string playerMemberNumber)
    {
        member = playerMemberNumber;
    }
    public static void SetFirstName(string playerFirstName)
    {
        firstname= playerFirstName;
    }
    public static void SetLastName(string playerLastName)
    {
        lastname = playerLastName;
    }
    public static void SetGender(string playerGender)
    {
        gender = playerGender;
    }
    public static void SetExtrastr(string playerExtrastr)
    {
        extrastr = playerExtrastr;
    }
    public static void SetMobileCode(string playerMobileCode)
    {
        mobilecode = playerMobileCode;
    }
    public static void SetCountry(string playerCountry)
    {
        country = playerCountry;
    }
    public static void SetCountryCode(string playerCountryCode)
    {
        countrycode = playerCountryCode;
    }
    public static void SetRegion(string playerRegion)
    {
        region = playerRegion;
    }
    public static void SetAges(string playerAges)
    {
        ages = playerAges;
    }
}
