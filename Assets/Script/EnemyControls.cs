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
        Idle(2f);
        AudioManager.AM.Play("Music");
    }
    void Update()
    {
        while (isIdle)
        {

        }
    }
    void nextAttackStarter()
    {
        StopAllCoroutines();
        StartCoroutine(NextAttack());
    }
    public void Idle(float time)
    {
        StartCoroutine(IdleAnimation());
        Invoke(nameof(nextAttackStarter), time);
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
        // check each string in moveset
        foreach (var attack in moveset.Moves[counter].Moves)
        {
            // check each attack in string
            if (attack.movetype == Moveset.Type.Normal)
            {
                //for mixups
                if (attack.Mixup)
                {
                    SR.sprite = moveset.ML.NormalAttacks[attack.AttackNumber].AttackFrames[0].AttackSprite;
                    yield return new WaitForSeconds(attack.AttackTime / 2);
                    int temprange = Random.Range(0, moveset.ML.NormalAttacks.Count);
                    foreach (var frames in moveset.ML.NormalAttacks[temprange].AttackFrames)
                    {
                        SR.sprite = frames.AttackSprite;
                        if (frames.Parryable)
                        {
                            AudioManager.AM.Play("Strike");
                            Parry(frames.AttackLocation, attack.ParryWindow);
                            yield return new WaitForSeconds(attack.ParryWindow);
                        }
                        else
                        {
                            yield return new WaitForSeconds(attack.AttackTime);
                        }
                    }
                }
                //no mixups
                else
                {
                    foreach (var frames in moveset.ML.NormalAttacks[attack.AttackNumber].AttackFrames)
                    {
                        SR.sprite = frames.AttackSprite;
                        if (frames.Parryable)
                        {
                            AudioManager.AM.Play("Strike");
                            Parry(frames.AttackLocation, attack.ParryWindow);
                            yield return new WaitForSeconds(attack.ParryWindow);
                        }
                        else
                        {
                            yield return new WaitForSeconds(attack.AttackTime);
                        }
                    }
                }
            }

            else if (attack.movetype == Moveset.Type.Special)
            {
                foreach (var frames in moveset.ML.SpecialAttacks[attack.AttackNumber].AttackFrames)
                {
                    SR.sprite = frames.AttackSprite;
                    if (frames.Parryable)
                    {
                        AudioManager.AM.Play("Strike");
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
                        AudioManager.AM.Play("Strike");
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
            Idle(moveset.Moves[counter].TimeTillNextAttack);
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
        AudioManager.AM.Stop("Music");
        StopAllCoroutines();
        GameManager.GM.EndScreen(true);
        Debug.Log("done");
    }
}
