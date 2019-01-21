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
        private readonly Dictionary<Type, Type> _queryToHandlerMapping;

        public MyDispatcher(IReportCommandExecution reportCommandExecution, IMailbox mailbox)
        {
            _reportCommandExecution = reportCommandExecution;
            _mailbox = mailbox;

            _commandToHandlerMapping = new Dictionary<Type, Type>();
            _queryToHandlerMapping = new Dictionary<Type, Type>();

            Discover();
        }

        private void Discover()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes();

            foreach (var command in types.Where(t => t.GetInterface(nameof(ICommand)) != null))
            {
                var handler = types.FirstOrDefault(t => t.Name == command.Name + "Handler");
                _commandToHandlerMapping[command] = handler ?? throw new ArgumentNullException($"The {command} has no defined handler.");
            }

            foreach (var query in types.Where(t => t.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<>)) != null))
            {
                var handler = types.FirstOrDefault(t => t.Name == query.Name + "Handler");
                _queryToHandlerMapping[query] = handler ?? throw new ArgumentNullException($"The {query} has no defined handler.");
            }
        }

        public void Send(ICommand command)
        {
            _mailbox.Insert(command);

            var handler = _commandToHandlerMapping[command.GetType()];

            var instance = (dynamic)Activator.CreateInstance(handler, _reportCommandExecution);

            instance.Handle((dynamic)command);
        }

        public T Send<T>(IQuery<T> query)
        {
            var handler = _queryToHandlerMapping[query.GetType()];

            var instance = (dynamic)Activator.CreateInstance(handler);

            var result = (T)instance.Handle((dynamic)query);

            return result;
        }
    }
}
