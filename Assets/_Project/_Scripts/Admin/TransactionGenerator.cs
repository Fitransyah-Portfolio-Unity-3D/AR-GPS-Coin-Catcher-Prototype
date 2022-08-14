using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransactionGenerator : MonoBehaviour
{
    [SerializeField] TMP_Text dateAndTimetext;
    [SerializeField] TMP_Text amountText;
    [SerializeField] TMP_Text symbolText;
    public TMP_Text DateAndTimeText { get { return dateAndTimetext; } set { value = dateAndTimetext; } }
    public TMP_Text AmountText { get { return amountText; } set { amountText = value; } }
    public TMP_Text SymbolText { get { return symbolText; } set { symbolText = value; } }

    public TransactionData thisItemTransaction;
}
