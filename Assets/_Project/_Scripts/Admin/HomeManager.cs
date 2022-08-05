using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    
    public void PlayButtonClicked()
    {
        SceneManager.LoadScene(4);
    }
    public void MyXrunButtonClicked()
    {
        // go to wallet page
    }
    public void MyAdsButtonClicked()
    {
        // go to my ads page
    }
    public void ShopButtonClicked()
    {
        // go to shop page
    }
    public void SettingButtonClicked()
    {
        // go to stting page
    }
}
