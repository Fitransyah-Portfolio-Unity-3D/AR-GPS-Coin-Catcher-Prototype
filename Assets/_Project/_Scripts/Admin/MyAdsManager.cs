using UnityEngine;
using UnityEngine.SceneManagement;

public class MyAdsManager : MonoBehaviour
{
    public void BackToHomeButtonClicked()
    {
        SceneManager.LoadScene(3);
    }
    public void WatchNowButtonClicked()
    {
        Debug.Log("Playing Ads");
    }
    public void CloseAdsButtonClicked()
    {
        Debug.Log("Whats this button for??");
    }
}
