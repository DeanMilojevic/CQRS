namespace CQRS
{
    public interface IDispatcher
    {
        void Send(ICommand command);
    }
}
