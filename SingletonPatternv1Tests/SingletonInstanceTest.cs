using FluentAssertions;
using SingletonPatternv1;
using Xunit;
using Xunit.Abstractions;

namespace SingletonPatternv1Tests
{
    public class SingletonInstanceTest
    {
        private readonly ITestOutputHelper _output;

        public SingletonInstanceTest(ITestOutputHelper output)
        {
            _output = output;
            SingletonTestHelpers.Reset(typeof(Singleton));
            Logger.Clear();
        }

        [Fact]
        public void ReturnsNonNullSingletonInstance()
        {
            Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<Singleton>());

            var result = Singleton.Instance;

            Assert.NotNull(result);
            Assert.IsType<Singleton>(result);

            Logger.Output().ToList().ForEach(h => _output.WriteLine(h));
        }

        [Fact]
        public void OnlyCallsConstructorOnceGivenThreeInstanceCalls()
        {
            Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<Singleton>());

            // configure logger to slow down the creation longer than the pauses below
            Logger.DelayMilliseconds = 10;

            var result1 = Singleton.Instance;
            Thread.Sleep(1);
            var result2 = Singleton.Instance;
            Thread.Sleep(1);
            var result3 = Singleton.Instance;

            var log = Logger.Output();

            result1.Should().BeSameAs(result2).And.BeSameAs(result3);
        }

        [Fact]
        public void CallsConstructorMultipleTimesGivenThreeParallelInstanceCalls()
        {
            Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<Singleton>());

            // configure logger to slow down the creation long enough to cause problems
            Logger.DelayMilliseconds = 50;

            var strings = new List<string>() { "one", "two", "three" };
            var instances = new List<Singleton>();
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 3 };
            Parallel.ForEach(strings, options, instance =>
            {
                instances.Add(Singleton.Instance);
            });

            var log = Logger.Output();
            try
            {
                Assert.Equal(1, log.Count(log => log.Contains("Constructor")));
                Assert.Equal(3, log.Count(log => log.Contains("Instance")));
            }
            finally
            {
                Logger.Output().ToList().ForEach(h => _output.WriteLine(h));
            }
        }
    }
}