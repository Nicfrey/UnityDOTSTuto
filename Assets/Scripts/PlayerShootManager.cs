using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{

    [SerializeField] private GameObject shootPopupPrefab;
    private void Start()
    {
        PlayerShootingSystem playerShootingSystem = 
            World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();

        playerShootingSystem.OnShoot += OnShoot;
    }

    private void OnShoot(object sender, EventArgs e)
    {
        Entity playerEntity = (Entity)sender;
        LocalTransform localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
        Instantiate(shootPopupPrefab, localTransform.Position,Quaternion.identity);
    }
}
