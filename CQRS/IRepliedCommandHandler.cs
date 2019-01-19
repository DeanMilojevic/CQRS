namespace CQRS
{
    public interface IRepliedCommandHandler<TResult> : IRepliedCommand<TResult>
    {
        TResult Handle(IRepliedCommand<TResult> command);
    }
}
