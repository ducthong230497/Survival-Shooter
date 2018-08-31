using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInputSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<PlayerInput> playerInputs;
        public ComponentArray<JoystickController> joystickControllers;
        public SubtractiveComponent<Dead> deads;
    }

    [Inject] private Data data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; ++i)
        {
            PlayerInput newInput = new PlayerInput
            {
                Move = new float3(data.joystickControllers[i].moveJoystick.value.x, 0, data.joystickControllers[i].moveJoystick.value.z)
            };
            data.playerInputs[i] = newInput;
        }
    }
}
