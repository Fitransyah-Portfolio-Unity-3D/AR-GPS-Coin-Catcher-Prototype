using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Web;
using UnityEngine.Networking;
using Newtonsoft.Json;

[Serializable]
public class CardFetcher : MonoBehaviour
{
    [SerializeField]
    GameObject carousel;
    [SerializeField]
    Transform carouselContentTransform;
    [SerializeField]
    GameObject cardPrefab;
    AllCardData allCardData;

    [SerializeField]
    string memberNumber;
    string serverEndpoint = "https://app.xrun.run/gateway.php?";

    public string MemberNumber { get {return memberNumber;}}

    // membernumber : PlayerPrefs.GetString("member");

    public delegate void FirstCardFetchHandler(Toggle firstCardToggle);

    public event Action OnCardsFetched;
    public event FirstCardFetchHandler OnFirstCardsFetched;

    void Awake()
    {
        StartCoroutine(RequestCardsData());
    }
    IEnumerator RequestCardsData()
    {
        // building query
        string endpoint = serverEndpoint;
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "app4000-01-rev-01";
        query["member"] = memberNumber;
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();

        // requesting cards data
        using (UnityWebRequest www = UnityWebRequest.Get(endpoint))
        {


            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // cahching request response
                var rawData = www.downloadHandler.text;
                AllCardData responseData = new AllCardData();
                responseData = JsonConvert.DeserializeObject<AllCardData>(rawData);
                allCardData = responseData;
                PopulateCard();
            }
        }
    }
    void PopulateCard()
    {
        // instantiate prefab as per number carddata list count
        // set prefab parent (carousel - content)
        // define rect transform position

        for (int i = 0; i < allCardData.Data.Count; i++)
        {
            GameObject instance = Instantiate(cardPrefab, carouselContentTransform);
            instance.name = allCardData.Data[i].displaystr + " card";
            float k = i  * 900f;
            RectTransform rect = instance.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(k, 0);

            CardGenerator instanceCardDetail = instance.GetComponent<CardGenerator>();
            instanceCardDetail.CardName.text = allCardData.Data[i].displaystr;
            instanceCardDetail.Address.text = allCardData.Data[i].address;
            decimal currency1Final = decimal.Parse(allCardData.Data[i].amount);
            instanceCardDetail.Currency1Amount.text = Math.Round(currency1Final, 2).ToString();
            decimal currency2Final = decimal.Parse(allCardData.Data[i].eamount);
            instanceCardDetail.Currency2Amount.text = Math.Round(currency2Final, 2).ToString();
            instanceCardDetail.Currency1Symbol.text = allCardData.Data[i].symbol;
            instanceCardDetail.Currency2Symbol.text = allCardData.Data[i].countrySymbol;
            instanceCardDetail.CardLogo.sprite = GetCardLogo(allCardData.Data[i].file);
            instanceCardDetail.CardBackground.color = GetCardColor(allCardData.Data[i].file);
            instanceCardDetail.thisCardData = allCardData.Data[i];

            instance.AddComponent<Toggle>();
            Toggle instanceToggle = instance.GetComponent<Toggle>();
            if (i == 0)
            {
                instanceToggle.isOn = true;
                OnFirstCardsFetched(instanceToggle);
            }
            else
            {
                instanceToggle.isOn = false;
            }
            instanceToggle.group = carouselContentTransform.GetComponentInChildren<ToggleGroup>();
        }
        //OnCardsFetched();
    }
    public Sprite GetCardLogo(string symbolName)
    {
        Sprite returnSymbol = Resources.Load<Sprite>("CardIcon/" + symbolName);
        return returnSymbol;
    }
    Color GetCardColor(string fileName)
    {
        string colorHex;
        switch (fileName)
        {
            case "979":
                colorHex = "22BBE3";
                break;
            case "978":
             colorHex =   "E73C47";
                break;
            case "1757":
                colorHex = "FFCD00";
                break;
            case "2030":
                colorHex = "FFFFF";
                break;
            case "2465":
              colorHex  = "394882";
                break;
                default: 
                colorHex = "394882";
                break;
        }
        Color theColor;
        ColorUtility.TryParseHtmlString("#" + colorHex, out theColor);
        return theColor;  
    }
    public class AllCardData
    {
       public List<CardData> Data;
    }
    
}
