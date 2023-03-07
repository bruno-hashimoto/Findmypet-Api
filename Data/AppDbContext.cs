using Microsoft.EntityFrameworkCore;
using Authenticate.Models;

namespace Authenticate.Data;

public class AppDbContext : DbContext
{
    public DbSet<TodoModel> Todos { get; set; }

    protected void OnConfigurin(DbContextOptionsBuilder options)
        => options.UseSqlite("dataSource=app.db; Cache=Shared");
}