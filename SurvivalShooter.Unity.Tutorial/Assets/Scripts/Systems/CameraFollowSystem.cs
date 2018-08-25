using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CameraFollowSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public readonly ComponentDataArray<PlayerInput> playerInputs;
    }

    [Inject] Data data;

    bool isFirstFrame = true;
    Vector3 offset;
    protected override void OnUpdate()
    {
        Camera cam = SurvivalShooterGame.mainCamera;
        if(isFirstFrame)
        {
            offset = cam.transform.position - data.gameObjects[0].transform.position;
            isFirstFrame = false;
        }
        int speed = SurvivalShooterGame.survivalShooterSettings.cameraSpeed;
        float dt = Time.deltaTime;

        Vector3 camNewPosition = data.gameObjects[0].transform.position + offset;

        cam.transform.position = Vector3.Lerp(cam.transform.position, camNewPosition, speed * dt);
    }
}
