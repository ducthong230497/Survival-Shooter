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
        public EntityArray entities;
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

                GameObject go = data.enemySpawners[i].enemyStack.Pop();
                go.SetActive(true);
                go.transform.position = data.enemySpawners[i].transform.position;
                go.GetComponent<NavMeshAgent>().Warp(go.transform.position);
            }
        }
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        entityCommandBuffer = PostUpdateCommands;
    }
}
