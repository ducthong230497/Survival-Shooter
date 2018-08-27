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
        playerStartHealth = SurvivalShooterGame.survivalShooterSettings.playerStartHealth;
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
        Debug.Log($"new health: {newHealth}");
        float newHealthPercent = (float)newHealth / (float)playerStartHealth;
        float currentHealthPercent = healthSlider.value;
        float timer = 0;
        while(timer < 0.5f)
        {
            timer += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(currentHealthPercent, newHealthPercent, timer / 0.5f);
            yield return null;
        }

        healthSlider.value = newHealthPercent;
    }
}
