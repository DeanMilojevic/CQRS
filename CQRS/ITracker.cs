namespace CQRS
{
    public interface ITracker
    {
        ICommand AttachTo(ICommand command);
    }
}