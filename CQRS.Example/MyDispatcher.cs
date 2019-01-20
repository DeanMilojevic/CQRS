using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CQRS.Example
{
    public sealed class MyDispatcher : IDispatcher
    {
        private readonly IMailbox _mailbox;
        private readonly IReportCommandExecution _reportCommandExecution;

        private readonly Dictionary<Type, Type> _commandToHandlerMapping;

        public MyDispatcher(IReportCommandExecution reportCommandExecution, IMailbox mailbox)
        {
            _reportCommandExecution = reportCommandExecution;
            _mailbox = mailbox;

            _commandToHandlerMapping = new Dictionary<Type, Type>();
            DiscoverCommandsAndHandlers();
        }

        private void DiscoverCommandsAndHandlers()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes();
            foreach (var command in types.Where(t => t.GetInterface("ICommand") != null))
            {
                var handler = types.FirstOrDefault(t => t.Name == command.Name + "Handler");
                _commandToHandlerMapping[command] = handler ?? throw new ArgumentNullException($"The {command} has no defined handler for it.");
            }
        }

        public void Send(ICommand command)
        {
            _mailbox.Insert(command);

            var handler = _commandToHandlerMapping[command.GetType()];

            var instance = (dynamic)Activator.CreateInstance(handler, _reportCommandExecution);

            instance.Handle((dynamic)command);
        }
    }
}
