using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnderDevelopment : MonoBehaviour
{
    [SerializeField]
    CanvasGroup promptPanel;
    [SerializeField]
    TMP_Text promptText;
    float fadeTime = 1;
    float currentAlphaPromptPanel;
    float desiredAlphaPromptPanel;
    string invokeGamebjectNotActive = "PrompSetNotActive";
    private void Start()
    {
        promptPanel.gameObject.SetActive(false);
        currentAlphaPromptPanel = 0;
        desiredAlphaPromptPanel = 0;
    }
    private void Update()
    {
        promptPanel.alpha = currentAlphaPromptPanel;
        currentAlphaPromptPanel = Mathf.MoveTowards(currentAlphaPromptPanel, desiredAlphaPromptPanel, fadeTime * Time.deltaTime);
    }
    public void UnderDevelopmentTrigger()
    {
        promptText.text = "Maaf, fitur ini sedang dalam tahap pengembangan";
        desiredAlphaPromptPanel = 1f;
        promptPanel.gameObject.SetActive(true);
    }
    public void OnClickNextPromptButton()
    {
        desiredAlphaPromptPanel = 0f;
        Invoke(invokeGamebjectNotActive, fadeTime);
    }
    void PrompSetNotActive()
    {
        promptPanel.gameObject.SetActive(false);
    }
}
