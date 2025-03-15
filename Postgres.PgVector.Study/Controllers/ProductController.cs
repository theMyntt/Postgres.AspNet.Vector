using Microsoft.AspNetCore.Mvc;
using Pgvector;
using Postgres.PgVector.Study.Context;
using Postgres.PgVector.Study.Models;
using Postgres.PgVector.Study.Services;

namespace Postgres.PgVector.Study.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController
{
    private readonly DatabaseContext _context;

    public ProductController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(
        [FromBody] ProductModel dto,
        [FromServices] OllamaEmbedingService ollamaService)
    {
        var embeddings = await ollamaService.GenerateEmbeddingsAsync(dto.Description);
        var vector = new Vector(embeddings);
        
        var model = new ProductModel
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Embeddings = vector
        };

        await _context.Products.AddAsync(model);
        await _context.SaveChangesAsync();

        return Results.Ok();
    }
}