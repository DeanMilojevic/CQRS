using CQRS.Example;
using NUnit.Framework;

namespace CQRS.Tests
{
    [TestFixture(Description = "Tests for the MyFirstQuery")]
    public class MyFirstQueryTests
    {
        private MyDispatcher _dispatcher;

        [SetUp]
        public void Setup()
        {
            var executionRepository = new ExecutionRepository();
            IMailbox mailbox = executionRepository;
            IReportCommandExecution reportCommandExecution = executionRepository;

            _dispatcher = new MyDispatcher(reportCommandExecution, mailbox);
        }

        [Test]
        public void WhenExecuted_QueryShouldReturnResult()
        {
            // arrange
            var query = new MyFirstQuery(1);
            var expected = new SomeInfo(1, 2);

            // act
            var result = _dispatcher.Send(query);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}
