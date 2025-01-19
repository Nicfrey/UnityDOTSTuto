using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct RotatingCubeSystem : ISystem // This is running on the main thread
{
    // has three different functions : OnCreate, OnUpdate and OnDestroy

    public void OnUpdate(ref SystemState state)
    {
        // RefRW is for Read-Write
        // RefRO is for Read-Only
        // Goes to every entity that have those components
        foreach (var (localTransform, rotateSpeed) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<RotateSpeed>>())
        {
            // Time.deltaTime is not working with entities
            localTransform.ValueRW = localTransform.ValueRW.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }
    }

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>(); // This entity will work only if the RotateSpeed component is in the entity
    }
}
