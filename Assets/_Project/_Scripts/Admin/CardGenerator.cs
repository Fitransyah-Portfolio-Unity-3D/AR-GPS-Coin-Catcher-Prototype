using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


[System.Serializable]
public class CardGenerator : MonoBehaviour
{
    [SerializeField]
    Image cardBackground;
    [SerializeField]
    TMP_Text cardName;
    [SerializeField]
    Button hamburgerButton;
    [SerializeField]
    Image cardLogo;
    [SerializeField]
    TMP_Text currency1Amount;
    [SerializeField]
    TMP_Text currency1Symbol;
    [SerializeField]
    TMP_Text currency2Amount;
    [SerializeField]
    TMP_Text currency2Symbol;
    [SerializeField]
    TMP_Text address;
    [SerializeField]
    CanvasGroup promptPanel;
    [SerializeField]
    GameObject promptPanelGO;

    float currentAlphaPromptPanel;
    float desiredAlphaPromptPanel;
    float fadeTime = 3f;

    public Image CardBackground { get { return cardBackground; } set { cardBackground = value; } }
    public TMP_Text CardName { get { return cardName; } set { cardName = value; } }
    public TMP_Text Currency1Amount { get { return currency1Amount; } set { currency1Amount = value; } }
    public TMP_Text Currency1Symbol { get { return currency1Symbol; } set { currency1Symbol = value; } }
    public TMP_Text Currency2Amount { get { return currency2Amount; } set { currency2Amount = value; } }
    public TMP_Text Currency2Symbol { get { return currency2Symbol; } set { currency2Symbol = value; } }
    public TMP_Text Address { get { return address; } set { address = value; } }
    public Button HamburgerButton { get { return hamburgerButton; } set { hamburgerButton = value; } }
    public Image CardLogo { get { return cardLogo; } set { cardLogo = value; } }
    private void Awake()
    {
        currentAlphaPromptPanel = 0f;
        desiredAlphaPromptPanel = 0f;
        promptPanelGO.SetActive(false);
    }
    private void Update()
    {
        promptPanel.alpha = currentAlphaPromptPanel;
        currentAlphaPromptPanel = Mathf.MoveTowards(currentAlphaPromptPanel, desiredAlphaPromptPanel, fadeTime * Time.deltaTime);
    }
    public void CopyAddressToClipboard()
    {
        if (!string.IsNullOrEmpty(address.text))
        {
            UniClipboard.SetText(address.text);
            StartCoroutine(ShowCopyAdressPrompt());
        } 
    }
    public void ShowQRCodeButtonClicked()
    {
        MyQRCreator qrCreator = GameObject.FindGameObjectWithTag("QRCreator").GetComponent<MyQRCreator>();
        qrCreator.ShowQRCanvas();
    }
   IEnumerator ShowCopyAdressPrompt()
    {
        promptPanelGO.SetActive(true);
        desiredAlphaPromptPanel = 1f;
        yield return new WaitForSeconds(1f);
        desiredAlphaPromptPanel = 0f;
        yield return new WaitForSeconds(1f);
        promptPanelGO.SetActive(false);
    }

    public CardData thisCardData;
}
