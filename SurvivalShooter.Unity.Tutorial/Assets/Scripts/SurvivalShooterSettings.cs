using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalShooterSettings : MonoBehaviour {
    [Header("Player Settings")]
    public int startingHealth;
    public int playerMovementSpeed;
    public int raycastLength;
    [Header("Shoot Settings")]
    public int shootRange;
    public float shootCoolDown;
    public float shootEffectDisplayTime;
    [Header("Camera")]
    public int cameraSpeed;
    [Space(10)]
    public int scoreKillEnemy;
}
