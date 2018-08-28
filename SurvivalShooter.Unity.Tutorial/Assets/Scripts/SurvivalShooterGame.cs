using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public sealed class SurvivalShooterGame {
    public static SurvivalShooterSettings survivalShooterSettings;
    public static Camera mainCamera;
    public static EntityManager entityManager;
    public static void NewGame()
    {
        entityManager = World.Active.GetOrCreateManager<EntityManager>();

        GameObject player = GameObject.FindGameObjectWithTag(GameString.player);
        Entity entity = player.GetComponent<GameObjectEntity>().Entity;
        entityManager.AddComponentData(entity, new PlayerInput() { Move = new Vector3(0, 0, 0) });
        entityManager.AddComponentData(entity, new Health() { value = survivalShooterSettings.playerStartHealth });

        #region TEST ENEMY
        GameObject enemy = GameObject.Find("TestEnemy");
        Entity enemyEntity = enemy.GetComponent<GameObjectEntity>().Entity;
        entityManager.AddComponentData(enemyEntity, new Enemy());
        entityManager.AddComponentData(enemyEntity, new Health() { value = survivalShooterSettings.enemyStartHealth });

        mainCamera = Camera.main;

        GameObject zombearEnemy = GameObject.Find("Zombear");
        if(zombearEnemy == null)
        {
            Debug.Log("Zombear is null");
            return;
        }
        else
        {
            Debug.Log("Zombear is not null");
            Entity zombearEntity = zombearEnemy.GetComponent<GameObjectEntity>().Entity;
            if (zombearEntity == null)
            {
                Debug.Log("Zombear Entity is null");
            }
            else
            {
                Debug.Log("Zombear Entity is not null");
                entityManager.AddComponentData(zombearEntity, new Enemy());
                entityManager.AddComponentData(zombearEntity, new Health() { value = survivalShooterSettings.enemyStartHealth });
            }
        }
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
