using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;

namespace E7.EcsGadgets
{
    // /// <summary>
    // /// Performs more exotic operation on any EntityQuery.
    // /// </summary>
    // public static class AdvancedQuery
    // {
    //     /// <summary>
    //     /// Adds even more SCD filter to what is already there in the query. The query's filter is not modified.
    //     /// Filters added here are one-off use. The
    //     /// </summary>
    //     public static NativeArray<T> ToComponentDataArrayMoreFilters<T,F1,F2>(EntityQuery eq, 
    //         F1 addedScd1, F2 addedScd2, 
    //         Allocator allocator, out JobHandle jobHandle)
    //         where T : struct, IComponentData
    //         where F1 : struct, ISharedComponentData
    //         where F2 : struct, ISharedComponentData
    //     {
    //         NativeArray<T> gathered = new NativeArray<T>(eq.CalculateEntityCount(), allocator);
    //         var iter = eq.GetArchetypeChunkIterator();
    //     }
    //     
    //     [BurstCompile]
    //     unsafe struct GatherMultiFilter<T, F1, F2> : IJobChunk
    //         where T : struct, IComponentData
    //         where F1 : struct, ISharedComponentData
    //         where F2 : struct, ISharedComponentData
    //     {
    //         public NativeArray<T> ComponentData;
    //         [ReadOnly] public ArchetypeChunkComponentType<T> ComponentType;
    //         
    //         [ReadOnly] public ArchetypeChunkSharedComponentType<F1> scd1;
    //         [ReadOnly] public ArchetypeChunkSharedComponentType<F2> scd2;
    //         
    //         [ReadOnly] public NativeArray<ArchetypeChunkComponentTypeDynamic> scdType;
    //     
    //         public void Execute(ArchetypeChunk chunk, int chunkIndex, int entityOffset)
    //         {
    //             var sourcePtr = chunk.GetNativeArray(ComponentType).GetUnsafeReadOnlyPtr();
    //             var destinationPtr = (byte*) ComponentData.GetUnsafePtr() + UnsafeUtility.SizeOf<T>() * entityOffset;
    //             var copySizeInBytes = UnsafeUtility.SizeOf<T>() * chunk.Count;
    //     
    //             UnsafeUtility.MemCpy(destinationPtr, sourcePtr, copySizeInBytes);
    //         }
    //     }
        
    // }
}