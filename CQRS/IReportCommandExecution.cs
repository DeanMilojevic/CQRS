namespace CQRS
{
    public interface IReportCommandExecution
    {
        void Finished(ICommand command);

        void Failed(ICommand command);
    }
}