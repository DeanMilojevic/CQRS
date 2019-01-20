namespace CQRS
{
    public interface IGetCommandResult
    {
        Finished For(ICommand command);
    }
}