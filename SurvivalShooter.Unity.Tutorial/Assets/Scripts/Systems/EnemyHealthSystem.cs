using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyHealthSystem : ComponentSystem {

    public struct Data
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public EntityArray entities;
        public readonly ComponentDataArray<Enemy> enemies;
        public ComponentDataArray<Health> healths;
        public readonly ComponentDataArray<Damage> damages;
        private SubtractiveComponent<Dead> deads;
    }

    [Inject] Data data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; ++i)
        {
            int currentHealth = data.healths[i].value;
            int dame = data.damages[i].value;
            int newHealth = currentHealth - dame;

            if (newHealth <= 0)
            {
                PostUpdateCommands.AddComponent(data.entities[i], new Dead() { value = 0});
                GameUI.Instance.OnKillEnemy();
                continue;
            }

            ParticleSystem hitParticle = data.gameObjects[i].GetComponentInChildren<ParticleSystem>();
            hitParticle.transform.position = data.damages[i].hitPoint;
            hitParticle.Play();

            data.healths[i] = new Health() { value = newHealth };
            PostUpdateCommands.RemoveComponent<Damage>(data.entities[i]);
        }
    }
}
