using Microsoft.Extensions.AI;
using OllamaSharp;

namespace Postgres.PgVector.Study.Services;

public class OllamaEmbedingService
{
    private readonly OllamaApiClient _client = new("http://localhost:11434", "mxbai-embed-large");

    public async Task<float[]> GenerateEmbeddingsAsync(string text)
    {
        var response = await _client.GenerateEmbeddingAsync(text);
        return response.Vector.ToArray();
    }
}