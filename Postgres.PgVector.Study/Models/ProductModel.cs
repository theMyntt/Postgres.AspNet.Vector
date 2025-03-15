using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Pgvector;

namespace Postgres.PgVector.Study.Models;

[Table("Products")]
public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    
    [JsonIgnore]
    public Vector? Embeddings { get; set; }
}