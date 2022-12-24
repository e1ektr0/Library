namespace Library.Data.Models;

public interface IDateTimeEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}