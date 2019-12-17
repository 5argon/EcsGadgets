using System;
using Unity.Collections;
using Unity.Entities;

namespace E7.EcsGadgets
{
    /// <summary>
    /// You can defer set SCD to an entity in job, and it is burstable unlike
    /// the one on ECB.
    /// It is just that you have to ensure your SCD is composed of
    /// only unmanaged fields on your own. You need this per one type of SCD
    /// since all SCD API are gated tightly by generic typing.
    /// </summary>
    public struct SharedComponentCommandBuffer<T> : IDisposable
        where T : struct, ISharedComponentData
    {
        [ReadOnly] NativeQueue<T> scdValues;
        [ReadOnly] NativeQueue<Entity> targetEntity;
        
        //When this go into jobs the thread index would be set on these two.
        NativeQueue<T>.ParallelWriter scdValuesW;
        NativeQueue<Entity>.ParallelWriter targetEntityW;
        
        public SharedComponentCommandBuffer(Allocator allocator)
        {
            scdValues = new NativeQueue<T>(allocator);
            targetEntity = new NativeQueue<Entity>(allocator);
            scdValuesW = scdValues.AsParallelWriter();
            targetEntityW = targetEntity.AsParallelWriter();
        }

        /// <summary>
        /// Enqueue command by remembering the entity and SCD.
        /// SCD must only contains unmanaged types.
        /// </summary>
        public void SetSharedComponent(Entity e, T sharedComponentData)
        {
            scdValuesW.Enqueue(sharedComponentData);
            targetEntityW.Enqueue(e);
        }

        public void Playback(EntityManager em)
        {
            while (scdValues.Count > 0 && targetEntity.Count > 0)
            {
                em.SetSharedComponentData(targetEntity.Dequeue(), scdValues.Dequeue());
            }
        }

        public void Dispose()
        {
            scdValues.Dispose();
            targetEntity.Dispose();
        }
    }
}