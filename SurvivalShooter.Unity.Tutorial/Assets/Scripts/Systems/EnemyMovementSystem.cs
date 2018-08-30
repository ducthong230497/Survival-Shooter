using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementSystem : ComponentSystem {

    public struct Data
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public EntityArray entities;
        public ComponentArray<NavMeshAgent> navMeshAgents;
        public readonly ComponentDataArray<Enemy> enemies;
    }

    public struct PlayerData
    {
        public readonly GameObjectArray gameObjects;
        public readonly ComponentDataArray<PlayerInput> playerInputs;
        public readonly ComponentDataArray<Health> healths;
    }

    [Inject] Data data;
    [Inject] PlayerData playerData;

    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; ++i)
        {
            if (!SurvivalShooterGame.entityManager.HasComponent<Dead>(data.entities[i]))
            {
                data.navMeshAgents[i].SetDestination(playerData.gameObjects[0].transform.position);
            }
            else
            {
                data.navMeshAgents[i].isStopped = true;
            }
        }
    }
    
}
