using Microsoft.EntityFrameworkCore;
using TechnightApi.Data;
using TechnightApi.DTOs;
using TechnightApi.Models;

namespace TechnightApi.Endpoints;

/// <summary>
/// Extension methods for mapping Example endpoints
/// </summary>
public static class ExampleEndpoints
{
    /// <summary>
    /// Maps all Example-related endpoints
    /// </summary>
    public static IEndpointRouteBuilder MapExampleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/examples")
            .WithTags("Examples");

        // GET /api/examples - Get all examples
        group.MapGet("/", GetAllExamples)
            .WithName("GetAllExamples")
            .WithSummary("Get all examples")
            .WithDescription("Retrieves all example records from the database")
            .Produces<List<Example>>(200);

        // GET /api/examples/{id} - Get example by ID
        group.MapGet("/{id}", GetExampleById)
            .WithName("GetExampleById")
            .WithSummary("Get example by ID")
            .WithDescription("Retrieves a specific example by its ID")
            .Produces<Example>(200)
            .Produces(404);

        // POST /api/examples - Create a new example
        group.MapPost("/", CreateExample)
            .WithName("CreateExample")
            .WithSummary("Create a new example")
            .WithDescription("Creates a new example record")
            .Produces<Example>(201)
            .Produces(400);

        // PUT /api/examples/{id} - Update an existing example
        group.MapPut("/{id}", UpdateExample)
            .WithName("UpdateExample")
            .WithSummary("Update an example")
            .WithDescription("Updates an existing example record")
            .Produces<Example>(200)
            .Produces(404)
            .Produces(400);

        // DELETE /api/examples/{id} - Delete an example
        group.MapDelete("/{id}", DeleteExample)
            .WithName("DeleteExample")
            .WithSummary("Delete an example")
            .WithDescription("Deletes an example record")
            .Produces(204)
            .Produces(404);

        // GET /api/examples/search - Search examples by name
        group.MapGet("/search", SearchExamples)
            .WithName("SearchExamples")
            .WithSummary("Search examples")
            .WithDescription("Search examples by name (case-insensitive)")
            .Produces<List<Example>>(200);

        return app;
    }

    private static async Task<IResult> GetAllExamples(ApplicationDbContext db)
    {
        var examples = await db.Examples
            .OrderByDescending(e => e.EntryDate)
            .ToListAsync();

        return Results.Ok(examples);
    }

    private static async Task<IResult> GetExampleById(int id, ApplicationDbContext db)
    {
        var example = await db.Examples.FindAsync(id);

        if (example is null)
        {
            return Results.NotFound(new { message = $"Example with ID {id} not found" });
        }

        return Results.Ok(example);
    }

    private static async Task<IResult> CreateExample(
        CreateExampleDto dto,
        ApplicationDbContext db)
    {
        var example = new Example
        {
            Name = dto.Name,
            Title = dto.Title,
            Description = dto.Description,
            EntryDate = DateTime.UtcNow,
            IsActive = dto.IsActive
        };

        db.Examples.Add(example);
        await db.SaveChangesAsync();

        return Results.Created($"/api/examples/{example.Id}", example);
    }

    private static async Task<IResult> UpdateExample(
        int id,
        UpdateExampleDto dto,
        ApplicationDbContext db)
    {
        var example = await db.Examples.FindAsync(id);

        if (example is null)
        {
            return Results.NotFound(new { message = $"Example with ID {id} not found" });
        }

        // Update only the provided fields
        if (dto.Name is not null)
        {
            example.Name = dto.Name;
        }

        if (dto.Title is not null)
        {
            example.Title = dto.Title;
        }

        if (dto.Description is not null)
        {
            example.Description = dto.Description;
        }

        if (dto.IsActive.HasValue)
        {
            example.IsActive = dto.IsActive.Value;
        }

        await db.SaveChangesAsync();

        return Results.Ok(example);
    }

    private static async Task<IResult> DeleteExample(int id, ApplicationDbContext db)
    {
        var example = await db.Examples.FindAsync(id);

        if (example is null)
        {
            return Results.NotFound(new { message = $"Example with ID {id} not found" });
        }

        db.Examples.Remove(example);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    private static async Task<IResult> SearchExamples(
        string? name,
        ApplicationDbContext db)
    {
        var query = db.Examples.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));
        }

        var examples = await query
            .OrderByDescending(e => e.EntryDate)
            .ToListAsync();

        return Results.Ok(examples);
    }
}

