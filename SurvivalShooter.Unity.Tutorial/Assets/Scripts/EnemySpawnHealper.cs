using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemySpawnHealper : MonoBehaviour {
    public GameObject enemy;
    public int count;
    public float spawnTime;
    public float timer;
    public Stack<GameObject> enemyStack;

    private void Start()
    {
        enemyStack = new Stack<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject enemyObject = Instantiate(enemy);
            Entity enemyEntity = enemyObject.GetComponent<GameObjectEntity>().Entity;
            SurvivalShooterGame.entityManager.AddComponentData(enemyEntity, new Enemy());
            SurvivalShooterGame.entityManager.AddComponentData(enemyEntity, new Health() { value = SurvivalShooterGame.survivalShooterSettings.enemyStartHealth });

            enemyObject.GetComponent<EnemySinker>().enemySpawnHealper = this;
            enemyObject.SetActive(false);

            enemyStack.Push(enemyObject);
        }
    }
}