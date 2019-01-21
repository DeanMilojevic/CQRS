using CQRS.Example;
using NUnit.Framework;

namespace CQRS.Tests
{
    [TestFixture(Description = "Tests for the MyFirstCommand")]
    public class MyFirstCommandTests
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
        public void WhenExecuted_CommandShouldReportSuccess()
        {
            var command = new MyFirstCommand(1, string.Empty);

            _dispatcher.Send(command);

            var inquiry = new Inquiry(_getCommandResult);
            var answer = inquiry.For(command);

            Assert.IsTrue(answer.Success);
        }
    }
}