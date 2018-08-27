using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalShooterSettings : MonoBehaviour {
    [Header("Player Settings")]
    public int playerStartHealth;
    public int playerMovementSpeed;
    public int raycastLength;
    [Header("Shoot Settings")]
    public int shootRange;
    public float shootCoolDown;
    public float shootEffectDisplayTime;
    [Header("Camera Settings")]
    public int cameraSpeed;
    [Header("Enemy Settings")]
    public int enemyStartHealth;
    public int enemyDamage;
    public int scoreKillEnemy;
    public float enemyAttackCoolDown;
}
