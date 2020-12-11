using System.Linq;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace DotNet.Testing.AutoMoqFixture
{
    public class AutoMoqFixture : Fixture
    {
        private ITestOutputHelper _logger;


        public AutoMoqFixture(int repeatCount = 3, int recursionDepth = 3, bool configureMembers=false)
        {
            AddCustomitation(repeatCount, recursionDepth, configureMembers);
        }

        public AutoMoqFixture(ITestOutputHelper logger, int repeatCount = 3, int recursionDepth = 3, bool configureMembers = false) : this(repeatCount, recursionDepth, configureMembers)
        {
            _logger = logger;
        }

        private void AddCustomitation(int repeatCount, int recursionDepth, bool configureMembers)
        {
            new FixtureBuilder(this)
                .CustomizeAutoMoq(configureMembers)
                .CustomizeServiceProvider()
                .RecursionDepth(recursionDepth);

            RepeatCount = repeatCount;
        }
        
        public bool InjectLogger<T>()
        {
            if (_logger != null)
            {
                var l = (ILogger<T>) _logger.BuildLoggerFor<T>();
                this.Register(() => l);
                return true;
            }
            return false;
        }

    }
}
