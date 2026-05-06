using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinToMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}