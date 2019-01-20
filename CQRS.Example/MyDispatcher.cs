namespace CQRS.Example
{
    public sealed class MyDispatcher : IDispatcher
    {
        private readonly IMailbox _mailbox;
        private readonly IReportCommandExecution _reportCommandExecution;

        public MyDispatcher(IReportCommandExecution reportCommandExecution, IMailbox mailbox)
        {
            _reportCommandExecution = reportCommandExecution;
            _mailbox = mailbox;
        }

        public void Send(ICommand command)
        {
            _mailbox.Insert(command);
            new MyFirstCommandHandler(_reportCommandExecution).Handle((MyFirstCommand)command);
        }
    }
}
