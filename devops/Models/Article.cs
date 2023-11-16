using System.ComponentModel.DataAnnotations;

namespace devops.Models;

public class Article
{
    [Key]
    public string Title { get; set; }
    [MaxLength(200)]
    public string? DisplayTitle { get; set; }
    [MaxLength(80)]
    public string? Author { get; set; }
    [MaxLength(50)]
    public string? Category { get; set; }
    [MaxLength(200)]
    public string? Subjects { get; set; }
    public DateTime? PostedOn { get; set; }
    public DateTime? LastEditedOn { get; set; }
    public string? Body { get; set; }
}
