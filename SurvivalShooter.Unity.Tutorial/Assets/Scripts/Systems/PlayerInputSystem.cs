using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInputSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<PlayerInput> playerInputs;
        public SubtractiveComponent<Dead> deads;
    }

    [Inject] private Data data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; ++i)
        {
            PlayerInput newInput = new PlayerInput
            {
                Move = new float3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))
            };
            data.playerInputs[i] = newInput;
        }
    }
}
