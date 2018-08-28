using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemySpawnerSystem : ComponentSystem {

    public struct Data
    {
        public readonly int Length;
        public ComponentArray<EnemySpawnHealper> enemySpawners;
    }

    public struct PlayerData
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public ComponentDataArray<PlayerInput> playerInputs;
        public ComponentDataArray<Health> healths;
    }

    [Inject] Data data;
    [Inject] PlayerData playerData;
    List<float> timer = new List<float>();
    protected override void OnUpdate()
    {
        
        float dt = Time.deltaTime;
        for (int i = 0; i < data.Length; ++i)
        {
            if (playerData.healths[i].value <= 0)
                return;
            if (timer.Count < i + 1)
                timer.Add(0f);

            timer[i] += dt;

            EnemySpawnHealper spawnHealper = data.enemySpawners[i];

            if (timer[i] > data.enemySpawners[i].spawnTime)
            {
                //timer[i] = 0;
                //GameObject enemy = Object.Instantiate(spawnHealper.enemy, spawnHealper.transform.position, Quaternion.identity);
                //Entity enemyEntity = enemy.GetComponent<GameObjectEntity>().Entity;
                //SurvivalShooterGame.entityManager.AddComponentData(enemyEntity, new Enemy());
                //SurvivalShooterGame.entityManager.AddComponentData(enemyEntity, new Health() { value = SurvivalShooterGame.survivalShooterSettings.enemyStartHealth});


            }

            

        }
    }
}
