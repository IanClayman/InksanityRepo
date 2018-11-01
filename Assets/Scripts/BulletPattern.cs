using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum PatternType { wave, ring, spiral };

[System.Serializable]
public class BulletPattern {
    [Title("Pattern", null, TitleAlignments.Centered, true, true)]
    public PatternType patternType = PatternType.wave;
    public GameObject bullet;
    public bool aimAtPlayer = false;
    [HideIf("aimAtPlayer")]
    public Vector2 direction = Vector2.down;
    public float speed = 1;
    public int numBullets = 1;

    [TitleGroup("Pattern-Specific Properties",null, TitleAlignments.Left, false, true)]
    [ShowIf("patternType", PatternType.wave)]
    [Range(0, 180f)]
    public float angle = 90f;
    [ShowIf("patternType", PatternType.ring)]
    public float spreadSpeed = 1;
    [ShowIf("patternType", PatternType.spiral)]
    public float duration = 1;
    [ShowIf("patternType", PatternType.spiral)]
    public float revolutions = 1;
    [ShowIf("patternType", PatternType.spiral)]
    public Rotation rotation = Rotation.CCW;
    [BoxGroup]
    public float delay = 1;
}
