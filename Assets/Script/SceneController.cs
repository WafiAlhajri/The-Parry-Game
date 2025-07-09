using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void normalfight()
    {
        SceneManager.LoadScene(1);
    }
    public void hardfight()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
