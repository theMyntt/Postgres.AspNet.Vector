using Microsoft.EntityFrameworkCore;
using Postgres.PgVector.Study.Context;
using Postgres.PgVector.Study.Services;

namespace Postgres.PgVector.Study;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration["PgSql:ConnectionString"];

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("PgSql:ConnectionString Is Null");

        // Add services to the container.
        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(connectionString, o => o.UseVector());
        });
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<OllamaEmbedingService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}