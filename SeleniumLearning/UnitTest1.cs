using NUnit.Framework;

namespace SeleniumLearning
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup Method Execution");
            TestContext.Progress.WriteLine("TestContext for NUnit");
        }

        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("From Test One");
            Assert.Pass();
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("TearDown");
        }
    }
}