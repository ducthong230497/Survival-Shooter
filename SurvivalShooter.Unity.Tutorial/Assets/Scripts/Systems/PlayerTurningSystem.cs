using Unity.Entities;
using UnityEngine;

public class PlayerTurningSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentArray<Rigidbody> rigidbodys;
        public ComponentDataArray<PlayerInput> players;
    }

    [Inject] private Data data;

    protected override void OnUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        LayerMask groundLayer = LayerMask.GetMask(GameString.groundLayer);
        int raycastLength = SurvivalShooterGame.survivalShooterSettings.raycastLength;
        float dt = Time.deltaTime;
        for (int i = 0; i < data.Length; ++i)
        {
            Ray camRay = SurvivalShooterGame.mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(camRay, out raycastHit, raycastLength, groundLayer))
            {
                Vector3 position = data.rigidbodys[i].transform.position;

                Vector3 playerToMouse = raycastHit.point - position;

                playerToMouse.y = 0;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                data.rigidbodys[i].MoveRotation(newRotation);
            }
        }
    }
}
