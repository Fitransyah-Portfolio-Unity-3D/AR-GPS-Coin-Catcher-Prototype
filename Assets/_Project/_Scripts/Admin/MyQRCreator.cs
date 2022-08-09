using System.Collections; 
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MyQRCreator : MonoBehaviour
{
    [Header("QR Creator")]
    [SerializeField]
    CodeWriter codeWtr;
    [SerializeField]
    RawImage previewImg;
    [SerializeField]
    CodeWriter.CodeType codeType;
    [SerializeField]
    Texture2D targetTex;
    [SerializeField]
    Text errorText;
    [SerializeField]
    CardManager cardManager;

    [Header("Card Info")]
    [SerializeField]
    Image CurrencyLogo;
    [SerializeField]
    TMP_Text currencyName;
    [SerializeField]
    TMP_Text address;
    [SerializeField]
    Button copyToClipboardButton;
    [SerializeField]
    Button shareQRButton;
    [SerializeField]
    Button downloadQRButton;
    [SerializeField]
    CardFetcher cardFetcher;

    [Header("Controlling")]
    [SerializeField]
    Canvas QRCanvas;

    public string testingQR;
    void Start()
    {
        CodeWriter.onCodeEncodeFinished += GetCodeImage;
        CodeWriter.onCodeEncodeError += errorInfo;
        cardManager.OnActiveCardSet += Create_Code;
        QRCanvas.gameObject.SetActive(false);
    }
    public void Create_Code()
    {
        if (codeWtr != null)
        {
            codeWtr.CreateCode(codeType, cardManager.activeCardData.address);
            CreateCardInfo();
        }
    }
    public void SaveIamgeToGallery()
    {
        if (targetTex != null)
        {
            MediaController.SaveImageToGallery(targetTex);
            promptPanelText.text = "QR Code telah tersimpan ke Gallery Anda";
            StartCoroutine(ShowCopyAdressPrompt());
        }
    }
    public void GetCodeImage(Texture2D tex)
    {
        if (targetTex != null)
        {
            DestroyImmediate(targetTex, true);
        }
        targetTex = tex;
        RectTransform component = this.previewImg.GetComponent<RectTransform>();
        float y = component.sizeDelta.x * (float)tex.height / (float)tex.width;
        component.sizeDelta = new Vector2(component.sizeDelta.x, y);
        previewImg.texture = targetTex;
    }
    public void errorInfo(string str)
    {
        errorText.text = str;
    }
   
    void CreateCardInfo()
    {
        CurrencyLogo.sprite = cardFetcher.GetCardLogo(cardManager.activeCardData.file);
        currencyName.text = cardManager.activeCardData.displaystr;
        address.text = cardManager.activeCardData.address;
    }
    public void ShowQRCanvas()
    {
        QRCanvas.gameObject.SetActive(true);
    }
    public void HideQRCanvas()
    {
        QRCanvas.gameObject.SetActive(false);
    }

    #region CopyClipboardNotif
    [SerializeField]
    CanvasGroup promptPanel;
    [SerializeField]
    GameObject promptPanelGO;
    [SerializeField]
    TMP_Text promptPanelText;

    float currentAlphaPromptPanel;
    float desiredAlphaPromptPanel;
    float fadeTime = 3f;

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
            promptPanelText.text = "Alamat transaksi dompet Anda telah tersalin ke clipboard!";
            StartCoroutine(ShowCopyAdressPrompt());
        }
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
    #endregion
}
