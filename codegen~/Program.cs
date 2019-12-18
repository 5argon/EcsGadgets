using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace codegen
{
    class Program
    {
        static int[] tag = new int[] { 0, 1, 2, 3, 4, 5, 6 };
        static int[] scd = new int[] { 0, 1, 2 };
        static StringWriter sw = new StringWriter();

        static void Main(string[] args)
        {
            for (int i = 0; i < tag.Length; i++)
            {
                for (int j = 0; j < scd.Length; j++)
                {
                    Gen1(tag[i], scd[j]);
                }
            }
            File.WriteAllText("./EntityManagerUtilitySingleton.gen", sw.ToString());
        }

        const string gsGetSingleton = @"
/// <summary>
/// Like `GetSingleton` in system but usable from outside.
/// You can add more tag components and SCD constraints via
/// overloads provided. The first type is always the returning value.
/// </summary>
public <<MAIN>> GetSingleton<<<TYPEHOR>>>(<<ARGS>>)
<<WHERE>>
{
    using (var eq = em.CreateEntityQuery(
        <<TYPEVERT>>
    ))
    {
        <<FILTER>>
        return eq.GetSingleton<<<MAIN>>>();
    }
}
";

        const string gsGetSingletonScdFirst = @"
/// <summary>
/// Like `GetSingleton` in system but usable from outside.
/// You can add more tag components and SCD constraints via
/// overloads provided. The first type is always the returning value.
/// </summary>
public <<MAIN>> GetSingleton<<<TYPEHOR>>>(<<ARGS>>)
<<WHERE>>
{
    using (var eq = em.CreateEntityQuery(
        <<TYPEVERT>>
    ))
    {
        <<FILTER>>
        return em.GetSharedComponentData<<<MAIN>>>(eq.GetSingletonEntity());
    }
}
";


        const string gsGetSingletonEntity = @"
/// <summary>
/// Like `GetSingletonEntity` in system but usable from outside.
/// You can add more tag components and SCD constraints via
/// overloads provided.
/// </summary>
public Entity GetSingletonEntity<<<TYPEHOR>>>(<<ARGS>>)
<<WHERE>>
{
    using (var eq = em.CreateEntityQuery(
        <<TYPEVERT>>
    ))
    {
        <<FILTER>>
        return eq.GetSingletonEntity();
    }
}
";

        const string gsEntityCount = @"
/// <summary>
/// Count entities that are returned from All query of
/// all components in the overload you choose plus upto
/// 2 SCD filters.
/// </summary>
public int EntityCount<<<TYPEHOR>>>(<<ARGS>>)
<<WHERE>>
{
    using (var eq = em.CreateEntityQuery(
        <<TYPEVERT>>
    ))
    {
        <<FILTER>>
        return eq.CalculateEntityCount();
    }
}
";

        const string gsCda = @"
/// <summary>
/// Return a linearized component data array of the first component.
/// You can add additional tag components and upto 2 SCD filters to
/// the query.
/// 
/// You have to dispose the returned native array.
/// The returned native array will be allocated with Persistent allocator.
/// </summary>
public NativeArray<<<MAIN>>> ComponentDataArray<<<TYPEHOR>>>(<<ARGS>>)
<<WHERE>>
{
    using (var eq = em.CreateEntityQuery(
        <<TYPEVERT>>
    ))
    {
        <<FILTER>>
         return eq.ToComponentDataArray<<<MAIN>>>(Allocator.Persistent);
    }
}
";

        const string gsGet = @"
/// <summary>
/// Return a linearized component data array of the first component.
/// You can add additional tag components and upto 2 SCD filters to
/// the query.
/// 
/// Returns managed array, you don't have to dispose it but
/// it is not efficient for real use as it produces garbage.
/// Good for unit testing.
/// </summary>
public <<MAIN>>[] Get<<<TYPEHOR>>>(<<ARGS>>)
<<WHERE>>
{
    using (var eq = em.CreateEntityQuery(
        <<TYPEVERT>>
    ))
    {
        <<FILTER>>
        var na = eq.ToComponentDataArray<<<MAIN>>>(Allocator.Persistent);
        var array = na.ToArray();
        na.Dispose();
        return array;
    }
}
";
        const string gsEa = @"
/// <summary>
/// Return a linearized entity array of all components combined into All query.
/// You can add upto 2 SCD filters to the query.
/// 
/// You have to dispose the returned native array.
/// The returned native array will be allocated with Persistent allocator.
/// </summary>
public NativeArray<Entity> EntityArray<<<TYPEHOR>>>(<<ARGS>>)
<<WHERE>>
{
    using (var eq = em.CreateEntityQuery(
        <<TYPEVERT>>
    ))
    {
        <<FILTER>>
         return eq.ToEntityArray(Allocator.Persistent);
    }
}
";

        const string gsEntities = @"
/// <summary>
/// Return a linearized entity array of all components combined into All query.
/// You can add upto 2 SCD filters to the query.
/// 
/// You have to dispose the returned native array.
/// The returned native array will be allocated with Persistent allocator.
/// </summary>
public Entity[] Entities<<<TYPEHOR>>>(<<ARGS>>)
<<WHERE>>
{
    using (var eq = em.CreateEntityQuery(
        <<TYPEVERT>>
    ))
    {
        <<FILTER>>
        var na = eq.ToEntityArray(Allocator.Persistent);
        var array = na.ToArray();
        na.Dispose();
        return array;
    }
}
";

        static void Gen1(int tag, int scd)
        {
            if(tag == 0 && scd == 0) return;
            List<string> tags = new List<string>();
            List<string> scds = new List<string>();
            List<string> args = new List<string>();
            for (int i = 0; i < tag; i++)
            {
                tags.Add($"CD{i + 1}");
            }
            for (int i = 0; i < scd; i++)
            {
                tags.Add($"SCD{i + 1}");
                scds.Add($"filter{i + 1}");
                args.Add($"SCD{i + 1} filter{i + 1}");
            }
            string typeHor = string.Join(",", tags);
            string argsString = string.Join(",", args);
            string wheres = string.Join("\n", tags.Select(x =>
            {
                if (x.Contains("SCD"))
                {
                    return $"where {x} : struct, ISharedComponentData";
                }
                else
                {
                    return $"where {x} : struct, IComponentData";
                }
            }));
            string typeVert = string.Join(",\n", tags.Select(x => $"ComponentType.ReadOnly<{x}>()"));
            string filters = scd > 0 ? $"eq.SetSharedComponentFilter({string.Join(",", scds)});" : string.Empty;

            if (tag != 0)
            {
                Do(gsGetSingleton);
            }
            else
            {
                Do(gsGetSingletonScdFirst);
            }

            Do(gsGetSingletonEntity);
            Do(gsEntityCount);

            if (tag != 0)
            {
                Do(gsCda);
                Do(gsGet);
            }

            Do(gsEa);
            Do(gsEntities);

            void Do(string tem)
            {
                sw.WriteLine(tem
                    .Replace("<<MAIN>>",tags[0])
                    .Replace("<<TYPEHOR>>", typeHor)
                    .Replace("<<ARGS>>", argsString)
                    .Replace("<<WHERE>>", wheres)
                    .Replace("<<TYPEVERT>>", typeVert)
                    .Replace("<<FILTER>>", filters)
                );
            }
        }
    }
}
