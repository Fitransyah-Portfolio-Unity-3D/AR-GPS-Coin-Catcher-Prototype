using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

// this class placed in Panel on Canvas0
// Exist in hierarchy on GameScene

// This class handle : 
// Movement of FeaturePanel 
// Text of FeaturePanel
// Both in  function on this class, called by Canvas0/FeaturePanel/ShowOrHideButton

public class FeaturePanel : MonoBehaviour
{
    public float yStartPosition;
    public float yEndPosition;
    public float animationSpeed;

    RectTransform panelTransform;
    [SerializeField]
    TMP_Text showHideText;

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
            showHideText.text = "Sembunyikan Menu";
        }
        else if (panelTransform.anchoredPosition.y == yEndPosition)
        {
            panelTransform.DOAnchorPos(new Vector2(0, yStartPosition), animationSpeed, true);
            showHideText.text = "Tampilkan Menu";
        }
    }
}
