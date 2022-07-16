using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Panel : MonoBehaviour
{
    public float yStartPosition;
    public float yEndPosition;
    public float animationSpeed;

    RectTransform panelTransform;

    private void Awake()
    {
        panelTransform = GetComponent<RectTransform>();
        yStartPosition = panelTransform.anchoredPosition.y;
    }
    public void MoveUp()
    {
        if (panelTransform.anchoredPosition.y == yStartPosition)
        {
            panelTransform.DOAnchorPos(new Vector2(0, yEndPosition), animationSpeed, true);
        }
        else if (panelTransform.anchoredPosition.y == yEndPosition)
        {
            panelTransform.DOAnchorPos(new Vector2(0, yStartPosition), animationSpeed, true);
        }
        
    }


}
