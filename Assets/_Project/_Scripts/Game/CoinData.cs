using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinData : MonoBehaviour 
{
    [SerializeField]
    string coin, cointype, amount, countlimit, lng, lat, distance, advertisement, brand, title, contents, currency, adColor1, adColor2, coins, adThumbnail, adThumbnail2, tracking, isBigcoin, symbol, brandLogo, symbolimg, exad, exco;

    public string Coin { get { return coin; } set { coin = value; } }
    public string Cointype { get { return cointype; } set { cointype = value; } }
    public string Amount { get { return amount; } set { amount = value; } }
    public string Countlimit { get { return countlimit; } set { countlimit = value; } }
    public string Lng { get { return lng; } set { lng = value; } }
    public string Lat { get { return lat; } set { lat = value; } }
    public string Distance { get { return distance; } set { distance = value; } }
    public string Advertisement { get { return advertisement; } set { advertisement = value; } }
    public string Brand { get { return brand; } set { brand = value; } }
    public string Title { get { return title; } set { title = value; } }
    public string Contents { get { return contents; } set { contents = value; } }
    public string Currency { get { return currency; } set { currency = value; } }
    public string AdColor1 { get { return adColor1; } set { adColor1 = value; } }
    public string AdColor2 { get { return adColor2; } set { adColor2 = value; } }
    public string Coins { get { return coins; } set { coins = value; } }
    public string AdThumbnail { get { return adThumbnail; } set { adThumbnail = value; } }
    public string AdThumbnail2 { get { return adThumbnail2; } set { adThumbnail2 = value; } }
    public string Tracking { get { return tracking; } set { tracking = value; } }
    public string Isbigcoin { get { return isBigcoin; } set { isBigcoin = value; } }
    public string Symbol { get { return symbol; } set { symbol = value; } }
    public string BrandLogo { get { return brandLogo; } set { brandLogo = value; } }
    public string Symbolimg { get { return symbolimg; } set { symbolimg = value; } }
    public string Exad { get { return exad; } set { exad = value; } }
    public string Exco { get { return exco; } set { exco = value; } }
}
