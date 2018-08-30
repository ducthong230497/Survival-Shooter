using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyDeadSystem : ComponentSystem {
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Enemy> ememies;
        public ComponentDataArray<Dead> deads;
        public ComponentArray<Animator> animators;
        public ComponentArray<CapsuleCollider> capsuleColliders;
        public ComponentArray<AudioSource> audioSources;
    }

    [Inject] Data data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; ++i)
        {

            if(data.deads[i].value == 0)
            {
                data.capsuleColliders[i].isTrigger = true;
                data.deads[i] = new Dead() { value = 1 };
                data.animators[i].SetTrigger("Dead");
                data.audioSources[i].clip = SurvivalShooterGame.survivalShooterSettings.enemyDeadClip;
                data.audioSources[i].Play();
            }

        }
    }

    protected override void OnStopRunning()
    {
        base.OnStopRunning();
        for (int i = 0; i < data.Length; ++i)
        {
            data.animators[i].ResetTrigger("Dead");
        }
    }
}
