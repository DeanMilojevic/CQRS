using System;

namespace CQRS
{
    public sealed class Tracker : ITracker
    {
        private readonly IMailbox _mailbox;

        public Tracker(IMailbox mailbox)
        {
            _mailbox = mailbox;
        }

        public ICommand AttachTo(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var envelope = new Envelope(command);

            _mailbox.Insert(envelope);

            return envelope;
        }
    }
}
