using System;
using Unity.Collections;
using Unity.Entities;

namespace E7.EcsGadgets
{
    /// <summary>
    /// Performs higher level, one-off operation on <see cref="EntityManager"/>.
    /// Many methods allocate and immediately dispose <see cref="EntityQuery"/> inside each call.
    ///
    /// This system independent shortcuts are useful for unit testing so you can query and check in one line.
    ///
    /// There is no `CompleteAllJobs` inside. There is a chance that it would cause
    /// error when someone else is reading/writing the same data.
    /// </summary>
    public partial class EntityManagerAssertion
    {
        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public SCD1 GetSingleton<SCD1>(SCD1 filter1)
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return em.GetSharedComponentData<SCD1>(eq.GetSingletonEntity());
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<SCD1>(SCD1 filter1)
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<SCD1>(SCD1 filter1)
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<SCD1>(SCD1 filter1)
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public SCD1 GetSingleton<SCD1>(bool nf)
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return em.GetSharedComponentData<SCD1>(eq.GetSingletonEntity());
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<SCD1>(bool nf)
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<SCD1>(bool nf)
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<SCD1>(bool nf)
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public SCD1 GetSingleton<SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return em.GetSharedComponentData<SCD1>(eq.GetSingletonEntity());
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public SCD1 GetSingleton<SCD1, SCD2>(bool nf1, SCD2 filter2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return em.GetSharedComponentData<SCD1>(eq.GetSingletonEntity());
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<SCD1, SCD2>(bool nf1, SCD2 filter2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<SCD1, SCD2>(bool nf1, SCD2 filter2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<SCD1, SCD2>(bool nf1, SCD2 filter2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public SCD1 GetSingleton<SCD1, SCD2>(bool nf1, bool nf2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return em.GetSharedComponentData<SCD1>(eq.GetSingletonEntity());
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<SCD1, SCD2>(bool nf1, bool nf2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<SCD1, SCD2>(bool nf1, bool nf2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<SCD1, SCD2>(bool nf1, bool nf2)
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1>()
            where CD1 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1>()
            where CD1 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1>()
            where CD1 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1>()
            where CD1 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1>()
            where CD1 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3>(Func<CD1, CD2, CD3, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3>(Func<CD1, CD2, CD3, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1>(Func<CD1, CD2, CD3, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1>(Func<CD1, CD2, CD3, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4>(Func<CD1, CD2, CD3, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4>(Func<CD1, CD2, CD3, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4>(Func<CD1, CD2, CD3, CD4, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4>(Func<CD1, CD2, CD3, CD4, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, CD3, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, CD3, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5>(Func<CD1, CD2, CD3, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5>(Func<CD1, CD2, CD3, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5>(Func<CD1, CD2, CD3, CD4, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5>(Func<CD1, CD2, CD3, CD4, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5>(Func<CD1, CD2, CD3, CD4, CD5, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5>(Func<CD1, CD2, CD3, CD4, CD5, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, CD6>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, CD6>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, CD6>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6>()
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, CD3, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, CD3, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, CD3, CD4, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, CD3, CD4, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, CD3, CD4, CD5, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, CD3, CD4, CD5, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, bool> where, bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where,
            SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where,
            SCD1 filter1)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                eq.SetSharedComponentFilter(filter1);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where,
            bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where,
            bool nf)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Like `GetSingleton` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// The first type is always the returning value.
        /// </summary>
        public CD1 GetSingleton<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingleton<CD1>();
            }
        }


        /// <summary>
        /// Like `GetSingletonEntity` in system but usable from outside.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        public Entity GetSingletonEntity<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                return eq.GetSingletonEntity();
            }
        }


        /// <summary>
        /// Return a linearized component data array of the first component of generic type arguments.
        /// You can add additional components upto 6 CD and upto 2 SCD types to the query.
        /// </summary>
        public CD1[] Components<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToComponentDataArray<CD1>(Allocator.Persistent);
                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, SCD1 filter1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where,
            bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where, bool nf1,
            bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, bool> where,
            bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, bool> where,
            bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where,
            SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(
            Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where, SCD1 filter1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter1, filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where,
            bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(
            Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where, bool nf1, SCD2 filter2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                eq.SetSharedComponentFilter(filter2);
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }


        /// <summary>
        /// Count entities that are returned from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public int EntityCount<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where,
            bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                int count = na.Length;
                na.Dispose();
                return count;
            }
        }


        /// <summary>
        /// Return a linearized entity array from All query made of all components on generic type arguments.
        /// You can add upto 0~6 CD and 0~2 SCD types to the query.
        /// </summary>
        /// <remarks>
        /// In the argument :
        /// 
        /// - Add a lambda with input argument typed the same as component data specified
        /// in the generic type argument. This is a filter to only work on an entity that
        /// pass the criteria. (Like LINQ's `.Where`) It is possible to specify just a subset
        /// of all component data in the generic type as long as the omitted types come later
        /// when counting from left to right.
        /// 
        /// - Add from 0 to 2 SCD value filter, if you have enough SCD generic type specified.
        /// Use `nf: false` in place of actual value to skip filtering that SCD type.
        /// With that it is possible to use SCD types as one of tag components.
        /// </remarks>
        public Entity[] Entities<CD1, CD2, CD3, CD4, CD5, CD6, SCD1, SCD2>(
            Func<CD1, CD2, CD3, CD4, CD5, CD6, bool> where, bool nf1, bool nf2)
            where CD1 : struct, IComponentData
            where CD2 : struct, IComponentData
            where CD3 : struct, IComponentData
            where CD4 : struct, IComponentData
            where CD5 : struct, IComponentData
            where CD6 : struct, IComponentData
            where SCD1 : struct, ISharedComponentData
            where SCD2 : struct, ISharedComponentData
        {
            using (var eq = em.CreateEntityQuery(
                ComponentType.ReadOnly<CD1>(),
                ComponentType.ReadOnly<CD2>(),
                ComponentType.ReadOnly<CD3>(),
                ComponentType.ReadOnly<CD4>(),
                ComponentType.ReadOnly<CD5>(),
                ComponentType.ReadOnly<CD6>(),
                ComponentType.ReadOnly<SCD1>(),
                ComponentType.ReadOnly<SCD2>()
            ))
            {
                var na = eq.ToEntityArray(Allocator.Persistent);

                NativeList<Entity> filtered = new NativeList<Entity>(na.Length, Allocator.TempJob);
                using (var cd1Cda = eq.ToComponentDataArray<CD1>(Allocator.TempJob))
                using (var cd2Cda = eq.ToComponentDataArray<CD2>(Allocator.TempJob))
                using (var cd3Cda = eq.ToComponentDataArray<CD3>(Allocator.TempJob))
                using (var cd4Cda = eq.ToComponentDataArray<CD4>(Allocator.TempJob))
                using (var cd5Cda = eq.ToComponentDataArray<CD5>(Allocator.TempJob))
                using (var cd6Cda = eq.ToComponentDataArray<CD6>(Allocator.TempJob))
                {
                    for (int i = 0; i < na.Length; i++)
                    {
                        if (where(cd1Cda[i], cd2Cda[i], cd3Cda[i], cd4Cda[i], cd5Cda[i], cd6Cda[i]))
                        {
                            filtered.Add(na[i]);
                        }
                    }
                }

                na.Dispose();
                na = new NativeArray<Entity>(filtered.Length, Allocator.Persistent);
                for (int i = 0; i < filtered.Length; i++)
                {
                    na[i] = filtered[i];
                }

                filtered.Dispose();

                var array = na.ToArray();
                na.Dispose();
                return array;
            }
        }
    }
}