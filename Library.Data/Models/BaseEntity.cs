namespace Library.Data.Models;

public abstract class BaseEntity : DateTimeEntity
{
    public long Id { get; set; }
}