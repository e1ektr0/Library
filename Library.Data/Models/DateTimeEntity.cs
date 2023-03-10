namespace Library.Data.Models;

public abstract class DateTimeEntity : IDateTimeEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}