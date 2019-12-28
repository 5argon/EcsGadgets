using Unity.Entities;

// ReSharper disable InconsistentNaming

namespace E7.EcsGadgets
{
    /// <summary>
    /// Base class to begin writing a world-based test. It pours all systems available in to the world.
    /// You then create some entities and run `w.Update()` in sequences and check results.
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
    /// Testing this way is harder that you are trying to test a single system, yet it is not a unit test,
    /// you have no control that the input entity will be modified before it actually arrives at
    /// the system you are targeting or not. An input must be designed for the world rather than for a single system.
    ///
    /// But if you could it would create a stronger test than a unit test when considering a system as 1 unit :
    /// 
    /// - Catch bugs related to prior systems added later modifying starting data, including command buffer systems.
    /// - No need to write a similar integration test that the only difference is that the system is now among others.
    /// Now you can write one test that give you about the same confidence as a unit test plus integration tests over
    /// multiple systems. No more fixing 2 double red tests that are almost the same.
    /// - Newly added systems are automatically imbued into all written tests. Cherry picking related systems for each
    /// tests manually is hard and tiring. Usually when unit testing a system, you then want all thos Before/After in
    /// the world with it too because they are in its definition and you want to see their interaction. This got annoying
    /// so in the end I just test the world rather than each system, but with objective to test each system in each test.
    /// - Write concise test with only setup related to entities. When you don't have to prop up correct system environment
    /// it may made you more want to write more tests.
    /// - If you write a unit test, the test often break in compile error
    /// when you change the system's definition around (and system with its
    /// modularity encourages this) in a way that it achieves the same result,
    /// the test was too tight. A world test that aims to unit test a system maybe less prone to
    /// compile error, instead giving you back a useful red test because overall functionality related to all
    /// other systems changed.
    ///
    /// Disadvantages :
    /// 
    /// - It is not called a unit test. If you follow software developement methodology strongly that nothing could
    /// replace unit test, this is not an option for you.
    /// - Hard to predict end result in the state that all systems are present. You have to carefully set input data
    /// that most of the things except what you are testing are neutral. (0, `false`, null, etc.)
    /// - You may need to design so it fits with this testing method. The same way there are both testable and
    /// untestable OOP designs.
    ///
    /// Fall back to single system test when you are not able to handle the state where every systems unpredictably
    /// changing everything. But personally I will try to default to this way as much as possible. When the world
    /// is crowded, separating UPM package maybe the solution to continue testing this way since ECS scans only
    /// available assembly to populate the world.
    ///
    /// Of course with tons of time you can do both unit system test and world test. But world test is only a little
    /// bit harder and cover what unit test did really well it is tempting to cut your dev time by half by ignoring
    /// unit system tests altogether.
    /// </remarks>
    public abstract class ECSTestBase
    {
        protected World w { get; private set; }
        protected EntityManager em { get; private set; }
        protected EntityManagerAssertion ema { get; private set; }

        protected void SetUpBase()
        {
            w = new World("Test World");
            em = w.EntityManager;
            ema = new EntityManagerAssertion(w);
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