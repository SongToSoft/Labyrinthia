using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonControl : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
