using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerShootingSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public ComponentArray<ParticleSystem> particleSystems;
        public ComponentArray<LineRenderer> lineRenderers;
        public ComponentArray<AudioSource> audioSources;
        public ComponentArray<Light> lights;
        private SubtractiveComponent<Dead> deads;
    }

    [Inject] private Data data;

    float timer;
    LayerMask layerMask;
    protected override void OnUpdate()
    {
        float shootCoolDown = SurvivalShooterGame.survivalShooterSettings.shootCoolDown;
        float shootEffectDisplayTime = SurvivalShooterGame.survivalShooterSettings.shootEffectDisplayTime;

        timer += Time.deltaTime;
        layerMask = LayerMask.GetMask(GameString.enviromentLayer);
        for (int i = 0; i < data.Length; ++i)
        {
            if(Input.GetButton("Fire1") && timer >= shootCoolDown)
            {
                Shoot(data, i);
            }

            if (timer >= shootEffectDisplayTime * shootCoolDown)
            {
                DisableEffect(data, i);
            }
        }
    }

    void Shoot(Data data, int i)
    {
        timer = 0;

        data.audioSources[i].Play();
        data.lights[i].enabled = true;

        data.particleSystems[i].Stop();
        data.particleSystems[i].Play();

        data.lineRenderers[i].enabled = true;
        data.lineRenderers[i].SetPosition(0, data.gameObjects[i].transform.position);

        Ray shootRay = new Ray();
        shootRay.origin = data.gameObjects[i].transform.position;
        shootRay.direction = data.gameObjects[i].transform.forward;

        int shootRange = SurvivalShooterGame.survivalShooterSettings.shootRange;
        RaycastHit shootHit;
        if(Physics.Raycast(shootRay, out shootHit, shootRange, layerMask))
        {
            GameObjectEntity objectEntity = shootHit.collider.gameObject.GetComponent<GameObjectEntity>();
            Debug.Log("Hit: " + shootHit.collider.gameObject.name);
            GameUI.Instance.OnKillEnemy();
            if(objectEntity)
            {
            }

            data.lineRenderers[i].SetPosition(1, shootHit.point);
        }
        else
        {
            data.lineRenderers[i].SetPosition(1, shootRay.origin + shootRay.direction * shootRange);
        }
    }

    void DisableEffect(Data data, int i)
    {
        data.lineRenderers[i].enabled = false;
        data.lights[i].enabled = false;
    }
}
