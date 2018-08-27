using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerHealthSystem : ComponentSystem {

    public struct Data
    {
        public readonly int Length;
        public EntityArray entities;
        public readonly ComponentDataArray<PlayerInput> playerInputs;
        public ComponentDataArray<Health> healths;
        public ComponentDataArray<Damage> damages;
    }

    [Inject] Data data;

    protected override void OnUpdate()
    {
        EntityCommandBuffer entityCommandBuffer = PostUpdateCommands;

        for (int i = 0; i < data.Length; ++i)
        {
            int dame = data.damages[i].value;
            int currentHealth = data.healths[i].value;
            int newHealth = currentHealth - dame;

            data.healths[i] = new Health() { value = newHealth };

            GameUI.Instance.OnPlayerHitEnemy(newHealth);

            if(newHealth <= 0)
            {

            }

            entityCommandBuffer.RemoveComponent<Damage>(data.entities[i]);
        }
    }
}
