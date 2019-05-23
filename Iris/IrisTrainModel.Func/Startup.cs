/* based on https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/serve-model-serverless-azure-functions-ml-net */

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.ML;

[assembly: WebJobsStartup(typeof(Startup))]
namespace djkormo.Function
{
    class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
                .FromFile("models/model.zip");
        }
    }
}