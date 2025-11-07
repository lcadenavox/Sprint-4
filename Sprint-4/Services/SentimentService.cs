using Microsoft.ML;

namespace Sprint_4.Services;

public class SentimentService
{
    private readonly MLContext _ml;
    private readonly ITransformer _model;
    private readonly PredictionEngine<SentimentData, SentimentPrediction> _engine;

    public SentimentService()
    {
        _ml = new MLContext();
        // Simple pipeline with FeaturizeText + SdcaLogisticRegression
        var data = _ml.Data.LoadFromEnumerable(new List<SentimentData>
        {
            new("bom" , true),
            new("ótimo" , true),
            new("excelente" , true),
            new("adoro" , true),
            new("ruim" , false),
            new("péssimo" , false),
            new("horrível" , false),
            new("odeio" , false)
        });
        var pipeline = _ml.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
            .Append(_ml.BinaryClassification.Trainers.SdcaLogisticRegression());
        _model = pipeline.Fit(data);
        _engine = _ml.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
    }

    public SentimentPrediction Predict(string text)
    {
        return _engine.Predict(new SentimentData(text, false));
    }
}

public record SentimentData(string Text, bool Label);

public class SentimentPrediction
{
    public bool PredictedLabel { get; set; }
    public float Probability { get; set; }
    public float Score { get; set; }
}
