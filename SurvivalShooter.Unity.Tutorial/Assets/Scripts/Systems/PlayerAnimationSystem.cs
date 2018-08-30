using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(PlayerHealthSystem))]
public class PlayerAnimationSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public EntityArray entities;
        public ComponentArray<Animator> animators;
        public readonly ComponentDataArray<PlayerInput> playerInputs;
        public readonly ComponentDataArray<Health> healths;
    }

    [Inject] private Data data;
    public static bool isDead;

    protected override void OnUpdate()
    { 
        EntityCommandBuffer entityCommandBuffer = PostUpdateCommands;
        for (int i = 0; i < data.Length; ++i)
        {
            if (!SurvivalShooterGame.entityManager.HasComponent<Dead>(data.entities[i]))
            {
                Vector3 move = data.playerInputs[i].Move;
                data.animators[i].SetBool(GameString.isWalking, move.x != 0 || move.z != 0);
            }
            else if(isDead == false && SurvivalShooterGame.entityManager.HasComponent<Dead>(data.entities[i]))
            {
                data.animators[i].SetTrigger("Die");
                isDead = true;
            }
        }
    }
}
