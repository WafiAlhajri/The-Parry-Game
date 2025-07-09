using UnityEngine;

public class ParryPoint : MonoBehaviour
{
    int points;
    float time;
    bool parried = false;
    public void setParry(int getPoints, float getTime)
    {
        points = getPoints;
        time = getTime;
        Destroy(gameObject, time);
    }
    void OnDestroy()
    {
        if (!parried) GameManager.GM.lowerHP();
        else GameManager.GM.ParryAGAIN();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!parried)
        {
            AudioManager.AM.Play("Parry");
            GameManager.GM.setPoints(points);
            parried = true;
        }
    }
}
