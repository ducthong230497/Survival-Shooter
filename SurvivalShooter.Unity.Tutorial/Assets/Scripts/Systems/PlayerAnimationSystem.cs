using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class PlayerAnimationSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentArray<Animator> animators;
        public readonly ComponentDataArray<PlayerInput> playerInputs;
    }

    [Inject] private Data data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; ++i)
        {
            Vector3 move = data.playerInputs[i].Move;
            data.animators[i].SetBool("IsWalking", move.x != 0 || move.z != 0);
        }
    }
}
