using UnityEngine;
using UnityEngine.SceneManagement;

public class MyXrunManager : MonoBehaviour
{
    public void GoBackToHomeButtonClicked()
    {
        SceneManager.LoadScene(3);
    }
}
