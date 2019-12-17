using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Assertions;

namespace E7.EcsGadgets
{
    /// <summary>
    /// Performs higher level, one-off operation on <see cref="EntityManager"/> that it allocates
    /// and immediately dispose <see cref="EntityQuery"/> inside each call. Useful for unit testing.
    /// </summary>
    public class EntityManagerUtility
    {
        EntityManager em;
        List<int> scdIndices;
        
        public EntityManagerUtility(World world)
        {
            this.em = world.EntityManager;
            scdIndices = new List<int>();
        }

        /// <summary>
        /// Like `GetSingleton` in systems except it is usable from outside.
        /// </summary>
        public T GetSingleton<T>() where T : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(ComponentType.ReadOnly<T>()))
            {
                return eq.GetSingleton<T>();
            }
        }

        /// <summary>
        /// Special GetSingleton where you are able to add one more tag component. The returned type is of the first one.
        /// It still contains a check for singleton. There must be only 1 Entity that matches this All query of T1 and T2.
        /// </summary>
        public MAIN GetSingleton<MAIN, TAG>()
            where MAIN : struct, IComponentData
            where TAG : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(ComponentType.ReadOnly<MAIN>(), ComponentType.ReadOnly<TAG>()))
            {
                return eq.GetSingleton<MAIN>();
            }
        }

        /// <summary>
        /// Like `GetSingletonEntity` in systems except it is usable from outside.
        /// </summary>
        public Entity GetSingletonEntity<T>() where T : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(ComponentType.ReadOnly<T>()))
            {
                return eq.GetSingletonEntity();
            }
        }

        /// <summary>
        /// Special GetSingletonEntity where you are able to add one more tag component. The returned type is of the first one.
        /// It still contains a check for singleton. There must be only 1 Entity that matches this All query of T1 and T2.
        /// </summary>
        public Entity GetSingletonEntity<MAIN, TAG>()
            where MAIN : struct, IComponentData
            where TAG : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(ComponentType.ReadOnly<MAIN>(), ComponentType.ReadOnly<TAG>()))
            {
                return eq.GetSingletonEntity();
            }
        }

        /// <summary>
        /// Query entity with All condition for T and count them.
        /// </summary>
        public int EntityCount<T>()
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<T>()
            ))
            {
                return eq.CalculateEntityCount();
            }
        }

        /// <summary>
        /// Query entity with All condition for T1 and T2 and count them.
        /// </summary>
        public int EntityCount<T1, T2>()
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<T1>(),
                ComponentType.ReadOnly<T2>()))
            {
                return eq.CalculateEntityCount();
            }
        }

        /// <summary>
        /// Query entity with any EQD and count them.
        /// </summary>
        public int EntityCount(EntityQueryDesc eqd)
        {
            using (var eq = em.CreateEntityQuery(eqd))
            {
                return eq.CalculateEntityCount();
            }
        }

        /// <summary>
        /// Performs a linear search over all existing SCD of a single type to find out the index.
        /// The search equality test is by List.IndexOf
        /// Remember that the index may become unusable once the SCD order version changed.
        /// </summary>
        /// <param name="scdValues">In order for this method to not generate garbage, please bring your own
        /// managed List of the type you want that you allocate on OnCreate in your system. Just bring it in,
        /// this method has a .Clear inside.</param>
        /// <returns>-1 when not found. That means this SCD value has not been used anywhere yet or not used anymore.</returns>
        public int IndexOfSharedComponentData<T>(T sharedComponentValue, List<T> scdValues) where T : struct, ISharedComponentData
        {
            scdValues.Clear();
            scdIndices.Clear();
            em.GetAllUniqueSharedComponentData(scdValues, scdIndices);
            var indexOf = scdValues.IndexOf(sharedComponentValue);
            return indexOf == -1 ? -1 : scdIndices[indexOf];
        }
    }
}