using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cheats : MonoBehaviour
{
    [SerializeField] RectTransform arrowTransform;
    [SerializeField] RectTransform hammerTransform;

    public void TogglePositioning()
    {
        //arrowTransform.anchoredPosition.x == 200 && arrowTransform.anchoredPosition.y == 65
        // 250 -100
        if (arrowTransform.anchoredPosition.x < 250f && arrowTransform.anchoredPosition.y < 65f)
        {
            arrowTransform.DOAnchorPos(new Vector2(250f,-100f), 0.5f, false);
        }
        else
        {
            arrowTransform.DOAnchorPos(new Vector2(200f, 65f), 0.5f, false);
        }
        if (hammerTransform.anchoredPosition.x > 250f && hammerTransform.anchoredPosition.y > -100f)
        {
            hammerTransform.DOAnchorPos(new Vector2(250f, -100f), 0.5f, false);
        }
        else
        {
            hammerTransform.DOAnchorPos(new Vector2(315f, 130f), 0.5f, false);
        }
    }
}
