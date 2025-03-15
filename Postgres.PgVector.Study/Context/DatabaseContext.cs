using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Postgres.PgVector.Study.Models;

namespace Postgres.PgVector.Study.Context;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProductModel> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("vector");
        modelBuilder.Entity<ProductModel>(builder =>
        {
            builder
                .Property(p => p.Embeddings)
                .HasColumnType("vector(1024)");

            builder
                .HasIndex(_ => _.Embeddings)
                .HasMethod("hnsw")
                .HasOperators("vector_cosine_ops");
        });
        
        base.OnModelCreating(modelBuilder);
    }
}