using CQRS.Example;
using NUnit.Framework;

namespace CQRS.Tests
{
    [TestFixture(Description = "Tests for the MyFirstCommand")]
    public class MyFirstCommandTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void WhenExecuted_CommandShouldReportSuccess()
        {
            var executionRepository = new ExecutionRepository();
            IGetCommandResult getCommandResult = executionRepository;
            IMailbox mailbox = executionRepository;
            IReportCommandExecution reportCommandExecution = executionRepository;
            var dispatcher = new MyDispatcher(reportCommandExecution, mailbox);
            var command = new MyFirstCommand(1, string.Empty);

            dispatcher.Send(command);

            var inquiry = new Inquiry(getCommandResult);
            var answer = inquiry.For(command);

            Assert.IsTrue(answer.Success);
        }
    }
}