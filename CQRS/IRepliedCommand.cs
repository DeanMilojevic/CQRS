namespace CQRS
{
    /// <summary>
    /// Represents a decorator pattern for the implementations of the commands where we do expect an "answer" or a result of the execution.
    /// </summary>
    /// <typeparam name="TResult">The result that we expect back after execution of the command.</typeparam>
    public interface IRepliedCommand<out TResult> : ICommand
    {
        TResult Handle(ICommand command);
    }
}
