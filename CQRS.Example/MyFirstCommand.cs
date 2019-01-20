using System;

namespace CQRS.Example
{
    public class MyFirstCommand : ICommand
    {
        public int SomeNumber { get; }
        public string SomeDescription { get; }

        public MyFirstCommand(int someNumber, string someDescription)
        {
            SomeNumber = someNumber;
            SomeDescription = someDescription;
        }
    }

    internal class MyFirstCommandHandler : ICommandHandler<MyFirstCommand>
    {
        private readonly IReportCommandExecution _reportCommandExecution;

        public MyFirstCommandHandler(IReportCommandExecution reportCommandExecution)
        {
            _reportCommandExecution = reportCommandExecution;
        }

        public void Handle(MyFirstCommand command)
        {
            Console.WriteLine(command.SomeNumber);
            Console.WriteLine(command.SomeDescription);
            _reportCommandExecution.Finished(command);
        }
    }
}
