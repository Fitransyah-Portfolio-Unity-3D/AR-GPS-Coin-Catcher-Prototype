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
        SceneManager.LoadScene(8);
    }
    public void MyAdsButtonClicked()
    {
        SceneManager.LoadScene(5);
    }
    public void ShopButtonClicked()
    {
        SceneManager.LoadScene(6);
    }
    public void SettingButtonClicked()
    {
        SceneManager.LoadScene(7);
    }
}
