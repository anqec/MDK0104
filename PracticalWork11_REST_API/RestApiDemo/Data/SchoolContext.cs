using Microsoft.EntityFrameworkCore;
using RestApiDemo.Models;

namespace RestApiDemo.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
}
