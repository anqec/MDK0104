using System.ComponentModel.DataAnnotations;

namespace RestApiDemo.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите имя студента.")]
    [StringLength(100, MinimumLength = 1)]
    public required string Name { get; set; }

    [Range(1, 120, ErrorMessage = "Возраст должен быть от 1 до 120.")]
    public int Age { get; set; }
}
