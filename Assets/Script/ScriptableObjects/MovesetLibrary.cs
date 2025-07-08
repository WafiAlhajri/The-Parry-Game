using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Moves", menuName = "new moves Library")]
public class MovesetLibrary : ScriptableObject
{

    public List<Idle> IdleFrames = new List<Idle>();
    public List<Attacks> NormalAttacks = new List<Attacks>();
    public List<Attacks> SpecialAttacks = new List<Attacks>();
    public List<Attacks> UltimateAttacks = new List<Attacks>();

    [Serializable]
    public class Attacks
    {
        public string AttackName;
        public List<Attack> AttackFrames = new List<Attack>();
    }

    [Serializable]
    public class Attack
    {
        public string AttackFrame;
        public Sprite AttackSprite;
        public bool Parryable;
        public Vector3 AttackLocation;
    }

    [Serializable]
    public class Idle
    {
        public string FrameName;
        public Sprite FrameSprite;
    }
}
