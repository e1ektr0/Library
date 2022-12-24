using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models;

//todo: use in ef 7.0
//[PrimaryKey(nameof(State), nameof(LicensePlate))]
public class BookCategories : DateTimeEntity
{
    public long BookId { get; set; }
    [ForeignKey(nameof(BookId))]
    public Book? Book { get; set; }
    
    public long CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }
}