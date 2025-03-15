using Microsoft.EntityFrameworkCore;
using Postgres.PgVector.Study.Models;

namespace Postgres.PgVector.Study.Context;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProductModel> Products { get; set; }
}