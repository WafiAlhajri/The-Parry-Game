using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("data")]
    public int hp = 3;
    int points = 0;
    [Header("References")]
    public TextMeshProUGUI Counter;
    public List<GameObject> HPReferences = new List<GameObject>();
    public GameObject Endscreen;
    public TextMeshProUGUI EndscreenText;
    public static GameManager GM;
    void Awake()
    {
        if (GM == null)
        {
            GM = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void setPoints(int getPoints)
    {
        points += getPoints;
        Counter.text = points.ToString();
    }
    public void lowerHP()
    {
        hp--;
        HPReferences[hp].SetActive(false);
        if (hp <= 0) EndScreen(false);
    }
    public void EndScreen(bool Winlose)
    {
        Endscreen.SetActive(true);
        if (Winlose)
        {
            EndscreenText.text = "YOU WON!!!";
        }
        else
        {
            EndscreenText.text = "YOU LOST";
        }
    }
}
