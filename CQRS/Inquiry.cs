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

        // give command produced by some factory (which actually returns the ICommand and wraps the actual command)
        // give query (probably this can be a generic query that produces, eventually, the result of the execution of the command)
        // this needs to depend on some internal repository of commands executed/executing in the process
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
