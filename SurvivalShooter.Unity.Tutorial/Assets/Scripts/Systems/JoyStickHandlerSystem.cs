using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class JoyStickHandlerSystem : ComponentSystem {
    public struct Data
    {
        public readonly int Length;
        public readonly ComponentArray<JoystickController> joystickControllers;
        public ComponentDataArray<Shoot> shoots;
        private SubtractiveComponent<Dead> deads;
    }

    [Inject] Data data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; ++i)
        {
            int canShoot = data.joystickControllers[i].rotateJoystick.isClicked ? 1 : 0;
            data.shoots[i] = new Shoot() { value = canShoot };
        }
    }
}
