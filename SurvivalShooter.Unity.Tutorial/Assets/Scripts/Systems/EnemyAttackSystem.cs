using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyAttackSystem : ComponentSystem {

    public struct Data
    {
        public readonly int Length;
        public ComponentArray<EnemyAttacker> enemyAttackers;
        public readonly ComponentDataArray<Health> healths;
    }

    public struct PlayerData
    {
        public readonly int Length;
        public EntityArray entities;
        public readonly ComponentDataArray<Health> healths;
        public readonly ComponentDataArray<PlayerInput> playerInputs;
    }

    [Inject] private Data data;
    [Inject] private PlayerData playerData;

    protected override void OnUpdate()
    {
        EntityCommandBuffer entityCommandBuffer = PostUpdateCommands;

        int enemyDamage = SurvivalShooterGame.survivalShooterSettings.enemyDamage;
        float enemyAttackCoolDown = SurvivalShooterGame.survivalShooterSettings.enemyAttackCoolDown;

        float dt = Time.deltaTime;
        for (int i = 0; i < data.Length; ++i)
        {
            data.enemyAttackers[i].timer += dt;

            if(data.enemyAttackers[i].timer >= enemyAttackCoolDown && data.enemyAttackers[i].playerInRange && data.healths[i].value > 0)
            {
                data.enemyAttackers[i].timer = 0;
                if(playerData.healths[0].value > 0 && !SurvivalShooterGame.entityManager.HasComponent<Damage>(playerData.entities[0]))
                {
                    entityCommandBuffer.AddComponent(playerData.entities[0], new Damage() { value = enemyDamage});
                }
            }
        }
    }
}
