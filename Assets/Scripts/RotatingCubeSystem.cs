using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct RotatingCubeSystem : ISystem // This is running on the main thread
{
    // has three different functions : OnCreate, OnUpdate and OnDestroy

    [BurstCompile] // this can be added in the struct itself, but we only need to add it on the methods
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        return;
        /*
            // RefRW is for Read-Write
            // RefRO is for Read-Only
            // Goes to every entity that have those components
            foreach (var (localTransform, rotateSpeed) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<RotateSpeed>>().WithAll<RotatingCube>())
            {
                // Time.deltaTime is not working with entities
                localTransform.ValueRW = localTransform.ValueRW.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
            }
        */

        RotatingCubeJob rotatingCubeJob = new RotatingCubeJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        rotatingCubeJob.Schedule();
    }

    [BurstCompile]
    [WithAll(typeof(RotatingCube))] // This allows that this job is not activated by the component with tag Player and only RotatingCube
    public partial struct RotatingCubeJob : IJobEntity // This allows us to make this rotation on multiple thread
    {
        // We can't access the SystemAPI.Time.DeltaTime so we need to set up ourselve during update
        public float deltaTime;
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            localTransform = localTransform.RotateY(rotateSpeed.value * deltaTime);
        }
    }

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>(); // This entity will work only if the RotateSpeed component is in the entity
    }
}
