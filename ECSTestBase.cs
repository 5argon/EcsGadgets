using Unity.Entities;

// ReSharper disable InconsistentNaming

namespace E7.EcsGadgets
{
    /// <summary>
    /// Everything must work after pouring all the systems into the world "mindlessly".
    /// 
    /// Subclass this then create a `[SetUp]` and `[TearDown]` that calls <see cref="SetUpBase"/>
    /// and <see cref="TearDownBase"/>, because I don't want to have a reference to NUnit.
    /// 
    /// ```
    /// [SetUp] public void SetUp() => SetUpBase();
    /// [TearDown] public void TearDown() => TearDownBase();
    /// ```
    /// </summary>
    /// <remarks>
    /// By basing all your tests on this without cherry picking including systems
    /// you can test the strength of your system order. Of course this is best when you
    /// design your systems in a secluded UPM package so that limits the systems included
    /// from all assemblies via reflection, to just what you are interested in.
    /// </remarks>
    public abstract class ECSTestBase
    {
        protected World w { get; private set; }
        protected EntityManager em { get; private set; }
        protected EntityManagerUtility emu { get; private set; }

        protected void SetUpBase()
        {
            w = new World("Test World");
            em = w.EntityManager;
            emu = new EntityManagerUtility(w);
            var allSystems =
                DefaultWorldInitialization.GetAllSystems(WorldSystemFilterFlags.Default, requireExecuteAlways: false);
            allSystems.Add(typeof(ConstantDeltaTimeSystem)); //this has disable auto creation on it.
            DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(w, allSystems);
        }

        /// <summary>
        /// Call to make the next world update go in a specific time.
        /// </summary>
        protected void ForceDeltaTime(float deltaTime)
        {
            w.GetExistingSystem<ConstantDeltaTimeSystem>().ForceDeltaTime(deltaTime);
        }

        protected void TearDownBase()
        {
            w.Dispose();
        }
    }
}