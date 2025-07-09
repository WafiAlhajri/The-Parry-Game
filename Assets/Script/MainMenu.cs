using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject PlayMenu;
    public GameObject HowToMenu;

    public void turnOnPlayMenu()
    {
        PlayMenu.SetActive(true);
        HowToMenu.SetActive(false);
    }
    public void turnOnHowToMenu()
    {
        PlayMenu.SetActive(false);
        HowToMenu.SetActive(true);
    }
    public void turnOffBoth()
    {
        PlayMenu.SetActive(false);
        HowToMenu.SetActive(false);
    }
}
