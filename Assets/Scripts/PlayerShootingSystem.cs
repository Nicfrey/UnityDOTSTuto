using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerShootingSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<Player>();
    }

    protected override void OnUpdate()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        SpawnCubeConfig spawnCubeConfig = SystemAPI.GetSingleton<SpawnCubeConfig>();
            
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(WorldUpdateAllocator);
        
        foreach (RefRO<LocalTransform> localTransform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>())
        {
            Entity spawnedEntity = entityCommandBuffer.Instantiate(spawnCubeConfig.cubePrefabEntity);
            entityCommandBuffer.SetComponent(spawnedEntity,new LocalTransform
            {
                Position = localTransform.ValueRO.Position,
                Rotation = quaternion.identity,
                Scale = 1f 
            });
        }
        
        entityCommandBuffer.Playback(EntityManager);
    }
}
