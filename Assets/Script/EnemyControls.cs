using System.Collections;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    public Moveset moveset;
    SpriteRenderer SR;
    public GameObject ParryPrefab;
    public float IdleTimer = 0.2f;
    bool isIdle;
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Idle();
    }
    void Update()
    {
        while (isIdle)
        {

        }
    }
    string mode;
    void switcher()
    {
        StopAllCoroutines();
        if (mode == "attack")
        {
            StartCoroutine(NextAttack());
        }
        else if (mode == "idle")
        {
            Idle();
        }
    }
    public void Idle()
    {
        StartCoroutine(IdleAnimation());
        mode = "attack";
        Invoke(nameof(switcher), 2f);
    }
    IEnumerator IdleAnimation()
    {
        foreach (var frames in moveset.ML.IdleFrames)
        {
            SR.sprite = frames.FrameSprite;
            yield return new WaitForSeconds(IdleTimer);
        }
        yield return new WaitForSeconds(IdleTimer);
        StartCoroutine(IdleAnimation());
    }

    int counter = 0;

    public IEnumerator NextAttack()
    {
        yield return new WaitForSeconds(moveset.Moves[counter].TimeTillNextAttack);
        // check each attack in moveset
        foreach (var attack in moveset.Moves[counter].Moves)
        {
            // check each frame in attack
            if (attack.movetype == Moveset.Type.Normal)
            {
                foreach (var frames in moveset.ML.NormalAttacks[attack.AttackNumber].AttackFrames)
                {
                    SR.sprite = frames.AttackSprite;
                    if (frames.Parryable)
                    {
                        Parry(frames.AttackLocation, attack.ParryWindow);
                    }
                    yield return new WaitForSeconds(attack.AttackTime);
                }
            }

            else if (attack.movetype == Moveset.Type.Special)
            {
                foreach (var frames in moveset.ML.SpecialAttacks[attack.AttackNumber].AttackFrames)
                {
                    SR.sprite = frames.AttackSprite;
                    if (frames.Parryable)
                    {
                        Parry(frames.AttackLocation, attack.ParryWindow);
                    }
                    yield return new WaitForSeconds(attack.AttackTime);
                }
            }

            else if (attack.movetype == Moveset.Type.Ultimate)
            {
                foreach (var frames in moveset.ML.UltimateAttacks[attack.AttackNumber].AttackFrames)
                {
                    SR.sprite = frames.AttackSprite;
                    if (frames.Parryable)
                    {
                        Parry(frames.AttackLocation, attack.ParryWindow);
                    }
                    yield return new WaitForSeconds(attack.AttackTime);
                }
            }
        }
        counter++;
        if (counter >= moveset.Moves.Count) EndFight();
        else
        {
            mode = "idle";
            Invoke(nameof(switcher), 1f);
        }
    }

    public void Parry(Vector3 location, float time)
    {
        GameObject prefab = Instantiate(ParryPrefab, location, transform.rotation);
        ParryPoint PP = prefab.GetComponent<ParryPoint>();
        PP.setParry(50, time);
    }
    public void EndFight()
    {
        StopAllCoroutines();
        GameManager.GM.EndScreen(true);
        Debug.Log("done");
    }
}
