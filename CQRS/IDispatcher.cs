namespace CQRS
{
    public interface IDispatcher
    {
        void Send(ICommand command);

        T Send<T>(IQuery<T> query);
    }
}
