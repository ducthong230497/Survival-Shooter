using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentArray<Rigidbody> rigidbodys;
        public ComponentDataArray<PlayerInput> playerInputs;
    }

    [Inject] private Data data;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;
        int speed = SurvivalShooterGame.survivalShooterSettings.playerMovementSpeed;
        for (int i = 0; i < data.Length; ++i)
        {
            Vector3 position = data.rigidbodys[i].transform.localPosition;
            
            Vector3 move = data.playerInputs[i].Move;

            Vector3 newPosition = new Vector3(position.x + (move.x * speed * dt), position.y, position.z + (move.z * speed * dt));

            data.rigidbodys[i].MovePosition(newPosition);
        }
    }
}
