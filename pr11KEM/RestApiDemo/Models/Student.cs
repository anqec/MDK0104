using System.ComponentModel.DataAnnotations;

namespace RestApiDemo.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Range(1, 120)]
    public int Age { get; set; }
}
