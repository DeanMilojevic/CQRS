namespace CQRS
{
    public interface IMailbox
    {
        void Insert(ICommand command);
    }
}