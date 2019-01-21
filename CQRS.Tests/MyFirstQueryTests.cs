using CQRS.Example;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CQRS.Tests
{
    [TestFixture]
    public class MyFirstQueryTests
    {
        private IGetCommandResult _getCommandResult;
        private MyDispatcher _dispatcher;

        [SetUp]
        public void Setup()
        {
            var executionRepository = new ExecutionRepository();
            IMailbox mailbox = executionRepository;
            IReportCommandExecution reportCommandExecution = executionRepository;

            _getCommandResult = executionRepository;
            _dispatcher = new MyDispatcher(reportCommandExecution, mailbox);
        }

        [Test]
        public void WhenExecuted_QueryShouldReturnResult()
        {
            var query = new MyFirstQuery(1);

            var result = _dispatcher.Send(query);

            var expected = new SomeInfo(1, 2);
            Assert.AreEqual(expected, result);
        }
    }
}
