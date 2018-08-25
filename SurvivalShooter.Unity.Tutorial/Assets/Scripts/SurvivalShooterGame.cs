using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public sealed class SurvivalShooterGame {
    public static SurvivalShooterSettings survivalShooterSettings;
    public static Camera mainCamera;
    public static void NewGame()
    {
        EntityManager entityManager = World.Active.GetOrCreateManager<EntityManager>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Entity entity = player.GetComponent<GameObjectEntity>().Entity;
        entityManager.AddComponentData(entity, new PlayerInput() { Move = new Vector3(0, 0, 0) });
        entityManager.AddComponentData(entity, new Health() { health = survivalShooterSettings.startingHealth });

        mainCamera = Camera.main;
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
