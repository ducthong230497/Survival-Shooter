using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnerSystem : ComponentSystem {

    public struct Data
    {
        public readonly int Length;
        public ComponentArray<EnemySpawnHealper> enemySpawners;
    }

    public struct PlayerData
    {
        public readonly int Length;
        public ComponentDataArray<PlayerInput> playerInputs;
        public ComponentDataArray<Health> healths;
        
    }

    [Inject] Data data;
    [Inject] PlayerData playerData;
    EntityCommandBuffer entityCommandBuffer;
    protected override void OnUpdate()
    {
        
        float dt = Time.deltaTime;
        for (int i = 0; i < data.Length; ++i)
        {
            //if (playerData.healths[0].value <= 0)
            //    return;

            data.enemySpawners[i].timer += dt;

            EnemySpawnHealper spawnHealper = data.enemySpawners[i];

            if (data.enemySpawners[i].timer > data.enemySpawners[i].spawnTime)
            {
                data.enemySpawners[i].timer = 0;

                if (data.enemySpawners[i].enemyStack.Count == 0) continue;

                GameObject gameObject = data.enemySpawners[i].enemyStack.Pop();
                gameObject.SetActive(true);

                //Reset Properties
                gameObject.GetComponent<CapsuleCollider>().isTrigger = false;

                //Set Position
                //gameObject.GetComponent<NavMeshAgent>().Warp(data.enemySpawners[i].transform.position);
                gameObject.transform.position = data.enemySpawners[i].transform.position;

                Entity entity = gameObject.GetComponent<GameObjectEntity>().Entity;
                
                entityCommandBuffer = PostUpdateCommands;
                
                //Entity will automatically remove ComponentData, so i have to check and readd it, dunno why
                if (!SurvivalShooterGame.entityManager.HasComponent<Enemy>(entity)) 
                {
                    entityCommandBuffer.AddComponent(entity, new Enemy());
                }
                if (!SurvivalShooterGame.entityManager.HasComponent<Health>(entity))
                {
                    entityCommandBuffer.AddComponent(entity, new Health() { value = SurvivalShooterGame.survivalShooterSettings.enemyStartHealth });
                }
            }
        }
    }
}
