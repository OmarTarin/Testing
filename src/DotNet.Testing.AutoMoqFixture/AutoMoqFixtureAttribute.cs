using AutoFixture;
using AutoFixture.Xunit2;

namespace DotNet.Testing.AutoMoqFixture
{
    public class AutoMoqFixtureAttribute : AutoDataAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repeatCount">count of a list/array</param>
        /// <param name="recursionDepth">depht of an recursiv object</param>
        /// <param name="configureMembers">Specifies whether members of a mock will be automatically setup to retrieve the return values from a fixture.</param>
        public AutoMoqFixtureAttribute(int repeatCount = 3, int recursionDepth = 3, bool configureMembers = false)
            : base(() => GetFixture(repeatCount, configureMembers, recursionDepth))
        { }
        
        private static Fixture GetFixture(int repeatCount, bool configureMembers, int recursionDepth)
        {
            var fixture = new FixtureBuilder()
                .CustomizeAutoMoq(configureMembers)
                .CustomizeServiceProvider()
                .RecursionDepth(recursionDepth)
                .GetFixture();

            fixture.RepeatCount = repeatCount;

            return fixture;
        }
    }
}
