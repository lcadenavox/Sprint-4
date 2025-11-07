using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Sprint_4.Services;

namespace Sprint_4.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ml")]
public class MLController : ControllerBase
{
    private readonly SentimentService _sentiment;
    public MLController(SentimentService sentiment)
    {
        _sentiment = sentiment;
    }

    /// <summary>
    /// Analisa sentimento de um texto simples (português) demonstrando uso do ML.NET.
    /// </summary>
    /// <param name="text">Texto para análise</param>
    [HttpGet("sentiment")]
    [SwaggerResponse(200, "Resultado de sentimento.")]
    public IActionResult PredictSentiment([FromQuery] string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return BadRequest("text obrigatório");
        var result = _sentiment.Predict(text);
        return Ok(new
        {
            input = text,
            positivo = result.PredictedLabel,
            probabilidade = result.Probability,
            score = result.Score
        });
    }
}
