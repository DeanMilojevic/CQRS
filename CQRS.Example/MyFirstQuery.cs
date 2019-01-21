namespace CQRS.Example
{
    public struct SomeInfo
    {
        public int SomeNumber { get; }
        public int SomeOtherNumber { get; }

        public SomeInfo(int someNumber, int someOtherNumber)
        {
            SomeNumber = someNumber;
            SomeOtherNumber = someOtherNumber;
        }
    }

    public sealed class MyFirstQuery : IQuery<SomeInfo>
    {
        public int Id { get; }

        public MyFirstQuery(int id)
        {
            Id = id;
        }
    }

    internal sealed class MyFirstQueryHandler : IQueryHandler<MyFirstQuery, SomeInfo>
    {
        public SomeInfo Handle(MyFirstQuery query)
        {
            return new SomeInfo(1, 2);
        }
    }
}
