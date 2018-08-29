using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public sealed class SurvivalShooterGame {
    public static SurvivalShooterSettings survivalShooterSettings;
    public static Camera mainCamera;
    public static EntityManager entityManager = World.Active.GetOrCreateManager<EntityManager>();

    public static void NewGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag(GameString.player);
        Entity entity = player.GetComponent<GameObjectEntity>().Entity;
        entityManager.AddComponentData(entity, new PlayerInput() { Move = new Vector3(0, 0, 0) });
        entityManager.AddComponentData(entity, new Health() { value = survivalShooterSettings.playerStartHealth });

        mainCamera = Camera.main;

        #region TEST ENEMY
        //for (int i = 0; i < survivalShooterSettings.numberOfSmallEnemy; i++)
        //{
        //    GameObject zombear = UnityEngine.Object.Instantiate(survivalShooterSettings.Zombear);
        //    Entity zombearEntity = zombear.GetComponent<GameObjectEntity>().Entity;
        //    entityManager.AddComponentData(zombearEntity, new Enemy());
        //    entityManager.AddComponentData(zombearEntity, new Health() { value = survivalShooterSettings.enemyStartHealth });
        //    zombear.SetActive(false);

        //    GameObject zombunny = UnityEngine.Object.Instantiate(survivalShooterSettings.Zombunny);
        //    Entity zombunnyEntity = zombunny.GetComponent<GameObjectEntity>().Entity;
        //    entityManager.AddComponentData(zombunnyEntity, new Enemy());
        //    entityManager.AddComponentData(zombunnyEntity, new Health() { value = survivalShooterSettings.enemyStartHealth });
        //    zombunny.SetActive(false);

        //    zombearStack.Push(zombear);
        //    zombunnyStack.Push(zombunny);
        //}

        //for (int i = 0; i < survivalShooterSettings.numberOfBigEnemy; i++)
        //{
        //    GameObject hellephant = UnityEngine.Object.Instantiate(survivalShooterSettings.Hellephant);
        //    Entity hellephantEntity = hellephant.GetComponent<GameObjectEntity>().Entity;
        //    entityManager.AddComponentData(hellephantEntity, new Enemy());
        //    entityManager.AddComponentData(hellephantEntity, new Health() { value = survivalShooterSettings.enemyStartHealth });
        //    hellephant.SetActive(false);

        //    hellephantStack.Push(hellephant);
        //}
        #endregion
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitSettings()
    {
        GameObject settings = GameObject.Find("Settings");
        if(settings)
        {
            survivalShooterSettings = settings.GetComponent<SurvivalShooterSettings>();
        }
    }
}
