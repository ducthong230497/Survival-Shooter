using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    private static GameUI instance;
    public static GameUI Instance { get=>instance; }

    [Header("Score")]
    [SerializeField] private Text scoreText;
    private int currentScore;
    private int scorePerKill;

    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    private int playerStartHealth;

    // Use this for initialization
    private void Start()
    {
        if(instance != this)
        {
            instance = this;
        }

        scorePerKill = SurvivalShooterGame.survivalShooterSettings.scoreKillEnemy;
        playerStartHealth = SurvivalShooterGame.survivalShooterSettings.startingHealth;
    }

    public void OnKillEnemy()
    {
        currentScore += scorePerKill;
        scoreText.text = $"SCORE: {currentScore}";
    }

    public void OnPlayerHitEnemy(int newHealth)
    {
        StartCoroutine(UpdatePlayerHealth(newHealth));
    }

    private IEnumerator UpdatePlayerHealth(int newHealth)
    {
        float healthPercent = (float)newHealth / (float)playerStartHealth;
        float timer = 0;
        while(timer < 0.5f)
        {
            timer += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(healthPercent, 1, timer / 0.5f);
            yield return null;
        }

        healthSlider.value = healthPercent;
    }
}
