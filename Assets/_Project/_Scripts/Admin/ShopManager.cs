using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public void BackToHomeButtonClicked()
    {
        SceneManager.LoadScene(3);
    }
    public void PayArrowButtonClicked()
    {
        Debug.Log("Player got 5X Bow PowerUps");
    }
    public void PayHammerButtonClicked()
    {
        Debug.Log("Player got 5X Hammer PowerUps");
    }
}
