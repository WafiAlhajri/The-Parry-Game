using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "moveset", menuName = "new Moveset")]
public class Moveset : ScriptableObject
{
    public enum Type { Normal, Special, Ultimate, }
    public MovesetLibrary ML;
    public List<Attacks> Moves = new List<Attacks>();

    [Serializable]
    public class Attacks
    {
        public string movename;
        public List<Attack> Moves = new List<Attack>();
        public float TimeTillNextAttack = 1f;

    }

    [Serializable]
    public class Attack
    {
        public string MoveName;

        [Header("Time Lengeth")]
        public float AttackTime;
        public float ParryWindow;

        [Header("Library reference")]
        public Type movetype;
        public int AttackNumber;
        [Header("Normal Attack Options")]
        public bool Mixup;
    }
}