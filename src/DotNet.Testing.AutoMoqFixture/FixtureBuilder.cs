using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace DotNet.Testing.AutoMoqFixture
{
    public class FixtureBuilder
    {
        private readonly Fixture _fixture;

        public FixtureBuilder()
        {
            _fixture = new Fixture();
        }

        public FixtureBuilder(Fixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }


        public FixtureBuilder CustomizeServiceProvider()
        {
            _fixture.Customize(new ServiceProviderCustomization());
            return this;
        }

        public FixtureBuilder CustomizeAutoMoq(bool configureMembers = false)
        {
            _fixture.Customize(new AutoMoqCustomization()
            {
                ConfigureMembers = configureMembers
            });

            return this;
        }

        public FixtureBuilder RecursionDepth(int recursionDepth)
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(recursionDepth));

            return this;
        }

        public Fixture GetFixture()
        {
            return _fixture;
        }
    }
}
