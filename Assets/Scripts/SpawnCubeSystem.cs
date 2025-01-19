using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class SpawnCubeSystem : SystemBase
{
    
    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubeConfig>();
    }

    protected override void OnUpdate()
    {
        Enabled = false; // Will be called once
        SpawnCubeConfig spawnCubeConfig = SystemAPI.GetSingleton<SpawnCubeConfig>();

        for (int i = 0; i < spawnCubeConfig.amountToSpawn; ++i)
        {
            Entity spawnedEntity = EntityManager.Instantiate(spawnCubeConfig.cubePrefabEntity);
            SystemAPI.SetComponent(spawnedEntity,new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-10f,5f),0.6f,UnityEngine.Random.Range(-10f,5f)),
                Rotation = quaternion.identity,
                Scale = 1f // do that because otherwise the scale will be 0
            });
        }
    }
}
