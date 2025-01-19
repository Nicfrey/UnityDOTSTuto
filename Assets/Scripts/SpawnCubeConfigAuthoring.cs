using Unity.Entities;
using UnityEngine;

public class SpawnCubeConfigAuthoring : MonoBehaviour
{
    public GameObject cubePrefab;
    public int amountToSpawn;

    private class Baker : Baker<SpawnCubeConfigAuthoring>
    {
        public override void Bake(SpawnCubeConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnCubeConfig
            {
                cubePrefabEntity = GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                amountToSpawn = authoring.amountToSpawn
            });
        }
    }
}

public struct SpawnCubeConfig : IComponentData
{
    public Entity cubePrefabEntity;
    public int amountToSpawn;
}