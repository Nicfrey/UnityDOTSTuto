using Unity.Entities;
using Unity.Transforms;

public partial struct HandleCubeSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (localTransform,rotateSpeed, movement) in 
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>, RefRO<Movement>>().WithAll<RotatingCube>())
        {
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
            localTransform.ValueRW = localTransform.ValueRO.Translate(movement.ValueRO.movementVector * SystemAPI.Time.DeltaTime);
        }
    }
}
