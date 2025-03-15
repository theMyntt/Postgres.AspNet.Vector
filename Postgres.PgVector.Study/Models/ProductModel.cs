using System.ComponentModel.DataAnnotations.Schema;

namespace Postgres.PgVector.Study.Models;

[Table("Products")]
public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    
    [Column(TypeName = "vector(768)")]
    public float[] Embeddings { get; set; } = [];
}