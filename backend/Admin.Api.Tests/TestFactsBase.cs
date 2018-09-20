using AutoFixture;
using AutoFixture.AutoMoq;
using Xunit;

namespace Admin.Api.Tests
{
    public abstract class TestFactsBase<T> where T : class
    {
        public IFixture Fixture { get; private set; }

        public TestFactsBase ()
        {
            Fixture = new Fixture ().Customize (new AutoMoqCustomization ());
        }

        public T GetSut ()
        {
            return Fixture.Create<T> ();
        }
    }
}