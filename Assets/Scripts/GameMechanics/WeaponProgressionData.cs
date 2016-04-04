﻿using UnityEngine;

/// <summary>
/// Represents the data that is specified by game design for specific weapon. 
/// </summary>
public class WeaponProgressionData : ScriptableObject {

    [SerializeField]
    private bool isMeleeWeapon;
    [SerializeField]
    private float baseDamagePerHit;
    [SerializeField]
    private float damageIncreasePerLevel;
    [SerializeField]
    [Range(0f, 10f)]
    private float timeBetweenShots;
    [SerializeField]
    [Range(0f, 100f)]
    private float bulletSpeed;
    [SerializeField]
    [Range(0f, 100f)]
    private float rangeInMeters;

    public bool IsMeleeWeapon { get { return this.isMeleeWeapon; } }
    public float BaseDamagePerHit { get { return this.baseDamagePerHit; } }
    public float DamageIncreasePerLevel { get { return this.damageIncreasePerLevel; } }
    public float TimeBetweenShots { get { return this.timeBetweenShots; } }
    public float BulletSpeed { get { return this.bulletSpeed; } }
    public float RangeInMeters { get { return this.rangeInMeters; } }
}