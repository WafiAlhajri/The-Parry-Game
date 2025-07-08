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
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!parried)
        {
            GameManager.GM.setPoints(points);
            parried = true;
        }
    }
}
