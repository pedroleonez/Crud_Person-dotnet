using Crud_Person.Data;
using Crud_Person.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Crud_Person.Routes;

public static class PersonRoute
{
    public static void PersonRoutes(this WebApplication app)
    {
        var route = app.MapGroup("person");

        route.MapPost("",
            async (PersonRequest req, PersonContext context) =>
            {
                var person = new PersonModel(req.name);
                await context.AddAsync(person);
                await context.SaveChangesAsync();
            });
        
        route.MapGet("",
            async (PersonContext context) =>
            {
                var persons = await context.Persons.ToListAsync();
                return Results.Ok(persons);
            });

        route.MapPut("{id:guid}",
            async (Guid id, PersonRequest req, PersonContext context) =>
            {
                var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);
                
                if (person is null)
                    return Results.NotFound();
                
                person.UpdateName(req.name);
                await context.SaveChangesAsync();
                
                return Results.Ok(person);
            });

        route.MapDelete("{id:guid}", 
            async (Guid id, PersonContext context) =>
        {
            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            
            if (person is null)
                return Results.NotFound();
            
            person.SetInactive();
            await context.SaveChangesAsync();
                
            return Results.Ok(person);
        });
    }
}