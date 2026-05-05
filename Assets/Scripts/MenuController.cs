using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}