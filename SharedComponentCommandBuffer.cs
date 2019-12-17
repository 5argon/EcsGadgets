using System;
using Unity.Collections;
using Unity.Entities;

namespace E7.EcsGadgets
{
    /// <summary>
    /// You can set SCD to an entity in job, and it is burstable unlike
    /// the one on ECB.
    /// .
    /// It is just that you have to ensure your SCD is componsed of
    /// only unmanaged fields on your own. You need this per one type of SCD
    /// since all SCD API are gated tightly by generic typing.
    /// </summary>
    internal struct SharedComponentCommandBuffer<T> : IDisposable
        where T : struct, ISharedComponentData
    {
        NativeList<T> scdValues;
        NativeList<Entity> targetEntity;
        public SharedComponentCommandBuffer(Allocator allocator)
        {
            scdValues = new NativeList<T>(4,allocator);
            targetEntity = new NativeList<Entity>(4,allocator);
        }

        /// <summary>
        /// Enqueue command by remembering the entity and SCD.
        /// SCD must only contains unmanaged types.
        /// </summary>
        public void SetComponentData(Entity e, T sharedComponentData)
        {
            scdValues.Add(sharedComponentData);
            targetEntity.Add(e);
        }

        public void Playback(EntityManager em)
        {
            for (int i = 0; i < scdValues.Length; i++)
            {
                em.SetSharedComponentData(targetEntity[i], scdValues[i]);
            }
        }

        public void Dispose()
        {
            scdValues.Dispose();
            targetEntity.Dispose();
        }
    }
}