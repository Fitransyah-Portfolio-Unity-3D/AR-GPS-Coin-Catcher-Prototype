using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MyCountryCodeManager : MonoBehaviour
{
    public Sprite[] flags;
    public TextAsset jsonFile;
    List<string> countryCodesList;
    public TMP_Dropdown MyCountryCode;
    MyData code;
    MyCountryName name;
    string countryCode;
    void Start()
    {
        //jsonFile = Resources.Load("Codes/CountryCode") as TextAsset;
        if (jsonFile != null)
            FileReader();
        countryCodesList = new List<string>();
        countryCodesList.Clear();
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(MyCountryCode.captionText.text))
        {
            string[] country_code = MyCountryCode.options[MyCountryCode.value].text.Split(new[] { "   (+" }, StringSplitOptions.None);
            string code = country_code[1].Remove(country_code[1].Length - 1);
            countryCode = "" + code;
            MyCountryCode.captionText.text = "+" + countryCode;
        }
    }

    void FileReader()
    {
        code = JsonUtility.FromJson<MyData>(jsonFile.text);
        name = JsonUtility.FromJson<MyCountryName>(jsonFile.text);
        flags = new Sprite[code.data.Length];
        populateCode();
    }

    void populateCode()
    {
        MyCountryCode.ClearOptions();
        // Create the option list
        List<TMP_Dropdown.OptionData> flagItems = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < code.data.Length; i++)
        {
            Resources.Load<Sprite>("Sprites/my_sprite");
            flags[i] = Resources.Load<Sprite>("Flags/" + code.data[i].country_code.ToLower().ToString());
            string flagName = code.data[i].country_en + "   (+" + code.data[i].phone_code + ")";
            var flagOption = new TMP_Dropdown.OptionData(flagName, flags[i]);
            flagItems.Add(flagOption);
        }
        MyCountryCode.AddOptions(flagItems);
    }
}
[System.Serializable]
public class MyData
{
    public CodeData[] data;
}

[System.Serializable]
public class MyCodeData
{
    public string country_code;
    public string country_en;
    public string phone_code;
    public string country_cn;
}
[System.Serializable]
public class MyCountryName
{
    public List<string> Name;
}
