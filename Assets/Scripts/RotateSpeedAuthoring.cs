using Unity.Entities;
using UnityEngine;

public class RotateSpeedAuthoring : MonoBehaviour // The name of the file should be authoring and not the component data
{
    public float value; // Should be the same name as the entity component

    private class Baker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic); // Dynamic because we want to rotate the entity
            AddComponent(entity, new RotateSpeed { value = authoring.value });
        }
    }
}

public struct RotateSpeed : IComponentData
{
    public float value;
}
