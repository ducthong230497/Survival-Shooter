using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementSystem : ComponentSystem {

    public struct Data
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public ComponentArray<NavMeshAgent> navMeshAgents;
        public readonly ComponentDataArray<Enemy> enemies;
        public readonly ComponentDataArray<Health> healths;
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
            if (data.healths[i].value > 0)
            {
                data.navMeshAgents[i].SetDestination(playerData.gameObjects[0].transform.position);
            }
            else
            {
                data.navMeshAgents[i].enabled = false;
            }
        }
    }
    
}
