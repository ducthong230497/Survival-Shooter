using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySinker : MonoBehaviour
{
    private bool isSinking;
    [Range(0, 1)]
    public float timeToDisable;
    public EnemySpawnHealper enemySpawnHealper;

    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * SurvivalShooterGame.survivalShooterSettings.enemySinkSpeed * Time.deltaTime);
        }
    }

    public void StartSinking()
    {
        StartCoroutine(WaitToDisable());
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(timeToDisable);

        animator.ResetTrigger("Dead");
        audioSource.clip = SurvivalShooterGame.survivalShooterSettings.enemyHurtClip;
        gameObject.SetActive(false);
        enemySpawnHealper.enemyStack.Push(gameObject);
    }
}
