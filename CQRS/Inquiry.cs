using System;

namespace CQRS
{
    public sealed class Answer
    {
        public bool Success { get; }

        public Answer(bool success)
        {
            Success = success;
        }
    }

    public sealed class Inquiry
    {
        private readonly IGetCommandResult _getCommandResult;

        public Inquiry(IGetCommandResult getCommandResult)
        {
            _getCommandResult = getCommandResult;
        }

        public Answer For(ICommand command)
        {
            EnsureCommandIsProvided(command);

            var finished = _getCommandResult.For(command);

            return new Answer(finished.InSuccess());
        }

        private static void EnsureCommandIsProvided(ICommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
        }
    }
}
