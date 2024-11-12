namespace JobDataAccess.Entities;

public interface IAggregateRoot<T>
{
    T Id { get; set; }
}
