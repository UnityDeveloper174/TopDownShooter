using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeEnemyScriptable", menuName = "Scriptable Enemys Type/MeleeEnemyScriptable")]
public class MeleeEnemyScriptable : ScriptableObject
{
    [Header("Stats")]
    public float health;
    public float damage;
    public float speed;
    public float attackSpeed;
    public float attackRange;
    public float detectionOtherEnemysRange = 2.5f;

    [Header("Effects")]
    public GameObject deathEffect;

    

    
}
