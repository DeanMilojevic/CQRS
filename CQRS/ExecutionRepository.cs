using System;
using System.Collections.Concurrent;

namespace CQRS
{
    public sealed class ExecutionRepository : IMailbox, IGetCommandResult, IReportCommandExecution
    {
        private readonly ConcurrentDictionary<string, Finished> _commands;

        public ExecutionRepository()
        {
            _commands = new ConcurrentDictionary<string, Finished>();
        }

        public void Insert(ICommand command)
        {
            EnsureCommandIsProvided(command);

            var id = GetCommandId(command);

            _commands[id] = Finished(false, false);
        }

        public Finished For(ICommand command)
        {
            EnsureCommandIsProvided(command);

            var commandId = GetCommandId(command);

            EnsureCommandIdIsRegistered(commandId);

            return _commands[commandId];
        }

        public void Finished(ICommand command)
        {
            EnsureCommandIsProvided(command);

            var commandId = GetCommandId(command);

            EnsureCommandIdIsRegistered(commandId);

            _commands[commandId] = Finished(true, false);
        }

        public void Failed(ICommand command)
        {
            EnsureCommandIsProvided(command);

            var commandId = GetCommandId(command);

            EnsureCommandIdIsRegistered(commandId);

            _commands[commandId] = Finished(true, true);
        }

        #region Helpers

        private static Finished Finished(bool success, bool failure) => new Finished(success, failure);

        private void EnsureCommandIsProvided(ICommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
        }

        private void EnsureCommandIdIsRegistered(string key)
        {
            if (!_commands.TryGetValue(key, out _))
            {
                throw new ArgumentOutOfRangeException(
                    $"The command was never registered. Please register command for tracking first.");
            }
        }

        private static string GetCommandId(ICommand command) => command is Envelope envelope ? envelope.Id.ToString() : command.GetType().FullName;

        #endregion
    }
}
