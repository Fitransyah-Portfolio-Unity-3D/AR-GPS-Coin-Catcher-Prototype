using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    //[SerializeField] Image timerFill;
    [SerializeField] TMP_Text arrowTimerText;
    [SerializeField] Button arrowButton;
    [SerializeField] TMP_Text hammerTimerText;
    [SerializeField] Button hammerButton;

    int remainingDuration;

    public event Action OnArrowActive;
    public event Action OnArrowInactive;
    public event Action OnHammerActive;
    public event Action OnHammerInActive;

    public void StartArrowTimer(int second)
    {
        remainingDuration = second;
        arrowTimerText.gameObject.SetActive(true);
        StartCoroutine(UpdateArrowTimer());
    }
    public void StartHammerTimer(int second)
    {
        remainingDuration = second;
        hammerTimerText.gameObject.SetActive(true);
        StartCoroutine(UpdateHammerTimer());
    }
    IEnumerator UpdateArrowTimer()
    {
        arrowButton.interactable = false;
        while (remainingDuration > 0)
        {
            arrowTimerText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60 : 00}";
            //timerFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnArrowTimerEnd();
    }
    IEnumerator UpdateHammerTimer()
    {
        hammerButton.interactable = false;
        while (remainingDuration > 0)
        {
            hammerTimerText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60: 00}";
            //timerFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnHammerTimerEnd();
    }
    void OnArrowTimerEnd()
    {
        arrowButton.interactable = true;
        arrowTimerText.gameObject.SetActive(false);
    }
    void OnHammerTimerEnd()
    {
        hammerButton.interactable = true;
        hammerTimerText.gameObject.SetActive(false);
    }

}
