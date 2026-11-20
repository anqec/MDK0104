using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiDemo.Data;
using RestApiDemo.Models;

namespace RestApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly SchoolContext _context;

    public StudentsController(SchoolContext context)
    {
        _context = context;
    }

    // GET /api/students — получить всех студентов
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.AsNoTracking().OrderBy(s => s.Id).ToListAsync();
    }

    // GET /api/students/1 — получить студента по Id
    [HttpGet("{id:int}", Name = nameof(GetStudent))]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (student is null) return NotFound();
        return student;
    }

    // POST /api/students — добавить студента
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtRoute(nameof(GetStudent), new { id = student.Id }, student);
    }

    // PUT /api/students/1 — обновить студента
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        if (id != student.Id) return BadRequest("Id в адресе и теле запроса должны совпадать.");

        var existing = await _context.Students.FindAsync(id);
        if (existing is null) return NotFound();

        existing.Name = student.Name;
        existing.Age = student.Age;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/students/1 — удалить студента
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student is null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
