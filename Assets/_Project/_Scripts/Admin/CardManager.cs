using System;
using UnityEngine;
using UnityEngine.UI;
using AsPerSpec;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    GameObject cardsParent;
    [SerializeField]
    CardGenerator activeCard;
    [SerializeField]
    CarouselToggler carouselToggler;
    [SerializeField]
    CardFetcher cardFetcher;

    public CardData activeCardData;

    public event Action OnActiveCardSet;

    private void Awake()
    {
        carouselToggler.OnSnapEnded += GetActiveToggle;
        cardFetcher.OnFirstCardsFetched += GetActiveToggle;
    }
    // unsubscribe later
    void GetActiveToggle(Toggle activeToggle)
    {
        SetActiveCard(activeToggle);
    }
    void SetActiveCard(Toggle isOnToggle)
    {
        activeCard = isOnToggle.gameObject.GetComponent<CardGenerator>();
        SetActiveCardData(isOnToggle);
        
    }
    void SetActiveCardData(Toggle isOntoggle)
    {
        activeCardData = isOntoggle.gameObject.GetComponent<CardGenerator>().thisCardData;
        OnActiveCardSet();
    }

    // this can be usefull for populating data with transaction history
}
