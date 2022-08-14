using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TransactionFetcher : MonoBehaviour
{
    [SerializeField] CardManager cardManager;
    [SerializeField] ScrollRect transactionScrollRect;
    [SerializeField] RectTransform transactionParentTransform;
    [SerializeField] GameObject transactionPrefab;
    [SerializeField] GameObject nullTransactionPrefab;
    [SerializeField] AllTransactionData allTransactionData;

    string memberNumber;
    string activeCardCurrency;
    string serverEndpoint = "https://app.xrun.run/gateway.php?";
    float originalTransactionParentSizeDeltaY;
    private void Awake()
    {
        cardManager.OnActiveCardSet += GenerateTransactionHistory;
        originalTransactionParentSizeDeltaY = transactionParentTransform.sizeDelta.y;
    }
    private void OnDestroy()
    {
        cardManager.OnActiveCardSet -= GenerateTransactionHistory;
    }
    void GenerateTransactionHistory()
    {
        memberNumber = PlayerDataStatic.Member;
        activeCardCurrency = cardManager.activeCardData.currency;
        StartCoroutine(RequestTransactionData());
    }
    IEnumerator RequestTransactionData()
    {
        yield return null;
        // building query
        string endpoint = serverEndpoint;
        var uriBuilder = new UriBuilder(endpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["act"] = "app4100-02";
        query["member"] = memberNumber;
        query["startwith"] = "0";
        query["currency"] = activeCardCurrency;
        query["code"] = "3114";
        uriBuilder.Query = query.ToString();
        endpoint = uriBuilder.ToString();

        // requesting transaction data
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
                AllTransactionData responseData = new AllTransactionData();
                responseData = JsonConvert.DeserializeObject<AllTransactionData>(rawData);
                foreach(RectTransform transaction in transactionParentTransform)
                {
                    Destroy(transaction.gameObject);
                }
                allTransactionData.Data.Clear();
                allTransactionData = responseData;
                Debug.Log(allTransactionData.Data.Count);
                PopulateTransaction();

            }   
        }
    }
    void PopulateTransaction()
    {
        transactionParentTransform.sizeDelta = new Vector2(transactionParentTransform.sizeDelta.x, originalTransactionParentSizeDeltaY);
        if (allTransactionData.Data.Count == 0)
        {
            Debug.Log("No transaction!");
            Instantiate(nullTransactionPrefab, transactionParentTransform);
        }
        else
        {
            for (int i = 0; i < allTransactionData.Data.Count; i++)
            {
                if (i >= 4)
                {
                    transactionParentTransform.sizeDelta = new Vector2(transactionParentTransform.sizeDelta.x, transactionParentTransform.sizeDelta.y + 200f);
                }

                GameObject transaction = Instantiate(transactionPrefab, transactionParentTransform);
                transaction.name = $"Transaction number : {allTransactionData.Data[i].transaction}";
                TransactionGenerator instanceTransactionGenerator = transaction.GetComponent<TransactionGenerator>();
                instanceTransactionGenerator.thisItemTransaction = allTransactionData.Data[i];

                instanceTransactionGenerator.DateAndTimeText.text = allTransactionData.Data[i].datetimefull;
                instanceTransactionGenerator.AmountText.text = allTransactionData.Data[i].amount;
                instanceTransactionGenerator.SymbolText.text = allTransactionData.Data[i].symbol;
            }
        }

    }
}
[Serializable]
public class AllTransactionData
{
    public List<TransactionData> Data;
}
