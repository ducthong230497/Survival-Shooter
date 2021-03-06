﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerShootingSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public EntityArray entities;
        public ComponentArray<ParticleSystem> particleSystems;
        public ComponentArray<LineRenderer> lineRenderers;
        public ComponentArray<AudioSource> audioSources;
        public ComponentArray<Light> lights;
        public ComponentDataArray<Shoot> shoots;
        private SubtractiveComponent<Dead> deads;
    }

    [Inject] private Data data;

    float timer;
    float shootCoolDown;
    float shootEffectDisplayTime;
    LayerMask layerMask;
    EntityCommandBuffer entityCommandBuffer;

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;
        entityCommandBuffer = PostUpdateCommands;
        for (int i = 0; i < data.Length; ++i)
        {
            //Debug.Log($"name: {data.gameObjects[i].name}, has shoot: {SurvivalShooterGame.entityManager.HasComponent<Shoot>(data.entities[i])}");
            if (data.shoots[i].value == 1 && timer >= shootCoolDown)
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
            GameObjectEntity gameObjectEntity = shootHit.collider.gameObject.GetComponent<GameObjectEntity>();
            
            //Debug.Log("Hit: " + shootHit.collider.gameObject.name);
            //GameUI.Instance.OnKillEnemy();
            if(gameObjectEntity)
            {
                Entity entity = gameObjectEntity.Entity;
                if (entity != null)
                {
                    if (!SurvivalShooterGame.entityManager.HasComponent<Damage>(entity))
                    {
                        entityCommandBuffer.AddComponent(entity, new Damage() { value = SurvivalShooterGame.survivalShooterSettings.playerShootDamage, hitPoint = shootHit.point });
                    }
                }
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

    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        layerMask = LayerMask.GetMask(GameString.enviromentLayer) | LayerMask.GetMask(GameString.enemyLayer);

        shootCoolDown = SurvivalShooterGame.survivalShooterSettings.shootCoolDown;
        shootEffectDisplayTime = SurvivalShooterGame.survivalShooterSettings.shootEffectDisplayTime;
    }
}
