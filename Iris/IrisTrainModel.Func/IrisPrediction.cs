using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Extensions.ML;
namespace djkormo.Function
{
    /* 
    public static Class ModelInput 
    {
       
        public float SepalLength { get; set; }

        public float SepalWidth { get; set; }

        public float PetalLength { get; set; }

        public float PetalWidth { get; set; }

        public string Label { get; set; }

    }

   public static class ModelOutput
    {
        // ColumnName attribute is used to change the column name from
        // its default value, which is the name of the field.
        public String Prediction { get; set; }
        public float[] Score { get; set; }
    }
    
    */
     // http://luisquintanilla.me/2018/08/21/serverless-machine-learning-mlnet-azure-functions/   
     // https://github.com/Azure/azure-webjobs-sdk/issues/1879

    public static class IrisPrediction
    {
        [FunctionName("IrisPrediction")]
        public static IActionResult Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
    [Blob("models/model.zip", FileAccess.Read, Connection = "AzureWebJobsStorage")] Stream serializedModel,
    ILogger log )
{
    // Workaround for Azure Functions Host
    /* 
    if (typeof(Microsoft.ML.Runtime.Data.LoadTransform) == null ||
        typeof(Microsoft.ML.Runtime.Learners.LinearClassificationTrainer) == null ||
        typeof(Microsoft.ML.Runtime.Internal.CpuMath.SseUtils) == null ||
        typeof(Microsoft.ML.Runtime.FastTree.FastTree) == null)
    {
        log.Error("Error loading ML.NET");
        return new StatusCodeResult(500);
    }
    */

    //Read incoming request body
    string requestBody = new StreamReader(req.Body).ReadToEnd();

    log.LogInformation("C# HTTP trigger function processed a request.");
    log.LogInformation(requestBody);

    //Bind request body to IrisData object
    ModelInput data = JsonConvert.DeserializeObject<ModelInput>(requestBody);

    //Load prediction model
    var model = PredictionModel.ReadAsync<ModelInput, ModelOutput>(serializedModel).Result;

    //Make prediction
    ModelOutput prediction = model.Predict(data);

    //Return prediction
    return (IActionResult)new OkObjectResult(prediction.Prediction);
}
}
}
