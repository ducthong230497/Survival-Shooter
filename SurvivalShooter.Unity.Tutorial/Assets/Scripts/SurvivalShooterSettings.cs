using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalShooterSettings : MonoBehaviour {
    [Header("Player Settings")]
    public int playerStartHealth;
    public int playerMovementSpeed;
    public int raycastLength;
    public int playerShootDamage;
    public AudioClip playerDeadClip;
    [Header("Shoot Settings")]
    public int shootRange;
    public float shootCoolDown;
    public float shootEffectDisplayTime;
    [Header("Camera Settings")]
    public int cameraSpeed;
    [Header("Enemy Settings")]
    public GameObject Zombear;
    public GameObject Zombunny;
    public GameObject Hellephant;
    public int enemyStartHealth;
    public int enemyDamage;
    public int scoreKillEnemy;
    public float enemyAttackCoolDown;
    public float enemySinkSpeed;
    public AudioClip enemyHurtClip;
    public AudioClip enemyDeadClip;
}
