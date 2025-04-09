using Crud_Person.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud_Person.Data;

public class PersonContext : DbContext
{
    public DbSet<PersonModel> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=person.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}