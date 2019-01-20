using System;

namespace CQRS
{
    internal sealed class Envelope : ICommand
    {
        public Guid Id { get; }
        public ICommand Command { get; }

        public Envelope(ICommand command)
        {
            Id = Guid.NewGuid();
            Command = command;
        }
    }
}
